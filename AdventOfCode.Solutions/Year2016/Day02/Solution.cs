using System.Drawing;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day02;

[AdventOfCodeSolution(2016, 2)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var rows = input.Rows();

        Output.Answer(GetPasscode(KeypadPart1));
        Output.Answer(GetPasscode(KeypadPart2));
        return;

        string GetPasscode(Dictionary<Point, char> keypad)
        {
            var passcode = string.Empty;
            var position = keypad.First(kv => kv.Value == '5').Key;

            foreach (var instruction in rows)
            {
                foreach (var move in instruction)
                {
                    var nextPos = position.Step(move);
                    if (keypad.ContainsKey(nextPos))
                        position = nextPos;
                }

                passcode += keypad[position];
            }

            return passcode;
        }
    }

    private static readonly Dictionary<Point, char> KeypadPart1 = new()
    {
        { new Point(0, 0), '1' }, { new Point(1, 0), '2' }, { new Point(2, 0), '3' },
        { new Point(0, 1), '4' }, { new Point(1, 1), '5' }, { new Point(2, 1), '6' },
        { new Point(0, 2), '7' }, { new Point(1, 2), '8' }, { new Point(2, 2), '9' },
    };

    private static readonly Dictionary<Point, char> KeypadPart2 = new()
    {
        { new Point(2, 0), '1' },
        { new Point(1, 1), '2' }, { new Point(2, 1), '3' }, { new Point(3, 1), '4' },
        { new Point(0, 2), '5' }, { new Point(1, 2), '6' }, { new Point(2, 2), '7' }, { new Point(3, 2), '8' }, { new Point(4, 2), '9' },
        { new Point(1, 3), 'A' }, { new Point(2, 3), 'B' }, { new Point(3, 3), 'C' },
        { new Point(2, 4), 'E' },
    };
}