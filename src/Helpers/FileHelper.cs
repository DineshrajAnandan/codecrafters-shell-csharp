namespace codecraftersShell.Helpers;

public class FileHelper
{
    public static string SearchFileInPaths(
        string paths,
        string fileName)
    {
        var sourcePaths = paths.Split(Path.PathSeparator)
            .Where(Directory.Exists)
            .Reverse()
            .ToArray();
        foreach (var sourcePath in sourcePaths)
        {
            var filePath = Path.Combine(sourcePath, fileName);
            if (File.Exists(filePath))
            {
                return filePath;
            }
        }

        return null;
    }
}