using CodecraftersShell.Helpers;

namespace CodecraftersShell.Commands;

public interface IHistoryCommand : ICommand;
public class HistoryCommand(History history): IHistoryCommand
{
    public void Handle(string arguments)
    {
        if (string.IsNullOrEmpty(arguments))
        {
            PrintHistory(history.Data);
            return;
        }
        
        var displayHistoryWithLimit = int.TryParse(arguments.Trim(),out var limitCount);

        if (displayHistoryWithLimit)
        {
            PrintHistory(history.Data.Skip(history.Data.Count - limitCount));
        }
    }

    private void PrintHistory(IEnumerable<(int index, string command)> data)
    {
        foreach (var item in data)
            Console.WriteLine($"{item.index} {item.command}");
    }
}