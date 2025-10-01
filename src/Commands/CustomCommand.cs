using codecraftersShell.Helpers;

namespace codecrafterdShell.Commands;

public class CustomCommand: ICommand
{
    private readonly string _command;
    
    public CustomCommand(string command)
    {
        _command = command;
    }
    
    public void Handle(string arguments)
    {
        var result = FileHelper.SearchFileInPaths(
            Environment.GetEnvironmentVariable("PATH") ??
            string.Empty,
            _command);
        
        if(string.IsNullOrEmpty(result))
            throw new Exception($"{_command} {arguments}: command not found");
        
        var output = ExecutableFileHelper.ExecuteFile(_command, arguments);
        // Console.WriteLine(output);
    }
}