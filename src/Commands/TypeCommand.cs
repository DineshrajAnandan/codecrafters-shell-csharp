using CodecraftersShell.Constants;
using CodecraftersShell.Helpers;

namespace CodecraftersShell.Commands;

public interface ITypeCommand : ICommand;
public class TypeCommand(IFileHelper fileHelper): ITypeCommand
{
    public void Handle(string arguments)
    {
        var type = arguments;
        if (CommandsConstants.BuiltInCommands.Contains(type))
        {
            Console.WriteLine($"{type} is a shell builtin");
            return;
        }

        var result = fileHelper.SearchFileInPaths(type);

        if (!string.IsNullOrEmpty(result))
        {
            Console.WriteLine($"{type} is {result}");
            return;
        }
        
        Console.WriteLine($"{type}: not found");
    }
}