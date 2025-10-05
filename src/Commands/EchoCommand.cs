using System.Text;
using CodecraftersShell.Extensions;

namespace CodecraftersShell.Commands;

public interface IEchoCommand : ICommand;
public class EchoCommand: IEchoCommand
{
    public void Handle(string arguments)
    {
        var argumentsList = arguments.SplitArguments();
        Console.WriteLine(GetDisplayText(argumentsList));
    }

    private static string GetDisplayText(List<string> argumentsList)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < argumentsList.Count; i++)
        {
            if (string.IsNullOrWhiteSpace(argumentsList[i])) 
                continue;
            
            if (i > 0 && argumentsList[i - 1] == " ")
            {
                sb.Append(' ');
            }
            sb.Append(argumentsList[i]);
        }

        return sb.ToString();
    }
}