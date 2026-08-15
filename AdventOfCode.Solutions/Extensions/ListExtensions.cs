namespace AdventOfCode.Solutions.Extensions;

public static class ListExtensions
{
    public static List<List<T>> Permutations<T>(this IEnumerable<T> enumerable) => enumerable.ToList().Permutations();
    public static List<List<T>> Permutations<T>(this List<T> list)
    {
        return list.Count switch
        {
            < 2 => [list],
            2 => [[list[0], list[1]], [list[1], list[0]]],
            > 2 => list.SelectMany(t =>
                {
                    var subPaths = list.Except([t]).ToList().Permutations();
                    subPaths.ForEach(p => p.Insert(0, t));
                    return subPaths;
                })
                .ToList()
        };
    }
}