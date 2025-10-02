namespace CodecraftersShell.Helpers;

public class FileHelper
{
    public static string SearchFileInPaths(
        string paths,
        string fileName)
    {
        var sourcePaths = paths.Split(Path.PathSeparator)
            .Where(Directory.Exists)
            .ToArray();
        foreach (var sourcePath in sourcePaths)
        {
            var filePath = Path.Combine(sourcePath, fileName);
            if (!File.Exists(filePath)) 
                continue;
            
            var mode = File.GetUnixFileMode(filePath);
            if (mode.HasFlag(UnixFileMode.UserExecute))
            {
                return filePath;
            }
        }

        return null;
    }
    
    public static string SearchFileNameInPathsByPrefix(
        string paths,
        string prefix)
    {
        var sourcePaths = paths.Split(Path.PathSeparator)
            .Where(Directory.Exists)
            .ToArray();
        foreach (var sourcePath in sourcePaths)
        {
            var files = Directory.GetFiles(sourcePath, $"{prefix}.*", SearchOption.AllDirectories);
            foreach (var fileName in files)
            {
                var filePath = Path.Combine(sourcePath, fileName);
                if (!File.Exists(filePath)) 
                    continue;
            
                var mode = File.GetUnixFileMode(filePath);
                if (mode.HasFlag(UnixFileMode.UserExecute))
                {
                    return fileName;
                }
            }
            
        }

        return null;
    }
}