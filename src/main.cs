using CodecraftersShell.Commands;
using CodecraftersShell.Constants;
using CodecraftersShell.Helpers;

while (true)
{
    Console.Write("$ ");
    var input = Console.ReadLine();
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

