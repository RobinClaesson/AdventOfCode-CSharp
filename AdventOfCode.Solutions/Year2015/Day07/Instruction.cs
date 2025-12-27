namespace AdventOfCode.Solutions.Year2015.Day07;

public record Instruction
{
    public string InstructionSource { get; }
    public InstructionType InstructionType { get; }
    public string TargetWire { get; private set; }
    public List<string> SourcesWires { get; } = [];
    public ushort? Constant { get; private set; }

    public Instruction(string instruction)
    {
        InstructionSource = instruction;
        InstructionType = ParseInstructionType(instruction);

        var sections =
            instruction.Split("->", StringSplitOptions.TrimEntries);

        ParseSourcesAndConstant(sections[0]);
        TargetWire = sections[1];
    }

    public override string ToString() => InstructionSource;

    private static InstructionType ParseInstructionType(string instruction)
    {
        if (instruction.Contains("RSHIFT"))
            return InstructionType.RShift;
        if (instruction.Contains("LSHIFT"))
            return InstructionType.LShift;
        if (instruction.Contains("AND"))
            return InstructionType.And;
        if (instruction.Contains("OR"))
            return InstructionType.Or;
        if (instruction.Contains("NOT"))
            return InstructionType.Not;
        return InstructionType.Set;
    }

    private void ParseSourcesAndConstant(string instructionSection)
    {
        var words = instructionSection.Split(' ',
            StringSplitOptions.TrimEntries);
        ushort value;
        switch (InstructionType)
        {
            case InstructionType.Set:
                if (ushort.TryParse(instructionSection, out value))
                    Constant = value;
                else
                    SourcesWires.Add(instructionSection);
                break;
            case InstructionType.Not:
                SourcesWires.Add(words[1]);
                break;

            case InstructionType.RShift:
            case InstructionType.LShift:
                SourcesWires.Add(words[0]);
                Constant = ushort.Parse(words[2]);
                break;

            case InstructionType.And:
            case InstructionType.Or:
                if (ushort.TryParse(words[0], out value))
                    Constant = value;
                else
                    SourcesWires.Add(words[0]);

                if (ushort.TryParse(words[2], out value))
                    Constant = value;
                else
                    SourcesWires.Add(words[2]);
                break;
        }
    }
}

public enum InstructionType
{
    Set,
    RShift,
    LShift,
    And,
    Or,
    Not
}