namespace data_api.Utils;
using System;
using System.IO;

public static class DotEnv
{
    public static void Load(string filePath)
    {
        //Get the parent directory
         string currentDirectory = Directory.GetCurrentDirectory();
         string parentDirectory = Directory.GetParent(currentDirectory)?.FullName;

        if (string.IsNullOrEmpty(parentDirectory))
        {
            throw new Exception("Failed to locate the parent directory.");
        }

         string envFilePath = Path.Combine(parentDirectory, ".env");

        Console.WriteLine($"Resolved .env path: {envFilePath}");

        if (!File.Exists(envFilePath))
        {
            throw new FileNotFoundException(".env file not found at: " + envFilePath);
        }
        string envContent = File.ReadAllText(envFilePath);
        Console.WriteLine("Contents of .env file:");
        Console.WriteLine(envContent);

        foreach (var line in File.ReadAllLines(envFilePath))
        {
            Console.WriteLine("variable: " + line);
        }
        

        // Console.WriteLine("loading variables at " + filePath);
        // if (!File.Exists(filePath)){
        //      Console.WriteLine("file doesn't exist at " + filePath);
        //     return;

        // }
            
        foreach (var line in File.ReadAllLines(envFilePath))
        {
            var parts = line.Split(
                '=',
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
                continue;

            Console.WriteLine("key: "+parts[0], "value: "+parts[1]);
            Environment.SetEnvironmentVariable(parts[0].Trim(), parts[1].Trim());
        }
    }
}