namespace CodecraftersShell.Commands;

public interface ICommand
{
    public void Handle(string arguments);
}