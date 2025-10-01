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
}