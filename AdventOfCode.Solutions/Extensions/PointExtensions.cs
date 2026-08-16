using System.Drawing;

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
    }
}