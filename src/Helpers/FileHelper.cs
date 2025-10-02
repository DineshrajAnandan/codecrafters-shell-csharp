namespace CodecraftersShell.Helpers;

public class FileHelper
{
    public static string SearchFileInPaths(string fileName)
    {
        return GetSourcePaths()
            .Select(
                sourcePath => Path.Combine(sourcePath, fileName))
            .FirstOrDefault(
                filePath => IsFileExecutable(filePath)) ?? string.Empty;
    }
    
    public static string SearchFileNameInPathsByPrefix(string prefix)
    {
        var sourcePaths = GetSourcePaths();
        foreach (var sourcePath in sourcePaths)
        {
            var executableFile = 
                GetFileNamesInDirectory(prefix, sourcePath)
                    .Select(f => Path.Combine(sourcePath, f))
                    .FirstOrDefault(f => IsFileExecutable(f));
            
            if(!string.IsNullOrEmpty(executableFile))
                return executableFile;
        }
        return null;
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