namespace CodecraftersShell.Commands;

public class CdCommand: ICommand
{
    public void Handle(string arguments)
    {
        if (string.IsNullOrWhiteSpace(arguments))
        {
            throw new ArgumentException("Arguments cannot be null or whitespace");
        }

        if (Directory.Exists(arguments))
        {
            Directory.SetCurrentDirectory(arguments);
        }
        else
        {
            Console.WriteLine($"cd: {arguments}: No such file or directory");
        }
    }
}