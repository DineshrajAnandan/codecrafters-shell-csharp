using CodecraftersShell.Constants;
using CodecraftersShell.Helpers;

namespace CodecraftersShell.Commands;

public class TypeCommand: ICommand
{
    public void Handle(string arguments)
    {
        var type = arguments;
        if (CommandsConstants.BuiltInCommands.Contains(type))
        {
            Console.WriteLine($"{type} is a shell builtin");
            return;
        }

        var result = FileHelper.SearchFileInPaths(
            Environment.GetEnvironmentVariable("PATH") ??
            string.Empty,
            type);

        if (!string.IsNullOrEmpty(result))
        {
            Console.WriteLine($"{type} is {result}");
            return;
        }
        
        Console.WriteLine($"{type}: not found");
    }
}