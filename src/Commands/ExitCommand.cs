namespace codecrafterdShell.Commands;

public class ExitCommand: ICommand
{
    public void Handle(string arguments)
    {
        var exitCode = string.IsNullOrEmpty(arguments) ? 0 : 
            int.Parse(arguments);
        Environment.Exit(exitCode);
    }
}