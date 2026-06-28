using System.Text.Json;

namespace cs2_wasd;

public static class FileHelper
{
    public static string GetSaveFilePath(string moduleDirectory)
    {
        string configsDirectory = Path.Combine(moduleDirectory, "configs");
        return Path.Combine(configsDirectory, "player_data.json");
    }

    public static void SaveDataToFile(string filePath, Dictionary<ulong, PlayerSettings> settingsMap)
    {
        try
        {
            // Ensure the 'configs' directory exists before writing to prevent crashes
            string? directoryPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(settingsMap, options);
            File.WriteAllText(filePath, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WASD ERROR] Failed to save player data: {ex.Message}");
        }
    }

    public static Dictionary<ulong, PlayerSettings> LoadDataFromFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                return new Dictionary<ulong, PlayerSettings>();
            }

            string jsonString = File.ReadAllText(filePath);
            var deserialized = JsonSerializer.Deserialize<Dictionary<ulong, PlayerSettings>>(jsonString);

            return deserialized ?? new Dictionary<ulong, PlayerSettings>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WASD ERROR] Failed to load player data: {ex.Message}");
            return new Dictionary<ulong, PlayerSettings>();
        }
    }
}