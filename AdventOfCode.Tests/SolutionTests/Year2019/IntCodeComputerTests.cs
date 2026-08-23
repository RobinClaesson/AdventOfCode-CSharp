using AdventOfCode.Solutions.Year2019;

namespace AdventOfCode.Tests.SolutionTests.Year2019;

public class IntCodeComputerTests
{
    [Theory]
    [CombinatorialData]
    public void Day2_RunOutputsPositionZero(
        [CombinatorialMemberData(nameof(Day2Examples))]
        IntCodeComputerTestCase<int> testCase)
    {
        var computer = new IntCodeComputer(testCase.Program);

        var result = computer.Run();

        Assert.Equal(testCase.Expected, result);
        Assert.Equal(testCase.Expected, computer[0]);
    }

    public record IntCodeComputerTestCase<T>(List<int> Program, T Expected);

    public static List<IntCodeComputerTestCase<int>> Day2Examples() =>
    [
        new([1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50], 3500),
        new([1, 0, 0, 0, 99], 2),
        new([1, 1, 1, 4, 99, 5, 6, 0, 99], 30),
    ];
}