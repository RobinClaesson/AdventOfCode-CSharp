using System.Text.Json;

namespace AdventOfCode;

public class Settings
{
    public const string SettingsFileName = "settings.json";
    
    public string InputCachePath { get; set; } = ".";
    public string SessionToken { get; set; } = string.Empty;

    public void Save() => File.WriteAllText(SettingsFileName, JsonSerializer.Serialize(this));

    public static Settings LoadSettingsFile()
    {
        if (!File.Exists(SettingsFileName))
        {
            var defaultSettings = new Settings();
            var defaultJson = JsonSerializer.Serialize(defaultSettings);
            File.WriteAllText(SettingsFileName, defaultJson);
            return defaultSettings;
        }

        var fileContent = File.ReadAllText(SettingsFileName);
        var settings = JsonSerializer.Deserialize<Settings>(fileContent);
        return settings ?? new Settings();
    }
}