namespace AdventOfCode.InputHandler.Cache;

/// <summary>
/// Caching for Advent of Code puzzle inputs in text files in directory
/// </summary>
/// <param name="directoryPath">Path to cache directory</param>
public class FileInputCache(string directoryPath) : IInputCache
{
    /// <summary>
    /// Path to cache directory
    /// </summary>
    public string DirectoryPath { get; } = directoryPath;

    /// <inheritdoc/>
    public bool HasInput(int year, int day) => File.Exists(FilePath(year, day));

    /// <inheritdoc/>
    public string GetInput(int year, int day) => File.ReadAllText(FilePath(year, day));

    /// <inheritdoc/>
    public Task<string> GetInputAsync(int year, int day) => File.ReadAllTextAsync(FilePath(year, day));

    /// <inheritdoc/>
    public void CacheInput(int year, int day, string input) => File.WriteAllText(FilePath(year, day), input);

    /// <inheritdoc/>
    public Task CacheInputAsync(int year, int day, string input) => File.WriteAllTextAsync(FilePath(year, day), input);

    /// <summary>
    /// Get file path to cached input for the given puzzle
    /// </summary>
    /// <param name="year">Year of puzzle</param>
    /// <param name="day">Day of puzzle</param>
    /// <returns>File path to text file with puzzle input</returns>
    public string FilePath(int year, int day) => Path.Combine(DirectoryPath, $"{year}-{day:D2}.txt");
}