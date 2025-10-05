namespace CodecraftersShell.Helpers;

public class FileHelper
{
    public static string ReadAllText(string filePath)
    {
        return File.ReadAllText(filePath);
    }
    
    public static void WriteAllText(string filePath, string data)
    {
        File.WriteAllText(filePath, data);
    }
    
    public static string SearchFileInPaths(string fileName)
    {
        return GetSourcePaths()
            .Select(
                sourcePath => Path.Combine(sourcePath, fileName))
            .FirstOrDefault(
                filePath => IsFileExecutable(filePath)) ?? string.Empty;
    }
    
    public static IEnumerable<string> SearchFileNameInPathsByPrefix(string prefix)
    {
        return GetSourcePaths()
                .SelectMany(sourcePath => 
                    GetFileNamesInDirectory(prefix, sourcePath)
                        .Select(f => Path.Combine(sourcePath, f))
                        .Where(f => IsFileExecutable(f))
                );
    }

    private static IEnumerable<string?> GetFileNamesInDirectory(string prefix, string directoryPath)
    {
        return Directory.GetFiles(directoryPath)
            .Select(Path.GetFileName)
            .Where(f => f.StartsWith(prefix));
    }

    private static bool IsFileExecutable(string filePath)
    {
        if (!File.Exists(filePath)) 
            return false;
            
        var mode = File.GetUnixFileMode(filePath);
        return mode.HasFlag(UnixFileMode.UserExecute);
    }

    private static IEnumerable<string> GetSourcePaths()
    {
        return Environment.GetEnvironmentVariable("PATH")
            ?.Split(Path.PathSeparator)
            .Where(Directory.Exists) ?? Enumerable.Empty<string>();
    }
}