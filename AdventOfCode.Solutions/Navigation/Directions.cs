namespace AdventOfCode.Solutions.Navigation;

public enum Direction
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}

public enum FlatTopHexagonDirection
{
    North = 0,
    NorthEast = 1,
    SouthEast = 2,
    South = 3,
    SouthWest = 4,
    NorthWest = 5,
}

public static class DirectionsExtensions
{
    extension(Direction direction)
    {
        public Direction TurnRight() => (Direction)(((int)direction + 1) % 4);
        public Direction TurnLeft() => (Direction)(((int)direction + 3) % 4);
    }

    extension(FlatTopHexagonDirection direction)
    {
        public FlatTopHexagonDirection TurnRight() => (FlatTopHexagonDirection)(((int)direction + 1) % 6);
        public FlatTopHexagonDirection TurnLeft() => (FlatTopHexagonDirection)(((int)direction + 5) % 6);
    }
}