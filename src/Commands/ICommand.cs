namespace codecrafterdShell.Commands;

public interface ICommand
{
    public void Handle(string arguments);
}