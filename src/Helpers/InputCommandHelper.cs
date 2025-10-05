using CodecraftersShell.Constants;

namespace CodecraftersShell.Helpers;

public interface IInputCommandHelper
{
    string ParseInputCommand(string input, out string? arguments);

    void TryAutoCompleteCommand(string prefix,
        out string remainingSubstring,
        out List<string> commands);
}
public class InputCommandHelper(IFileHelper fileHelper): IInputCommandHelper
{
    public string ParseInputCommand(string input, out string? arguments)
    {
        var inputArr = input.Split(' ', 2);
        var command = inputArr[0];
        arguments = inputArr.Length == 1 ? null : inputArr[1];
        return command;
    }
    
    public void TryAutoCompleteCommand(string prefix,
        out string remainingSubstring,
        out List<string> commands)
    {
        remainingSubstring = string.Empty;
        var builtIn = GetAllBuiltInCommandsByPrefix(prefix);
        var executables = GetAllExecutableFilesCommandByPrefix(prefix);

        commands = builtIn.Concat(executables)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
        
        if (commands.Count == 0)
            return;
        
        var firstCommand = commands[0];
        if(commands.All(c => c.StartsWith(firstCommand)))
            remainingSubstring = firstCommand.Substring(prefix.Length);
    }

    private IEnumerable<string> GetAllBuiltInCommandsByPrefix(string prefix)
    {
        return CommandsConstants.BuiltInCommands.Where(c => c.StartsWith(prefix));
    }
    
    private IEnumerable<string> GetAllExecutableFilesCommandByPrefix(string prefix)
    {
        return fileHelper.SearchFileNameInPathsByPrefix(prefix).Select(Path.GetFileName);
    }
    
}