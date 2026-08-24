using System.Drawing;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2019.Day13;

[AdventOfCodeSolution(2019, 13)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var computer = new IntCodeComputer(input.SplitAsLong(','));
        computer.Run();

        var tiles = GetTiles();
        Output.Answer(tiles.Count(t => t.Type == TileType.Block));

        computer.Reset();
        computer[0] = 2;

        var score = 0L;
        var paddlePos = tiles.First(t => t.Type == TileType.HorizontalPaddle).Pos;
        var ballPos = tiles.First(t => t.Type == TileType.Ball).Pos;
        while (!computer.Halted)
        {
            computer.Run(RelevantInfoUpdated);
            tiles = GetTiles();
            computer.Outputs.Clear();

            if (tiles.FirstOrDefault(t => t.IsScoreInfo) is { } scoreTile)
                score = scoreTile.PlayerScore;

            if (tiles.FirstOrDefault(t => t is { Type: TileType.Ball }) is { } ballTile)
                ballPos = ballTile.Pos;

            if (tiles.FirstOrDefault(t => t is { Type: TileType.HorizontalPaddle }) is { } paddleTile)
                paddlePos = paddleTile.Pos;

            computer.Input = Math.Clamp(ballPos.X - paddlePos.X, -1, 1);

            continue;

            bool RelevantInfoUpdated() =>
                computer.Outputs.Count % 3 == 0 &&
                GetTiles().Any(t => t.Type is TileType.HorizontalPaddle or TileType.Ball || t.IsScoreInfo);
        }

        Output.Answer(score);
        return;

        List<Tile> GetTiles() => computer.Outputs
            .Chunk(3).Select(chunk => new Tile(chunk))
            .ToList();
    }

    private record Tile(Point Pos, TileType Type, long PlayerScore)
    {
        public Tile(long[] chunk) : this(new Point((int)chunk[0], (int)chunk[1]), (TileType)chunk[2], chunk[2])
        {
        }

        public bool IsScoreInfo => Pos is { X: -1, Y: -0 };
    };

    private enum TileType
    {
        Empty = 0,
        Wall = 1,
        Block = 2,
        HorizontalPaddle = 3,
        Ball = 4
    }
}