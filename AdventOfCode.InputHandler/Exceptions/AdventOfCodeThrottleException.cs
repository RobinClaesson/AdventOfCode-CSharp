namespace AdventOfCode.InputHandler.Exceptions;

/// <summary>
/// Represents errors that occurs when requests are made to frequent to Advent of Code
/// </summary>
public class AdventOfCodeThrottleException(RequestInfo? lastRequest)
    : AdventOfCodeException(
        $"""
         Requests to Advent of Code can only be made every {AdventOfCodeClient.RequestThrottleSeconds} seconds.
         See: https://www.reddit.com/r/adventofcode/wiki/faqs/automation/
         Next allowed request at UTC {lastRequest?.NextRequestAllowedAt ?? DateTime.UtcNow.AddSeconds(AdventOfCodeClient.RequestThrottleSeconds)}.
         Use {nameof(AdventOfCodeClient)}.{nameof(AdventOfCodeClient.CanMakeRequest)}() to know if request is allowed.
         """);