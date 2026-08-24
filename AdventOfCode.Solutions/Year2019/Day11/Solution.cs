using System.Drawing;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;
using AdventOfCode.Solutions.Navigation;

namespace AdventOfCode.Solutions.Year2019.Day11;

[AdventOfCodeSolution(2019, 11)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var computer = new IntCodeComputer(input.SplitAsLong(','));
        var direction = Direction.Up;
        var position = new Point(0, 0);
        var panels = new Dictionary<Point, Paint>();

        Output.Answer(RunPaintProgram());

        computer.Reset();
        direction = Direction.Up;
        position = new Point(0, 0);
        panels.Clear();
        panels.Add(position, Paint.White);

        RunPaintProgram();

        Output.Answer(string.Empty);
        for (var y = panels.Keys.Min(p => p.Y); y <= panels.Keys.Max(p => p.Y); y++)
        {
            for (var x = panels.Keys.Min(p => p.X); x <= panels.Keys.Max(p => p.X); x++)
            {
                Console.Write(GetPanelColor(new Point(x, y)) == Paint.White ? "■" : " ");
            }

            Console.WriteLine();
        }

        return;

        int RunPaintProgram()
        {
            var paintedPanels = new HashSet<Point>();
            while (!computer.Halted)
            {
                computer.Input = (long)GetPanelColor(position);

                var target = computer.Outputs.Count + 2;
                computer.Run(() => computer.Outputs.Count >= target);
                var outputs = computer.Outputs[^2..];

                var paintColor = (Paint)outputs[0];
                panels[position] = paintColor;

                if (paintColor == Paint.White)
                    paintedPanels.Add(position);

                direction = outputs[1] == 0 ? direction.TurnLeft() : direction.TurnRight();
                position = position.Step(direction);
            }

            return paintedPanels.Count;
        }

        Paint GetPanelColor(Point point) => panels.GetValueOrDefault(point, Paint.Black);
    }

    private enum Paint
    {
        Black = 0,
        White = 1
    }
}