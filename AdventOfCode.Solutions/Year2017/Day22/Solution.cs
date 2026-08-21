using System.Drawing;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;
using AdventOfCode.Solutions.Navigation;

namespace AdventOfCode.Solutions.Year2017.Day22;

[AdventOfCodeSolution(2017, 22)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        Output.Answer(GetCausedInfections(10000, ToggleInfected));
        Output.Answer(GetCausedInfections(10000000, WeakenAndFlag));
        return;

        int GetCausedInfections(int bursts, Func<State, State> stateUpdate)
        {
            var rows = input.Rows();
            var states = rows
                .SelectMany((r, y) => r.Select((c, x) => new { C = c, P = new Point(x, y) }))
                .Where(x => x.C == '#')
                .Select(x => x.P)
                .ToDictionary(p => p, _ => State.Infected);

            var position = new Point(rows[0].Length / 2, rows.Count / 2);
            var direction = Direction.Up;
            var causedInfections = 0;

            for (var i = 0; i < bursts; i++)
            {
                var state = states.GetValueOrDefault(position, State.Clean);

                direction = state switch
                {
                    State.Clean => direction.TurnLeft(),
                    State.Infected => direction.TurnRight(),
                    State.Flagged => direction.TurnAround(),
                    _ => direction
                };

                var updatedState = stateUpdate(state);
                states[position] = updatedState;

                if (updatedState == State.Infected)
                    causedInfections++;

                position = position.Step(direction);
            }

            return causedInfections;
        }
    }

    private static State ToggleInfected(State state) => state == State.Clean ? State.Infected : State.Clean;
    private static State WeakenAndFlag(State state) => (State)(((int)state + 1) % 4);

    private enum State
    {
        Clean,
        Weakened,
        Infected,
        Flagged
    }
}