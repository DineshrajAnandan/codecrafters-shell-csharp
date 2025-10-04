using CodecraftersShell.Extensions;
using CodecraftersShell.Helpers;

namespace CodecraftersShell.Commands;

public interface ICustomCommand : ICommand
{
    ICustomCommand WithCommand(string command);
}

public class CustomCommand: ICustomCommand
{
    private string _command = string.Empty;
    
    public ICustomCommand WithCommand(string command)
    {
        _command = command;
        return this;
    }
    
    public void Handle(string arguments)
    {
        var result = FileHelper.SearchFileInPaths(_command);
        
        if(string.IsNullOrEmpty(result))
            throw new Exception($"{_command}: command not found");
        
        var output = ExecutableFileHelper.ExecuteFile(_command, arguments);
        Console.WriteLine(output.TrimTrailingEmptyLine());
    }
}