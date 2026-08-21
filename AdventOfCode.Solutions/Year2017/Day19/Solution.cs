using System.Drawing;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;
using AdventOfCode.Solutions.Navigation;

namespace AdventOfCode.Solutions.Year2017.Day19;

[AdventOfCodeSolution(2017, 19)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var map = input.Rows();
        var position = new Point(map[0].IndexOf('|'), 0);
        var direction = Direction.Down;
        var letters = new List<char>();
        var steps = 0;

        while (map[position.Y][position.X] != ' ')
        {
            position = position.Step(direction);
            steps++;

            switch (map[position.Y][position.X])
            {
                case >= 'A' and <= 'Z':
                    letters.Add(map[position.Y][position.X]);
                    break;
                case '+':
                    var cameFrom = direction.TurnAround();
                    direction = Enum.GetValues<Direction>()
                        .First(d => d != direction && d != cameFrom && HasPath(d));
                    break;
            }
        }

        Output.Answer(letters.JoinToString());
        Output.Answer(steps);
        return;

        bool HasPath(Direction d)
        {
            var point = position.Step(d);
            return map[point.Y][point.X] is not (' ' or '+');
        }
    }
}