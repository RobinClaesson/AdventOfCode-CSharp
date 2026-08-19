using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;
using AdventOfCode.Solutions.Navigation;

namespace AdventOfCode.Solutions.Year2016.Day17;

[AdventOfCodeSolution(2016, 17)]
public class Solution : IAdventOfCodeSolution
{
    private record State(Point Position, string Path);

    private static readonly MD5 Md5Hash = MD5.Create();
    private static readonly UTF8Encoding Utf8Encoding = new();

    public void Run(string input)
    {
        var start = new Point(0, 0);
        var target = new Point(3, 3);
        var initialState = new State(start, string.Empty);

        var paths = new HashSet<string>();
        var queue = new Queue<State>();
        queue.Enqueue(initialState);
        
        while (queue.Count > 0)
        {
            var currentState = queue.Dequeue();
            GetOpenDoors(currentState.Path)
                .Select(d => new State(currentState.Position.Step(d), currentState.Path + d.ToString()[0]))
                .Where(s => s.Position is { X: >= 0 and <= 3, Y: >= 0 and <= 3 })
                .ToList()
                .ForEach(s =>
                {
                    if (s.Position == target)
                        paths.Add(s.Path);
                    else
                        queue.Enqueue(s);
                });
        }

        Output.Answer(paths.MinBy(p => p.Length));
        Output.Answer(paths.Max(p => p.Length));
        return;

        IEnumerable<Direction> GetOpenDoors(string path)
        {
            var hash = CreateMd5Hash(path);

            if (DoorIsOpen(hash[0]))
                yield return Direction.Up;
            if (DoorIsOpen(hash[1]))
                yield return Direction.Down;
            if (DoorIsOpen(hash[2]))
                yield return Direction.Left;
            if (DoorIsOpen(hash[3]))
                yield return Direction.Right;
        }

        bool DoorIsOpen(char c) => c is >= 'b' and <= 'f';

        string CreateMd5Hash(string path) => Md5Hash.ComputeHash(Utf8Encoding.GetBytes($"{input}{path}"))
            .Select(b => b.ToString("x2"))
            .JoinToString();
    }
}