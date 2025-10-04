namespace CodecraftersShell.Commands;

public interface IExitCommand : ICommand;
public class ExitCommand: IExitCommand
{
    public void Handle(string arguments)
    {
        var exitCode = string.IsNullOrEmpty(arguments) ? 0 : 
            int.Parse(arguments);
        Environment.Exit(exitCode);
    }
}