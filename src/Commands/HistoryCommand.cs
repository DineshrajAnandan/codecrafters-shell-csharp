using CodecraftersShell.Helpers;

namespace CodecraftersShell.Commands;

public interface IHistoryCommand : ICommand;
public class HistoryCommand(History history): IHistoryCommand
{
    private readonly List<string> _historyWrittenToFile = new();
    
    public void Handle(string arguments)
    {
        if (string.IsNullOrEmpty(arguments))
        {
            PrintHistory(history.Data);
            return;
        }
        
        if (TryParseHistoryLimit(arguments, out var limitCount))
        {
            PrintHistory(history.Data.Skip(history.Data.Count - limitCount));
            return;
        }

        HandleFileOperations(arguments);
    }

    private void HandleFileOperations(string arguments)
    {
        var (flag, fileName) = ParseFileArguments(arguments);

        switch (flag)
        {
            case "-r":
                ReadHistoryFromFile(fileName);
                break;
            case "-w":
                WriteHistoryToFile(fileName);
                break;
            case "-a":
                AppendHistoryToFile(fileName);
                break;
            default:
                throw new ArgumentException($"Unknown history argument: {arguments}");
        }
    }

    private void WriteHistoryToFile(string fileName)
    {
        var data = string.Join("\n", history.RawData) + "\n";
        FileHelper.WriteAllText(fileName, data);
        _historyWrittenToFile.AddRange(history.RawData);
    }
    
    private void AppendHistoryToFile(string fileName)
    {
        var allHistory = history.RawData;
        var historyToAppend = allHistory.Except(_historyWrittenToFile).ToList();
        var data = string.Join("\n", historyToAppend) + "\n";
        FileHelper.AppendAllText(fileName, data);
        _historyWrittenToFile.AddRange(historyToAppend);
    }

    private void ReadHistoryFromFile(string fileName)
    {
        var data = FileHelper.ReadAllText(fileName);
        if (string.IsNullOrEmpty(data))
            return;
        var historyList = data.Split('\n')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        foreach (var item in historyList)
        {
            history.Add(item);
        }
    }

    private bool TryParseHistoryLimit(string arguments, out int limitCount)
    {
        return int.TryParse(arguments.Trim(), out limitCount);
    }

    private (string flag, string fileName) ParseFileArguments(string arguments)
    {
        var argSplit = arguments.Split(' ');
        if (argSplit.Length != 2)
        {
            throw new ArgumentException("Invalid arguments");
        }
        return (argSplit[0], argSplit[1]);
    }

    private void PrintHistory(IEnumerable<(int index, string command)> data)
    {
        foreach (var item in data)
            Console.WriteLine($"{item.index} {item.command}");
    }
}