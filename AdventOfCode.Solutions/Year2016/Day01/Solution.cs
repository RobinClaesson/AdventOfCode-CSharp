using System.Drawing;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;
using AdventOfCode.Solutions.Navigation;

namespace AdventOfCode.Solutions.Year2016.Day01;

[AdventOfCodeSolution(2016, 1)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var instructions = input.Split(", ");

        var position = new Point(0, 0);
        var direction = Direction.Up;

        var visited = new HashSet<Point> { position };
        Point? firstVisitedTwice = null;
        foreach (var instruction in instructions)
        {
            direction = instruction[0] == 'R' ? direction.TurnRight() : direction.TurnLeft();
            
            var steps = int.Parse(instruction[1..]);
            for (var i = 0; i < steps; i++)
            {
                position = position.Step(direction);

                if (firstVisitedTwice is not null)
                    continue;

                if (visited.Contains(position))
                {
                    firstVisitedTwice = position;
                }

                visited.Add(position);
            }
        }

        Output.Answer(Math.Abs(position.X) + Math.Abs(position.Y));
        Output.Answer(Math.Abs(firstVisitedTwice!.Value.X) + Math.Abs(firstVisitedTwice!.Value.Y));
    }
}