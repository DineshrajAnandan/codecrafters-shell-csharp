using CodecraftersShell.Constants;

namespace CodecraftersShell.Helpers;

public class InputCommandHelper
{
    public static string ParseInputCommand(string input, out string? arguments)
    {
        var inputArr = input.Split(' ', 2);
        var command = inputArr[0];
        arguments = inputArr.Length == 1 ? null : inputArr[1];
        return command;
    }
    
    public static void TryAutoCompleteCommand(string prefix,
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

    private static IEnumerable<string> GetAllBuiltInCommandsByPrefix(string prefix)
    {
        return CommandsConstants.BuiltInCommands.Where(c => c.StartsWith(prefix));
    }
    
    private static IEnumerable<string> GetAllExecutableFilesCommandByPrefix(string prefix)
    {
        return FileHelper.SearchFileNameInPathsByPrefix(prefix).Select(Path.GetFileName);
    }
    
}