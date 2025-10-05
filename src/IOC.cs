using CodecraftersShell.Commands;
using CodecraftersShell.Helpers;

public class Ioc
{
    public CommandInput CommandInput { get; private set; } = null!;

    public Ioc InitContainer()
    {
        var history = new History();
        
        // helpers
        IFileHelper fileHelper = new FileHelper();
        IInputCommandHelper inputCommandHelper = new InputCommandHelper(fileHelper);
        IExecutableFileHelper executableFileHelper = new ExecutableFileHelper();
        
        // commands
        IHistoryCommand historyCommand = new HistoryCommand(history, fileHelper);
        IExitCommand exitCommand = new ExitCommand(historyCommand);
        IEchoCommand echoCommand = new EchoCommand();
        ITypeCommand typeCommand  = new TypeCommand(fileHelper);
        IPwdCommand pwdCommand = new PwdCommand();
        ICdCommand cdCommand  = new CdCommand();
        ICustomCommand customCommand  = new CustomCommand(fileHelper, executableFileHelper);

        var processor = new Processor(
            exitCommand,
            echoCommand,
            typeCommand,
            pwdCommand,
            cdCommand,
            customCommand,
            historyCommand,
            inputCommandHelper);
        var commandInput = new CommandInput(
            processor, 
            history,
            inputCommandHelper);
        
        CommandInput = commandInput;

        return this;
    }
}