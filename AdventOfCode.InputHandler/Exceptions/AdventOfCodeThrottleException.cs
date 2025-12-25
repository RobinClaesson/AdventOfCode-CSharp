namespace AdventOfCode.InputHandler.Exceptions;

public class AdventOfCodeThrottleException(RequestInfo? lastRequest)
    : AdventOfCodeException(
        $"""
         Requests to Advent of Code can only be made every {AdventOfCodeClient.RequestThrottleSeconds} seconds.
         See: https://old.reddit.com/r/adventofcode/wiki/faqs/automation#wiki_throttle_your_outbound_requests
         Next allowed request at UTC {lastRequest?.NextRequestAllowedAt ?? DateTime.UtcNow.AddSeconds(AdventOfCodeClient.RequestThrottleSeconds)}.
         Use {nameof(AdventOfCodeClient)}.{nameof(AdventOfCodeClient.CanMakeRequest)}() to know if request is allowed.
         """)
{
}