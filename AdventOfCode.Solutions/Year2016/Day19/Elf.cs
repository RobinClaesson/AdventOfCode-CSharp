namespace AdventOfCode.Solutions.Year2016.Day19;

internal class Elf
{
    public Elf(int number)
    {
        Number = number;
        Next = this;
        Previous = this;
    }

    public int Number { get; }
    public Elf Next { get; set; }
    public Elf Previous { get; set; }

    public void Remove()
    {
        var previous = Previous;
        var next = Next;

        next.Previous = previous;
        previous.Next = next;
    }

    public override string ToString()
    {
        return $"Prev: {Previous.Number,2} | This: {Number,2} | Next: {Next.Number,2}";
    }
}