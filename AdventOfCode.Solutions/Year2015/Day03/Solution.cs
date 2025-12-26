using System.Drawing;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day03;

[AdventOfCodeSolution(2015, 3)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var pos = new Point(0, 0);
        var houses = new HashSet<Point> { pos };

        foreach (var c in input)
        {
            switch (c)
            {
                case '<':
                    pos.X--;
                    break;
                case '>':
                    pos.X++;
                    break;
                case '^':
                    pos.Y--;
                    break;
                case 'v':
                    pos.Y++;
                    break;
            }

            houses.Add(pos);
        }

        Output.Answer(houses.Count);

        var positions = new[] { new Point(0, 0), new Point(0, 0) };
        houses.Clear();
        houses.Add(positions[0]);

        for (var i = 0; i < input.Length; i++)
        {
            switch (input[i])
            {
                case '<':
                    positions[i % 2].X--;
                    break;
                case '>':
                    positions[i % 2].X++;
                    break;
                case '^':
                    positions[i % 2].Y--;
                    break;
                case 'v':
                    positions[i % 2].Y++;
                    break;
            }

            houses.Add(positions[i % 2]);
        }

        Output.Answer(houses.Count);
    }
}