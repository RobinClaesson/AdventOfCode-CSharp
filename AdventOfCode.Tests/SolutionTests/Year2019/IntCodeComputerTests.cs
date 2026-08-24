using AdventOfCode.Solutions.Year2019;

namespace AdventOfCode.Tests.SolutionTests.Year2019;

public class IntCodeComputerTests
{
    public static IEnumerable<object[]> Day2ArithmeticExamples =>
    [
        [new List<int> { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 }, 3500],
        [new List<int> { 1, 0, 0, 0, 99 }, 2],
        [new List<int> { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, 30]
    ];

    [Theory]
    [MemberData(nameof(Day2ArithmeticExamples))]
    public void Day2_ArithmeticOperations(List<int> program, int expected)
    {
        var computer = new IntCodeComputer(program);

        var result = computer.Run();

        Assert.Equal(expected, result);
        Assert.Equal(expected, computer[0]);
    }

    [Fact]
    public void Day5_OutputsInputValue()
    {
        var input = Random.Shared.Next(-100, 100);
        var computer = new IntCodeComputer([3, 0, 4, 0, 99])
        {
            Input = input
        };

        computer.Run();

        Assert.Equal(input, computer.Output);
    }

    public static IEnumerable<object[]> Day5ArithmeticExamples =>
    [
        [new List<int> { 1002, 4, 3, 4, 33 }, 99],
        [new List<int> { 1101, 100, -1, 4, 0 }, 99],
    ];

    [Theory]
    [MemberData(nameof(Day5ArithmeticExamples))]
    public void Day5_ArithmeticOperations(List<int> program, int expected)
    {
        var computer = new IntCodeComputer(program);

        computer.Run();

        Assert.Equal(expected, computer[4]);
    }

    [Theory]
    [InlineData(8, 1)]
    [InlineData(7, 0)]
    public void Day5_PositionMode_InputEqualsEight(int input, int expected)
    {
        var computer = new IntCodeComputer([3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8])
        {
            Input = input
        };

        computer.Run();

        Assert.Equal(expected, computer.Output);
    }

    [Theory]
    [InlineData(8, 0)]
    [InlineData(7, 1)]
    public void Day5_PositionMode_InputLessThanEight(int input, int expected)
    {
        var computer = new IntCodeComputer([3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8])
        {
            Input = input
        };

        computer.Run();

        Assert.Equal(expected, computer.Output);
    }

    [Theory]
    [InlineData(8, 1)]
    [InlineData(7, 0)]
    public void Day5_ImmediateMode_InputEqualsEight(int input, int expected)
    {
        var computer = new IntCodeComputer([3, 3, 1108, -1, 8, 3, 4, 3, 99])
        {
            Input = input
        };

        computer.Run();

        Assert.Equal(expected, computer.Output);
    }

    [Theory]
    [InlineData(8, 0)]
    [InlineData(7, 1)]
    public void Day5_ImmediateMode_InputLessThanEight(int input, int expected)
    {
        var computer = new IntCodeComputer([3, 3, 1107, -1, 8, 3, 4, 3, 99])
        {
            Input = input
        };

        computer.Run();

        Assert.Equal(expected, computer.Output);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(-1, 1)]
    public void Day5_PositionMode_InputIsNonZero(int input, int expected)
    {
        var computer = new IntCodeComputer([3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9])
        {
            Input = input
        };

        computer.Run();

        Assert.Equal(expected, computer.Output);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(-1, 1)]
    public void Day5_ImmediateMode_InputIsNonZero(int input, int expected)
    {
        var computer = new IntCodeComputer([3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1])
        {
            Input = input
        };

        computer.Run();

        Assert.Equal(expected, computer.Output);
    }

    [Theory]
    [InlineData(7, 999)]
    [InlineData(8, 1000)]
    [InlineData(9, 1001)]
    public void Day5_LessThan_EqualTo_OrGreaterThanEight(int input, int expected)
    {
        var computer = new IntCodeComputer([
            3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31,
            1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104,
            999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99
        ])
        {
            Input = input
        };

        computer.Run();

        Assert.Equal(expected, computer.Output);
    }

    [Fact]
    public void Day9_AdjustRelativeBase_OutputsAddress1985()
    {
        var computer = new IntCodeComputer([109, 19, 204, -34, 99])
        {
            RelativeBase = 2000,
            [1985] = 418
        };

        computer.Run();

        Assert.Equal(418, computer.Output);
    }

    [Fact]
    public void Day9_OutputCopyOfSelf()
    {
        var program = new List<long> { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 };
        var computer = new IntCodeComputer(program);

        computer.Run();

        Assert.Equivalent(program, computer.Outputs);
    }

    [Fact]
    public void Day9_Output16DigitNumber()
    {
        var computer = new IntCodeComputer([1102, 34915192, 34915192, 7, 4, 7, 99, 0]);

        computer.Run();

        var outputString = computer.Output.ToString();
        Assert.Equal(16, outputString.Length);
    }

    [Fact]
    public void Day9_OutputLargeNumber()
    {
        var computer = new IntCodeComputer([104, 1125899906842624, 99]);

        computer.Run();

        Assert.Equal(1125899906842624, computer.Output);
    }

    public static IEnumerable<object[]> InstructionParsingData =>
    [
        [
            1, new IntCodeInstruction(1, Opcode.Add,
                [ParameterMode.Position, ParameterMode.Position, ParameterMode.Position])
        ],
        [
            2, new IntCodeInstruction(2, Opcode.Multiply,
                [ParameterMode.Position, ParameterMode.Position, ParameterMode.Position])
        ],
        [
            99, new IntCodeInstruction(99, Opcode.Halt,
                [ParameterMode.Position, ParameterMode.Position, ParameterMode.Position])
        ],
        [
            1002, new IntCodeInstruction(1002, Opcode.Multiply,
                [ParameterMode.Position, ParameterMode.Immediate, ParameterMode.Position])
        ],
        [
            102, new IntCodeInstruction(102, Opcode.Multiply,
                [ParameterMode.Immediate, ParameterMode.Position, ParameterMode.Position])
        ],
        [
            10002, new IntCodeInstruction(10002, Opcode.Multiply,
                [ParameterMode.Position, ParameterMode.Position, ParameterMode.Immediate])
        ],
        [
            11101, new IntCodeInstruction(11101, Opcode.Add,
                [ParameterMode.Immediate, ParameterMode.Immediate, ParameterMode.Immediate])
        ],
        [
            202, new IntCodeInstruction(202, Opcode.Multiply,
                [ParameterMode.Relative, ParameterMode.Position, ParameterMode.Position])
        ],
        [
            2102, new IntCodeInstruction(2102, Opcode.Multiply,
                [ParameterMode.Immediate, ParameterMode.Relative, ParameterMode.Position])
        ],
        [
            21002, new IntCodeInstruction(21002, Opcode.Multiply,
                [ParameterMode.Position, ParameterMode.Immediate, ParameterMode.Relative])
        ],
    ];

    [Theory]
    [MemberData(nameof(InstructionParsingData))]
    public void GetCurrentInstruction_ParsesInstruction(int instruction, IntCodeInstruction expected)
    {
        var computer = new IntCodeComputer([instruction]);

        var result = computer.GetCurrentInstruction();

        Assert.Equivalent(expected, result);
    }

    [Fact]
    public void Run_PauseCondition()
    {
        var computer = new IntCodeComputer([104, 1, 104, 2, 104, 3, 104, 4, 99]);

        computer.Run(() => computer.Outputs.Count == 1);
        Assert.Single(computer.Outputs);

        computer.Run(() => computer.Outputs.Count == 3);
        Assert.Equal(3, computer.Outputs.Count);

        computer.Run();
        Assert.Equal(4, computer.Outputs.Count);
    }
}