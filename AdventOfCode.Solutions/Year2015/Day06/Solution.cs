using System.Drawing;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day06;

[AdventOfCodeSolution(2015, 6)]
public class Solution : IAdventOfCodeSolution
{
    private const int Size = 1000;

    public void Run(string input)
    {
        var inputRows = input.RowsSplitted(' ');
        
        var lights = new bool[Size, Size];
        foreach (var words in inputRows)
        {
            if (words[0] == "turn")
            {
                var area = GetArea(words, 2, 4);
                Set(area, words[1] == "on", lights);
            }
            else
            {
                var area = GetArea(words, 1, 3);
                Toggle(area, lights);
            }
        }

        var turnedOnLights =
            from bool light in lights
            where light
            select light;

        Output.Answer(turnedOnLights.Count());

        var dimmed = new int[Size, Size];
        foreach (var words in inputRows)
        {
            if (words[0] == "turn")
            {
                var area = GetArea(words, 2, 4);
                Dimmer(area, words[1] == "on" ? 1 : -1, dimmed);
            }
            else
            {
                var area = GetArea(words, 1, 3);
                Dimmer(area, 2, dimmed);
            }
        }
        
        var lightsbrightness = 
            from int light in dimmed
            select light;

        Output.Answer(lightsbrightness.Sum());
    }

    private record Area(Point Start, Point End);

    private static Area GetArea(string[] words, int startIndex, int endIndex)
    {
        var startValues = words[startIndex].Split(',').Select(int.Parse).ToArray();
        var endValues = words[endIndex].Split(',').Select(int.Parse).ToArray();
        return new Area(new Point(startValues[0], startValues[1]), new Point(endValues[0], endValues[1]));
    }

    private static void Set(Area area, bool value, bool[,] lights)
    {
        for (var x = area.Start.X; x <= area.End.X; x++)
        for (var y = area.Start.Y; y <= area.End.Y; y++)
            lights[x, y] = value;
    }

    private static void Toggle(Area area, bool[,] lights)
    {
        for (var x = area.Start.X; x <= area.End.X; x++)
        for (var y = area.Start.Y; y <= area.End.Y; y++)
            lights[x, y] = !lights[x, y];
    }

    private static void Dimmer(Area area, int value, int[,] lights)
    {
        for (var x = area.Start.X; x <= area.End.X; x++)
        for (var y = area.Start.Y; y <= area.End.Y; y++)
        {
            lights[x, y] += value;

            if (lights[x, y] < 0)
                lights[x, y] = 0;
        }
    }
}