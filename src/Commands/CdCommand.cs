namespace CodecraftersShell.Commands;

public interface ICdCommand: ICommand;
public class CdCommand: ICdCommand
{
    public void Handle(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Path cannot be null or whitespace");
        }

        if (path.Equals("~"))
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }

        if (Directory.Exists(path))
        {
            Directory.SetCurrentDirectory(path);
        }
        else
        {
            Console.WriteLine($"cd: {path}: No such file or directory");
        }
    }
}