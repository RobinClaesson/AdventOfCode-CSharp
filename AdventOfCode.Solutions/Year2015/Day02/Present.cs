namespace AdventOfCode.Solutions.Year2015.Day02;

internal record Present(int L, int W, int H)
{
    private int[] Lengths { get; } = [L, W, H];
    private int[] Sides { get; } = [L * H, W * H, L * W];
    public int CalcPaper() => 2 * Sides[0] + 2 * Sides[1] + 2 * Sides[2] + Sides.Min();
    public int CalcRibbon() => 2 * L + 2 * W + 2 * H - 2 * Lengths.Max() + L * W * H;
};