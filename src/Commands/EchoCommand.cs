namespace CodecraftersShell.Commands;

public class EchoCommand: ICommand
{
    public void Handle(string arguments)
    {
        Console.WriteLine(arguments);
    }
}