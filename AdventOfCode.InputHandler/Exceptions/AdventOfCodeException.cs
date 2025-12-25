namespace AdventOfCode.InputHandler.Exceptions;

public abstract class AdventOfCodeException : Exception
{
    protected AdventOfCodeException() : base()
    {
    }

    protected AdventOfCodeException(string message) : base(message)
    {
    }
}