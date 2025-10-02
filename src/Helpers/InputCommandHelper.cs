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
    
    public static bool TryAutoCompleteCommand(string prefix,
        out string remainingSubString)
    {
        return TryAutoCompleteCommandByBuiltInCommands(prefix, out remainingSubString) ||
               TryAutoCompleteCommandByExecutableFiles(prefix, out remainingSubString);
    }
    
    
    private static bool TryAutoCompleteCommandByBuiltInCommands(string prefix,
        out string remainingSubString)
    {
        var command = CommandsConstants.BuiltInCommands.FirstOrDefault(c => c.StartsWith(prefix));
        remainingSubString = command?.Substring(prefix.Length) ?? string.Empty;
        return !string.IsNullOrEmpty(command);
    }
    
    private static bool TryAutoCompleteCommandByExecutableFiles(string prefix, out string remainingSubString)
    {
        remainingSubString  = string.Empty;
        var result = FileHelper.SearchFileNameInPathsByPrefix(prefix);
        
        if (string.IsNullOrEmpty(result)) 
            return false;
        
        result = Path.GetFileName(result);
        remainingSubString = result.Substring(prefix.Length);
        return true;
    }
}