using System.Drawing;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2019.Day15;

[AdventOfCodeSolution(2019, 15)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var computer = new IntCodeComputer(input.SplitAsLong(','));

        var visited = new HashSet<Point>() { new(0, 0) };
        var commandSequences = new Queue<List<Command>>();
        Enum.GetValues<Command>().ToList().ForEach(command => commandSequences.Enqueue([command]));

        int? pathLength = null;
        while (commandSequences.Count > 0)
        {
            computer.Reset();
            var commands = commandSequences.Dequeue();

            for (var i = 0; i < commands.Count; i++)
            {
                computer.Input = (long)commands[i];
                var pauseAt = i + 1;
                computer.Run(() => computer.Outputs.Count >= pauseAt);
            }

            switch (computer.Output)
            {
                case 1:
                    var currentPosition = GetFinalPosition(commands);
                    visited.Add(currentPosition);
                    Enum.GetValues<Command>()
                        .Where(command => !visited.Contains(Step(currentPosition, command)))
                        .ToList()
                        .ForEach(command => commandSequences.Enqueue(commands.Append(command).ToList()));
                    break;
                case 2 when pathLength is null:
                    pathLength = commands.Count;
                    break;
            }
        }

        Output.Answer(pathLength);
    }

    private static Point Step(Point position, Command command) => command switch
    {
        Command.North => position with { Y = position.Y - 1 },
        Command.South => position with { Y = position.Y + 1 },
        Command.West => position with { X = position.X - 1 },
        Command.East => position with { X = position.X + 1 },
        _ => position
    };

    private static Point GetFinalPosition(List<Command> commands) => new(
        commands.Count(c => c == Command.East) - commands.Count(c => c == Command.West),
        commands.Count(c => c == Command.South) - commands.Count(c => c == Command.North)
    );

    private enum Command
    {
        North = 1,
        South = 2,
        West = 3,
        East = 4,
    }
}