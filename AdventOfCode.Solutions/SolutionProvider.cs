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

    public static bool HasSolution(int year, int day) => AllSolutionTypes
        .Any(t => IsSolutionForPuzzle(t, year, day));

    public static IAdventOfCodeSolution? GetSolution(int year, int day)
    {
        var solutionType = AllSolutionTypes
            .FirstOrDefault(t => IsSolutionForPuzzle(t, year, day));

        return solutionType is null ? null : Activator.CreateInstance(solutionType) as IAdventOfCodeSolution;
    }
}