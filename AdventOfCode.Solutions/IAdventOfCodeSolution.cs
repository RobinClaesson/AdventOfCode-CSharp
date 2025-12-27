namespace AdventOfCode.Solutions;

/// <summary>
/// A solution for an Advent of Code puzzle
/// </summary>
public interface IAdventOfCodeSolution
{
    /// <summary>
    /// Runs the solution for the puzzle, prints the solutions to the console
    /// </summary>
    /// <param name="input">Puzzle input</param>
    public void Run(string input);
}