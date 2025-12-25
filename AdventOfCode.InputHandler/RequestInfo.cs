namespace AdventOfCode.InputHandler;

/// <summary>
/// Contains information about a request made from <see cref="AdventOfCodeClient"/> 
/// </summary>
public record RequestInfo
{
    /// <summary>
    /// UTC time when the request was made
    /// </summary>
    public DateTime Time { get; init; } = DateTime.UtcNow;
    /// <summary>
    /// Session token that was used for the request
    /// </summary>
    public required string SessionToken { get; init; }
    /// <summary>
    /// Contact information that was provided for the request
    /// </summary>
    public required string Contact { get; init; }
    /// <summary>
    /// The endpoint that the request was made to
    /// </summary>
    public required string Endpoint { get; init; }

    /// <summary>
    /// UTC time after which any subsequent requests will be allowed by a <see cref="AdventOfCodeClient"/>
    /// </summary>
    public DateTime NextRequestAllowedAt => Time.AddSeconds(AdventOfCodeClient.RequestThrottleSeconds);
};