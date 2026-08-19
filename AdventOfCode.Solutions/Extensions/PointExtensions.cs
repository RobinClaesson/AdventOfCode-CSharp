using System.Drawing;
using AdventOfCode.Solutions.Navigation;

namespace AdventOfCode.Solutions.Extensions;

public static class PointExtensions
{
    extension(Point point)
    {
        public List<Point> GetNeighbors() =>
        [
            new(point.X, point.Y - 1),
            new(point.X + 1, point.Y - 1),
            new(point.X + 1, point.Y),
            new(point.X + 1, point.Y + 1),
            new(point.X, point.Y + 1),
            new(point.X - 1, point.Y + 1),
            new(point.X - 1, point.Y),
            new(point.X - 1, point.Y - 1),
        ];

        public List<Point> GetManhattanNeighbors() =>
        [
            new(point.X, point.Y - 1),
            new(point.X + 1, point.Y),
            new(point.X, point.Y + 1),
            new(point.X - 1, point.Y),
        ];

        public Point Step(Direction direction, int steps = 1) => direction switch
        {
            Direction.Up => point with { Y = point.Y - steps },
            Direction.Right => point with { X = point.X + steps },
            Direction.Down => point with { Y = point.Y + steps },
            Direction.Left => point with { X = point.X - steps },
            _ => point
        };

        public Point Step(char direction, int steps = 1) => direction switch
        {
            'U' or 'u' => point with { Y = point.Y - steps },
            'R' or 'r' => point with { X = point.X + steps },
            'D' or 'd' => point with { Y = point.Y + steps },
            'L' or 'l' => point with { X = point.X - steps },
            _ => point
        };
    }
}