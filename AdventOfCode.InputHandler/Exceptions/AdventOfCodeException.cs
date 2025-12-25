namespace AdventOfCode.InputHandler.Exceptions;

/// <summary>
/// Represents errors that occurs in connection to Advent of Code data requests
/// </summary>
/// <param name="message"></param>
/// <param name="innerException"></param>
public abstract class AdventOfCodeException(string message, Exception? innerException = null)
    : Exception(message, innerException);