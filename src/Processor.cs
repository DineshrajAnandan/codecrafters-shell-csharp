using CodecraftersShell.Commands;
using CodecraftersShell.Constants;

namespace CodecraftersShell.Helpers;

public class Processor(
    IExitCommand exitCommand,
    IEchoCommand echoCommand,
    ITypeCommand typeCommand,
    IPwdCommand pwdCommand,
    ICdCommand cdCommand,
    ICustomCommand customCommand,
    IHistoryCommand historyCommand)
{
    public void Process(string input)
    {
        var command = InputCommandHelper.ParseInputCommand(input, out var arguments);

        try
        {
            ICommand commandToExecute = command switch
            {
                CommandsConstants.EXIT => exitCommand,
                CommandsConstants.ECHO => echoCommand,
                CommandsConstants.TYPE => typeCommand,
                CommandsConstants.PWD => pwdCommand,
                CommandsConstants.CD => cdCommand,
                CommandsConstants.HISTORY => historyCommand,
                _ => customCommand.WithCommand(command)
            };
            commandToExecute.Handle(arguments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}