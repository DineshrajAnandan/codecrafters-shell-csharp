using System.Text;
using CodecraftersShell.Commands;
using CodecraftersShell.Constants;
using CodecraftersShell.Helpers;

var inputBuilder = new StringBuilder();
Console.Write("$ ");
while (true)
{
    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

    if (keyInfo.Key == ConsoleKey.Tab)
    {
        ClearCurrentConsoleLine();
            
        string input = inputBuilder.ToString();

        var command = CommandsConstants.BuiltInCommands
            .FirstOrDefault(c => c.StartsWith(input));
        var displayText = string.IsNullOrEmpty(command) ? input : command;
        inputBuilder.Clear();
        inputBuilder.Append(displayText);
        Console.Write($"$ {displayText}");
    }
    else if (keyInfo.Key == ConsoleKey.Enter)
    {
        string input = inputBuilder.ToString();
        Console.Write("\n");
        inputBuilder.Clear();
        ProcessOnEnterCommand(input);
        Console.Write("$ ");    
    }
    else if (keyInfo.Key == ConsoleKey.Backspace && inputBuilder.Length > 0)
    {
        inputBuilder.Remove(inputBuilder.Length - 1, 1);
        Console.Write("\b \b");
    }
    else if (keyInfo.Key == ConsoleKey.Escape)
    {
        break;
    }
    else
    {
        inputBuilder.Append(keyInfo.KeyChar);
        Console.Write(keyInfo.KeyChar); 
    }
    
    
}

void ClearCurrentConsoleLine()
{
    int currentLineCursor = Console.CursorTop;
    // Move the cursor to the start of the line.
    Console.SetCursorPosition(0, currentLineCursor);
    // Overwrite the line with a string of spaces.
    Console.Write(new string(' ', Console.WindowWidth));
    // Move the cursor back to the start of the cleared line.
    Console.SetCursorPosition(0, currentLineCursor);
}

void ProcessOnEnterCommand(string input)
{
    var command = InputCommandHelper.ParseInputCommand(input, out var arguments);

    try
    {
        ICommand commandToExecute = command switch
        {
            CommandsConstants.EXIT => new ExitCommand(),
            CommandsConstants.ECHO => new EchoCommand(),
            CommandsConstants.TYPE => new TypeCommand(),
            CommandsConstants.PWD => new PwdCommand(),
            CommandsConstants.CD => new CdCommand(),
            _ => new CustomCommand(command)
        };
        commandToExecute.Handle(arguments);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}