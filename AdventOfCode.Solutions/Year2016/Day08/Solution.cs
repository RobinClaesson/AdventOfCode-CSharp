using System.Drawing;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day08;

[AdventOfCodeSolution(2016, 8)]
public class Solution : IAdventOfCodeSolution
{
    public const int MapHeight = 6;
    public const int MapWidth = 50;

    private enum Direction
    {
        Row,
        Column
    }

    private enum Instruction
    {
        Rect,
        Rotate
    }

    public void Run(string input)
    {
        var screen = new bool[MapHeight, MapWidth];

        foreach (var row in input.RowsSplitted(' '))
        {
            var instruction = Enum.Parse<Instruction>(row[0], true);
            switch (instruction)
            {
                default:
                case Instruction.Rect:
                    var dimensions = row[1].Split('x').Select(int.Parse).ToList();
                    Enumerable.Range(0, dimensions[0]).ToList().ForEach(x =>
                        Enumerable.Range(0, dimensions[1]).ToList().ForEach(y =>
                            screen[y, x] = true));
                    break;
                case Instruction.Rotate:
                    var direction = Enum.Parse<Direction>(row[1], true);
                    var index = int.Parse(row[2][2..]);
                    var steps = int.Parse(row.Last());

                    var pixels = direction switch
                    {
                        Direction.Column => Enumerable.Range(0, MapHeight).Select(y => screen[y, index]).ToList(),
                        _ => Enumerable.Range(0, MapWidth).Select(x => screen[index, x]).ToList()
                    };

                    Rotate(pixels, steps);

                    switch (direction)
                    {
                        case Direction.Column:
                            Enumerable.Range(0, MapHeight).ToList().ForEach(y => screen[y, index] = pixels[y]);
                            break;
                        default:
                        case Direction.Row:
                            Enumerable.Range(0, MapWidth).ToList().ForEach(x => screen[index, x] = pixels[x]);
                            break;
                    }
                    break;
            }
        }

        var litPixelCount = (from bool pixel in screen select pixel ? 1 : 0).Sum();
        Output.Answer(litPixelCount);
        Output.Answer(string.Empty);
        PrintScreen(onChar: '■', offChar: ' ');
        return;

        void PrintScreen(char onChar = '#', char offChar = '.')
        {
            for (var y = 0; y < MapHeight; y++)
            {
                for (var x = 0; x < MapWidth; x++)
                {
                    Console.Write(screen[y, x] ? onChar : offChar);
                }

                Console.WriteLine();
            }
        }
    }

    private static void Rotate(List<bool> pixels, int steps)
    {
        for (var i = 0; i < steps; i++)
        {
            pixels.Insert(0, pixels.Last());
            pixels.RemoveAt(pixels.Count - 1);
        }
    }
}