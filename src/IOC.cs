using CodecraftersShell.Commands;
using CodecraftersShell.Helpers;

public class IOC
{
    public CommandInput CommandInput { get; private set; } = null!;

    public IOC InitContainer()
    {
        var history = new History();
        IHistoryCommand historyCommand = new HistoryCommand(history);
        IExitCommand exitCommand = new ExitCommand(historyCommand);
        IEchoCommand echoCommand = new EchoCommand();
        ITypeCommand typeCommand  = new TypeCommand();
        IPwdCommand pwdCommand = new PwdCommand();
        ICdCommand cdCommand  = new CdCommand();
        ICustomCommand customCommand  = new CustomCommand();

        var processor = new Processor(exitCommand, echoCommand, typeCommand, pwdCommand, cdCommand, customCommand, historyCommand);
        var commandInput = new CommandInput(processor, history);
        
        CommandInput = commandInput;

        return this;
    }
}