namespace AdventOfCode.Solutions.Navigation;

public enum Direction
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}

public static class DirectionExtensions
{
    extension(Direction direction)
    {
        public Direction TurnRight() => (Direction)(((int)direction + 1) % 4);
        public Direction TurnLeft() => (Direction)(((int)direction + 3) % 4);
    }
}