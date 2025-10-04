using System.Text.RegularExpressions;
using CodecraftersShell.Extensions;
using CodecraftersShell.Helpers;

namespace CodecraftersShell.Commands;

public interface IHistoryCommand : ICommand;
public class HistoryCommand(History history): IHistoryCommand
{

    public void Handle(string arguments)
    {
        if (!string.IsNullOrEmpty(arguments)) 
            return;
        
        for(var i = 0; i < history.Data.Count; i++) 
        {
            Console.WriteLine($"{i+1} {history.Data[i]}");
        }
    }
}