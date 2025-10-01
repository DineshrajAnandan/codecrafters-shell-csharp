namespace CodecraftersShell.Commands;

public class PwdCommand: ICommand
{
    public void Handle(string _)
    {
        Console.WriteLine(Directory.GetCurrentDirectory());
    }
}