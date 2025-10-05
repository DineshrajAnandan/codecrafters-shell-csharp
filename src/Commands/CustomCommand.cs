using CodecraftersShell.Extensions;
using CodecraftersShell.Helpers;

namespace CodecraftersShell.Commands;

public interface ICustomCommand : ICommand
{
    ICustomCommand WithCommand(string command);
}

public class CustomCommand(
    IFileHelper fileHelper,
    IExecutableFileHelper executableFileHelper): ICustomCommand
{
    private string _command = string.Empty;
    
    public ICustomCommand WithCommand(string command)
    {
        _command = command;
        return this;
    }
    
    public void Handle(string arguments)
    {
        var result = fileHelper.SearchFileInPaths(_command);
        
        if(string.IsNullOrEmpty(result))
            throw new Exception($"{_command}: command not found");

        var argumentsList = arguments.SplitArguments().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        var output = executableFileHelper.ExecuteFile(_command, argumentsList);
        Console.WriteLine(output.TrimTrailingEmptyLine());
    }
}