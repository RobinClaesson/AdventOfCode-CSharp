namespace AdventOfCode.Solutions.Extensions;

public static class ListExtensions
{
    extension<T>(List<T> list)
    {
        public List<List<T>> Permutations()
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

        public List<List<T>> Subsets() => Enumerable.Range(1, (int)Math.Pow(2, list.Count) - 1)
            .Select(n => Convert.ToString(n, 2)
                .PadLeft(list.Count, '0')
                .Select((c, i) => (Char: c, Index: i))
                .Where(x => x.Char == '1')
                .Select(x => list[x.Index])
                .ToList())
            .ToList();
    }

    extension<T>(IEnumerable<T> source)
    {
        public List<List<T>> Permutations() => source.ToList().Permutations();
        public List<List<T>> Subsets() => source.ToList().Subsets();
    }
}