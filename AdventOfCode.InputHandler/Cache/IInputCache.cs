namespace AdventOfCode.InputHandler.Cache;

/// <summary>
/// Handles caching for Advent of Code puzzle inputs
/// </summary>
public interface IInputCache
{
    /// <summary>
    /// Checks if cache contains input for the given puzzle
    /// </summary>
    /// <param name="year">Year of puzzle</param>
    /// <param name="day">Day of puzzle</param>
    /// <returns><c>true</c> if puzzle input is cached, <c>false</c> otherwise.</returns>
    public bool HasInput(int year, int day);

    /// <summary>
    /// Gets the cached input for the given puzzle
    /// </summary>
    /// <param name="year">Year of puzzle</param>
    /// <param name="day">Day of puzzle</param>
    /// <returns>The full puzzle input</returns>
    public string GetInput(int year, int day);

    /// <summary>
    /// Saves the input for the given puzzle to the cache. Overwrites existing input.
    /// </summary>
    /// <param name="year">Year of puzzle</param>
    /// <param name="day">Day of puzzle</param>
    /// <param name="input">Input for the puzzle</param>
    public void CacheInput(int year, int day, string input);
}