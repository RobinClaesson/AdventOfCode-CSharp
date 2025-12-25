using System.Net;
using System.Text.Json;
using AdventOfCode.InputHandler.Cache;
using AdventOfCode.InputHandler.Exceptions;

namespace AdventOfCode.InputHandler;

public class AdventOfCodeClient(IInputCache inputCache, string sessionToken, string contact)
{
    public const string BaseUrl = "https://adventofcode.com";
    public const int RequestThrottleSeconds = 900;

    private const string LastRequestFileName = "LastRequest.json";
    private static readonly Uri BaseUri = new(BaseUrl);

    public IInputCache Cache => inputCache;
    public string SessionToken => sessionToken;
    public string Contact => contact;

    public async Task<string> GetInputAsync(int year, int day, bool force = false)
    {
        if (Cache.HasInput(year, day) && !force)
            return await Cache.GetInputAsync(year, day);

        ThrottleCheck();

        var endpoint = new Uri(BaseUri, $"{year}/day/{day}/input");
        var userAgent =
            $"{nameof(AdventOfCodeClient)}/1.0 (github.com/RobinClaesson/AdventOfCode-CSharp used by {contact})";

        using var handler = new HttpClientHandler();
        handler.CookieContainer = new CookieContainer();
        handler.CookieContainer.Add(BaseUri, new Cookie("session", sessionToken));

        using var httpClient = new HttpClient(handler);
        httpClient.DefaultRequestHeaders.Add("user-agent", userAgent);

        LogLastRequest(endpoint.ToString());
        var response = await httpClient.GetAsync(endpoint);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Input request to '{endpoint}' returned status code '{(int)response.StatusCode}: {response.ReasonPhrase}'",
                null,
                response.StatusCode);
        }

        var input = await response.Content.ReadAsStringAsync();
        await Cache.CacheInputAsync(year, day, input);
        return input;
    }

    private void LogLastRequest(string endPoint)
    {
        var requestInfo = new RequestInfo
        {
            Contact = Contact,
            SessionToken = SessionToken,
            Endpoint = endPoint
        };

        var json = JsonSerializer.Serialize(requestInfo);
        File.WriteAllText(LastRequestFileName, json);
    }

    private static void ThrottleCheck()
    {
        if (CanMakeRequest()) return;
        throw new AdventOfCodeThrottleException(GetLastRequest());
    }

    public static bool CanMakeRequest()
    {
        var lastRequest = GetLastRequest();
        return lastRequest is null || DateTime.UtcNow > lastRequest.NextRequestAllowedAt;
    }

    public static RequestInfo? GetLastRequest()
    {
        if (!File.Exists(LastRequestFileName))
            return null;

        var fileContent = File.ReadAllText(LastRequestFileName);
        return JsonSerializer.Deserialize<RequestInfo>(fileContent);
    }
}