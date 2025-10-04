using System.Text.RegularExpressions;
using CodecraftersShell.Extensions;

namespace CodecraftersShell.Commands;

public interface IEchoCommand : ICommand;
public class EchoCommand: IEchoCommand
{
    public void Handle(string arguments)
    {
        Console.WriteLine(arguments);
    }
}