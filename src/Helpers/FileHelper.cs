namespace codecraftersShell.Helpers;

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
            var files = Directory.GetFiles(sourcePath);
            Console.WriteLine(string.Join(", ",files));
            var filePath = Path.Combine(sourcePath, fileName);
            if (File.Exists(filePath))
            {
                return filePath;
            }
        }

        return null;
    }
}