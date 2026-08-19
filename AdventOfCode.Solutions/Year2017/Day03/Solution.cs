using System.Drawing;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;
using AdventOfCode.Solutions.Navigation;

namespace AdventOfCode.Solutions.Year2017.Day03;

[AdventOfCodeSolution(2017, 3)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var number = int.Parse(input);

        var sideLength = 3;
        var squares = InitialIncremental();
        var position = new Point(2, 1);
        var index = 10;
        while (squares.All(s => s.Value != number))
        {
            FillIncremental(Direction.Up, sideLength);
            FillIncremental(Direction.Left, ++sideLength);
            FillIncremental(Direction.Down, sideLength);
            FillIncremental(Direction.Right, ++sideLength);
        }

        var target = squares.First(s => s.Value == number).Key;
        Output.Answer(Math.Abs(target.X) + Math.Abs(target.Y));

        sideLength = 3;
        squares = InitialSummed();
        position = new Point(2, 1);
        while (squares.All(s => s.Value < number))
        {
            FillSummed(Direction.Up, sideLength);
            FillSummed(Direction.Left, ++sideLength);
            FillSummed(Direction.Down, sideLength);
            FillSummed(Direction.Right, ++sideLength);
        }
        
        Output.Answer(squares.First(s => s.Value > number).Value);
        return;

        void FillIncremental(Direction direction, int steps)
        {
            for (var i = 0; i < steps; i++)
            {
                position = position.Step(direction);
                squares[position] = ++index;
            }
        }

        void FillSummed(Direction direction, int steps)
        {
            for (var i = 0; i < steps; i++)
            {
                position = position.Step(direction);
                squares[position] = position.GetNeighbors()
                    .Sum(p => squares.GetValueOrDefault(p, 0));
            }
        }
    }

    private static Dictionary<Point, int> InitialIncremental() => new()
    {
        [new Point(-1, -1)] = 5, [new Point(0, -1)] = 4, [new Point(1, -1)] = 3,
        [new Point(-1, 0)] = 6, [new Point(0, 0)] = 1, [new Point(1, 0)] = 2,
        [new Point(-1, 1)] = 7, [new Point(0, 1)] = 8, [new Point(1, 1)] = 9, [new Point(2, 1)] = 10
    };

    private static Dictionary<Point, int> InitialSummed() => new()
    {
        [new Point(-1, -1)] = 5, [new Point(0, -1)] = 4, [new Point(1, -1)] = 2,
        [new Point(-1, 0)] = 10, [new Point(0, 0)] = 1, [new Point(1, 0)] = 1,
        [new Point(-1, 1)] = 11, [new Point(0, 1)] = 23, [new Point(1, 1)] = 25, [new Point(2, 1)] = 26
    };
}