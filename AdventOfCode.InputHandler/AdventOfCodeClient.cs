using System.Net;
using System.Text.Json;
using AdventOfCode.InputHandler.Cache;
using AdventOfCode.InputHandler.Exceptions;

namespace AdventOfCode.InputHandler;

/// <summary>
/// Class for fetching and caching input from Advent of Code
/// </summary>
/// <param name="inputCache">Cache source to use for inputs</param>
/// <param name="sessionToken">Advent of Code session authentication token</param>
/// <param name="contact">Contact information for user of the client. Best practice: 'github.com/user/repo-using-client'</param>
public class AdventOfCodeClient(IInputCache inputCache, string sessionToken, string contact)
{
    /// <summary>
    /// URL for Advent of Code requests
    /// </summary>
    public const string BaseUrl = "https://adventofcode.com";

    /// <summary>
    /// Amount of seconds that has to pass between each request to Advent of Code
    /// </summary>
    public const int RequestThrottleSeconds = 900;

    private const string LastRequestFileName = "LastRequest.json";
    private static readonly Uri BaseUri = new(BaseUrl);

    /// <summary>
    /// Provided cache source used for inputs
    /// </summary>
    public IInputCache Cache => inputCache;

    /// <summary>
    /// Provided session token for requests 
    /// </summary>
    public string SessionToken => sessionToken;

    /// <summary>
    /// Provided contact information included in requests 'user-agent' message 
    /// </summary>
    public string Contact => contact;

    /// <summary>
    /// Gets the input for the given puzzle and caches the result in <see cref="Cache"/>.<br/>
    /// Returns cached input without making request cache contains puzzle input for provided day.
    /// Requests to Advent of Code are throttled, see <see cref="RequestThrottleSeconds"/>.<br/>
    /// Inputs returned from cache are not affected by throttle limit.
    /// </summary>
    /// <param name="year">Year of puzzle</param>
    /// <param name="day">Day of puzzle</param>
    /// <param name="force">Force request to Advent of Code ignoring cache content</param>
    /// <returns>Task representing the asynchronous fetching of the cached puzzle input</returns>
    /// <exception cref="AdventOfCodeThrottleException">Thrown when requests are made outside throttle limits.</exception>
    /// <exception cref="HttpRequestException">Thrown when requests to Advent of Code returns with a non-success status code.</exception>
    public async Task<string> GetInputAsync(int year, int day, bool force = false)
    {
        if (Cache.HasInput(year, day) && !force)
            return await Cache.GetInputAsync(year, day);

        ThrottleExceptionCheck();

        var endpoint = new Uri(BaseUri, $"{year}/day/{day}/input");
        var userAgent =
            $"{nameof(AdventOfCodeClient)}/1.0 (From {nameof(AdventOfCodeClient)} in github.com/RobinClaesson/AdventOfCode-CSharp, used by {contact})";

        using var handler = new HttpClientHandler();
        handler.CookieContainer = new CookieContainer();
        handler.CookieContainer.Add(BaseUri, new Cookie("session", sessionToken));

        using var httpClient = new HttpClient(handler);
        httpClient.DefaultRequestHeaders.Add("user-agent", userAgent);

        SaveLastRequest(endpoint.ToString());
        var response = await httpClient.GetAsync(endpoint);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Input request to '{endpoint}' returned status code '{(int)response.StatusCode}: {response.ReasonPhrase}'",
                null,
                response.StatusCode);
        }

        var input = await response.Content.ReadAsStringAsync();
        input = input.Trim();
        await Cache.CacheInputAsync(year, day, input);
        return input;
    }

    /// <summary>
    /// Creates and saves <see cref="RequestInfo"/> for a request from this client to the provided <paramref name="endpoint"/>.
    /// </summary>
    /// <param name="endpoint">The url that the request was made to</param>
    private void SaveLastRequest(string endpoint)
    {
        var requestInfo = new RequestInfo
        {
            Contact = Contact,
            SessionToken = SessionToken,
            Endpoint = endpoint
        };

        var json = JsonSerializer.Serialize(requestInfo);
        File.WriteAllText(LastRequestFileName, json);
    }

    /// <exception cref="AdventOfCodeThrottleException">Thrown when new request can't be without breaking throttle limits.</exception>
    private static void ThrottleExceptionCheck()
    {
        if (CanMakeRequest()) return;
        throw new AdventOfCodeThrottleException(GetLastRequest());
    }

    /// <summary>
    /// Checks if a new request to Advent of Code can be made within throttle limits.<br/>
    /// </summary>
    /// <returns><c>true</c> if request can be made, <c>false otherwise.</c></returns>
    public static bool CanMakeRequest()
    {
        var lastRequest = GetLastRequest();
        return lastRequest is null || DateTime.UtcNow > lastRequest.NextRequestAllowedAt;
    }

    /// <summary>
    /// Returns the information for the last request made to Advent of Code
    /// </summary>
    /// <returns></returns>
    public static RequestInfo? GetLastRequest()
    {
        if (!File.Exists(LastRequestFileName))
            return null;

        var fileContent = File.ReadAllText(LastRequestFileName);
        return JsonSerializer.Deserialize<RequestInfo>(fileContent);
    }
}