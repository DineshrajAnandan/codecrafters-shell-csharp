using CodecraftersShell.Commands;
using CodecraftersShell.Constants;

while (true)
{
    Console.Write("$ ");
    var input = Console.ReadLine();
    var inputArr = input.Split(' ', 2);
    var command = inputArr[0];
    var arguments = inputArr.Length == 1 ? null : inputArr[1];

    try
    {
        ICommand commandToExecute = command switch
        {
            CommandsConstants.EXIT => new ExitCommand(),
            // CommandsConstants.ECHO => new EchoCommand(),
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
