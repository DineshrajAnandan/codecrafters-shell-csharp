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
        commands = [];
        remainingSubString = string.Empty;
        var allCommands = new List<string>();
        allCommands.AddRange(GetAllBuiltInCommandsByPrefix(prefix));
        allCommands.AddRange(GetAllExecutableFilesCommandByPrefix(prefix));
        allCommands.Sort();
        
        if (!allCommands.Any())
            return false;

        if (allCommands.Count == 1)
        {
            remainingSubString = allCommands[0].Substring(prefix.Length);
            return true;
        }

        if (!showAllCommands)
            return false;
        
        commands = allCommands;
        return true;
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