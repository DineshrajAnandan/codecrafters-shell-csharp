using CodecraftersShell.Extensions;
using CodecraftersShell.Helpers;

namespace CodecraftersShell.Commands;

public class CustomCommand: ICommand
{
    private readonly string _command;
    
    public CustomCommand(string command)
    {
        _command = command;
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