namespace AdventOfCode.Solutions.Navigation;

public record HexagonPoint(int Q, int R, int S)
{
    public HexagonPoint Step(FlatTopHexagonDirection direction) => direction switch
    {
        FlatTopHexagonDirection.North => this with { S = S + 1, R = R - 1 },
        FlatTopHexagonDirection.NorthEast => this with { Q = Q + 1, R = R - 1 },
        FlatTopHexagonDirection.SouthEast => this with { Q = Q + 1, S = S - 1 },
        FlatTopHexagonDirection.South => this with { S = S - 1, R = R + 1 },
        FlatTopHexagonDirection.SouthWest => this with { Q = Q - 1, R = R + 1 },
        FlatTopHexagonDirection.NorthWest => this with { Q = Q - 1, S = S + 1 },
        _ => this
    };

    public List<HexagonPoint> GetNeighbors() =>
    [
        this with { S = S + 1, R = R - 1 },
        this with { Q = Q + 1, R = R - 1 },
        this with { Q = Q + 1, S = S - 1 },
        this with { S = S - 1, R = R + 1 },
        this with { Q = Q - 1, R = R + 1 },
        this with { Q = Q - 1, S = S + 1 },
    ];

    public int Length => (Math.Abs(Q) + Math.Abs(R) + Math.Abs(S)) / 2;
    public int DistanceTo(HexagonPoint other) => (this - other).Length;

    public static HexagonPoint operator +(HexagonPoint a, HexagonPoint b) => new(a.Q + b.Q, a.R + b.R, a.S + b.S);
    public static HexagonPoint operator -(HexagonPoint a, HexagonPoint b) => new(a.Q - b.Q, a.R - b.R, a.S - b.S);

    public static HexagonPoint operator *(HexagonPoint point, int scalar) =>
        new(point.Q * scalar, point.R * scalar, point.S * scalar);

    public static readonly HexagonPoint Zero = new(0, 0, 0);
}