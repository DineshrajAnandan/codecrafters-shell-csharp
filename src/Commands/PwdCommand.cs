namespace CodecraftersShell.Commands;

public interface IPwdCommand : ICommand;
public class PwdCommand: IPwdCommand
{
    public void Handle(string _)
    {
        Console.WriteLine(Directory.GetCurrentDirectory());
    }
}