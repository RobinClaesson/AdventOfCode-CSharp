namespace AdventOfCode.Solutions.IO;

/// <summary>
/// Helper functions to format solution outputs
/// </summary>
internal static class Output
{
    private static int _outputs = 0;

    /// <summary>
    /// Outputs the answer to the console
    /// </summary>
    /// <param name="answer">Calculated answer</param>
    /// <typeparam name="T">Type of the answer</typeparam>
    public static void Answer<T>(T answer)
    {
        Console.WriteLine($"Answer {++_outputs}: {answer}");
    }

    /// <summary>
    /// The minium time that has to pass between <see cref="Log"/> prints. <br />
    /// Default: <c>1 second</c>
    /// </summary>
    public static TimeSpan MinLogInterval { get; set; } = TimeSpan.FromSeconds(1);

    /// <summary>
    /// UTC time for last <see cref="Log"/> print
    /// </summary>
    public static DateTime LastLogAtUtc { get; private set; } = DateTime.MinValue;

    /// <summary>
    /// Prints a log message with the given object if at least <see cref="MinLogInterval"/> time
    /// has passed since last log print.
    /// </summary>
    /// <returns><c>true</c> if a log was printed, otherwise <c>false</c></returns>
    /// <param name="toLog">Objects to log</param>
    public static bool Log(params object[] toLog)
    {
        var now = DateTime.UtcNow;
        var interval = now - LastLogAtUtc;

        if (interval < MinLogInterval)
            return false;

        LastLogAtUtc = now;
        Console.WriteLine($"[{LastLogAtUtc}] {string.Join(" ", toLog)}");
        return true;
    }
}