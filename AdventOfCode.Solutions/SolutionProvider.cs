using System.Reflection;

namespace AdventOfCode.Solutions;

public static class SolutionProvider
{
    private static readonly Type SolutionInterfaceType = typeof(IAdventOfCodeSolution);

    private static readonly List<Type> AllSolutionTypes = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(a => a.GetTypes())
        .Where(t => SolutionInterfaceType.IsAssignableFrom(t))
        .ToList();

    private static bool IsSolutionForPuzzle(Type type, int year, int day) =>
        type.GetCustomAttribute<AdventOfCodeSolutionAttribute>() is { } solutionAttribute
        && solutionAttribute.Year == year && solutionAttribute.Day == day;

    private static int GetStarsForPuzzle(Type type, int year, int day) =>
        type.GetCustomAttribute<AdventOfCodeSolutionAttribute>() is { } solutionAttribute
            ? solutionAttribute.Stars
            : 0;

    /// <summary>
    /// Checks if a solution is implemented for the given puzzle
    /// </summary>
    /// <param name="year">Year of puzzle</param>
    /// <param name="day">Day of puzzle</param>
    /// <returns><c>true</c> if solution exists, <c>false</c> otherwise</returns>
    public static bool HasSolution(int year, int day) => AllSolutionTypes
        .Any(t => IsSolutionForPuzzle(t, year, day));

    /// <summary>
    /// Get an instance of the solution for the given puzzle if it exists
    /// </summary>
    /// <param name="year">Year of puzzle</param>
    /// <param name="day">Day of puzzle</param>
    /// <returns>Instance of the <see cref="IAdventOfCodeSolution"/> for the puzzle if it exists, <c>null</c> otherwise</returns>
    public static IAdventOfCodeSolution? GetSolution(int year, int day)
    {
        var solutionType = AllSolutionTypes
            .FirstOrDefault(t => IsSolutionForPuzzle(t, year, day));

        return solutionType is null ? null : Activator.CreateInstance(solutionType) as IAdventOfCodeSolution;
    }

    /// <summary>
    /// Get the number of collected stars for all puzzles with solutions.
    /// </summary>
    /// <returns><see langword="IReadOnlyDictionary"/> with <c>(Year, Day)</c> as key and number of stars as value for all found solutions.</returns>
    public static IReadOnlyDictionary<(int Year, int Day), int> GetCollectedStars() => AllSolutionTypes
        .Select(t => t.GetCustomAttribute<AdventOfCodeSolutionAttribute>())
        .Where(t => t is not null)
        .Cast<AdventOfCodeSolutionAttribute>()
        .ToDictionary(s => (s.Year, s.Day), s => s.Stars);
}