namespace data_api.Utils;
using System;
using System.IO;

public static class DotEnv
{
    public static void Load()
    {
        // 1️⃣ Check if an explicit .env path is set via environment variable
        string envFilePath = Environment.GetEnvironmentVariable("DOTENV_PATH");

        if (string.IsNullOrEmpty(envFilePath))
        {
            // 2️⃣ If not set, fall back to the parent directory of the current working directory
            string currentDirectory = Directory.GetCurrentDirectory();
            string parentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            
            if (string.IsNullOrEmpty(parentDirectory))
            {
                throw new Exception("Failed to locate the parent directory.");
            }

            envFilePath = Path.Combine(parentDirectory, ".env");
        }

        Console.WriteLine($"Resolved .env path: {envFilePath}");

        if (!File.Exists(envFilePath))
        {
            throw new FileNotFoundException($".env file not found at: {envFilePath}");
        }

        foreach (var line in File.ReadAllLines(envFilePath))
        {
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            var parts = line.Split('=', 2, StringSplitOptions.TrimEntries);
            if (parts.Length != 2)
                continue;

            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }

        Console.WriteLine("Environment variables loaded successfully.");
    }
}
