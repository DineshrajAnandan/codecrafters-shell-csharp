namespace codecraftersShell.Commands;

public interface ICommand
{
    public void Handle(string arguments);
}