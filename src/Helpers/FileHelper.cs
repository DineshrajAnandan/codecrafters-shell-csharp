namespace CodecraftersShell.Helpers;

public interface IFileHelper
{
    string ReadAllText(string filePath);
    void WriteAllText(string filePath, string data);
    void AppendAllText(string filePath, string data);
    string SearchFileInPaths(string fileName);
    IEnumerable<string> SearchFileNameInPathsByPrefix(string prefix);
}
public class FileHelper: IFileHelper
{
    public string ReadAllText(string filePath)
    {
        return File.ReadAllText(filePath);
    }
    
    public void WriteAllText(string filePath, string data)
    {
        File.WriteAllText(filePath, data);
    }
    
    public void AppendAllText(string filePath, string data)
    {
        File.AppendAllText(filePath, data);
    }
    
    public string SearchFileInPaths(string fileName)
    {
        return GetSourcePaths()
            .Select(
                sourcePath => Path.Combine(sourcePath, fileName))
            .FirstOrDefault(
                filePath => IsFileExecutable(filePath)) ?? string.Empty;
    }
    
    public IEnumerable<string> SearchFileNameInPathsByPrefix(string prefix)
    {
        return GetSourcePaths()
                .SelectMany(sourcePath => 
                    GetFileNamesInDirectory(prefix, sourcePath)
                        .Select(f => Path.Combine(sourcePath, f))
                        .Where(f => IsFileExecutable(f))
                );
    }

    private IEnumerable<string?> GetFileNamesInDirectory(string prefix, string directoryPath)
    {
        return Directory.GetFiles(directoryPath)
            .Select(Path.GetFileName)
            .Where(f => f.StartsWith(prefix));
    }

    private bool IsFileExecutable(string filePath)
    {
        if (!File.Exists(filePath)) 
            return false;
            
        var mode = File.GetUnixFileMode(filePath);
        return mode.HasFlag(UnixFileMode.UserExecute);
    }

    private IEnumerable<string> GetSourcePaths()
    {
        return Environment.GetEnvironmentVariable("PATH")
            ?.Split(Path.PathSeparator)
            .Where(Directory.Exists) ?? Enumerable.Empty<string>();
    }
}