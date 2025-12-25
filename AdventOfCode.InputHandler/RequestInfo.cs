namespace AdventOfCode.InputHandler;

public record RequestInfo
{
    public DateTime Time { get; init; } = DateTime.UtcNow;
    public required string SessionToken { get; init; }
    public required string Contact { get; init; }
    public required string Endpoint { get; init; }

    public DateTime NextRequestAllowedAt => Time.AddSeconds(AdventOfCodeClient.RequestThrottleSeconds);
};