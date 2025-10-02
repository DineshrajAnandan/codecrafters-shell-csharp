using System.Text;
using CodecraftersShell.Commands;
using CodecraftersShell.Constants;
using CodecraftersShell.Helpers;

var commandInput = new CommandInput();

while (true)
{
    var keyInfo = Console.ReadKey(true);

    switch (keyInfo.Key)
    {
        case ConsoleKey.Tab:
        {
            commandInput.TryAutoComplete();
            break;
        }
        case ConsoleKey.Enter:
        {
            Console.Write("\n");
            ProcessOnEnterCommand(commandInput.Input);
            commandInput.NewLine();
            break;
        }
        case ConsoleKey.Backspace:
            commandInput.RemoveLastChar();
            break;
        default:
            commandInput.Append(keyInfo.KeyChar);
            break;
    }
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
