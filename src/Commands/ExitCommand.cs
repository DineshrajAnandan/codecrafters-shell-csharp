namespace CodecraftersShell.Commands;

public interface IExitCommand : ICommand;
public class ExitCommand(IHistoryCommand historyCommand): IExitCommand
{
    public void Handle(string arguments)
    {
        var exitCode = string.IsNullOrEmpty(arguments) ? 0 : 
            int.Parse(arguments);
        historyCommand.UpdateHistoryFile();
        Environment.Exit(exitCode);
    }
}