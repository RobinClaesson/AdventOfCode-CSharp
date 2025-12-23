namespace AdventOfCode.InputHandler.Cache;

public class FileInputCache(string directoryPath) : IInputCache
{
    public string DirectoryPath { get; } = directoryPath;

    public bool HasInput(int year, int day) => File.Exists(FilePath(year, day));

    public string GetInput(int year, int day) => File.ReadAllText(FilePath(year, day));
    public Task<string> GetInputAsync(int year, int day) => File.ReadAllTextAsync(FilePath(year, day));

    public void CacheInput(int year, int day, string input) => File.WriteAllText(FilePath(year, day), input);
    public Task CacheInputAsync(int year, int day, string input) => File.WriteAllTextAsync(FilePath(year, day), input);

    public string FilePath(int year, int day) => Path.Combine(DirectoryPath, $"{year}-{day,2}.txt");
}