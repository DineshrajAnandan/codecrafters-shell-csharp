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
    
    public static bool TryAutoCompleteCommand(
        string prefix,
        out string remainingSubString,
        bool showAllCommands,
        out IEnumerable<string> commands)
    {
        remainingSubString = string.Empty;
        var builtIn = GetAllBuiltInCommandsByPrefix(prefix);
        var executables = GetAllExecutableFilesCommandByPrefix(prefix);
    
        var allCommands = builtIn.Concat(executables)
                                            .Distinct()
                                            .OrderBy(c => c)
                                            .ToList();
    
        switch (allCommands.Count)
        {
            case 0:
                commands = [];
                return false;
            case 1:
                commands = allCommands;
                remainingSubString = allCommands[0].Substring(prefix.Length);
                return true;
            default:
                commands = showAllCommands ? allCommands : [];
                return showAllCommands;
        }
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