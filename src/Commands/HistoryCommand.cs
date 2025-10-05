using CodecraftersShell.Helpers;

namespace CodecraftersShell.Commands;

public interface IHistoryCommand : ICommand
{
    void ReadHistoryFromFile(string fileName);
}
public class HistoryCommand(History history): IHistoryCommand
{
    private int _lastHistoryWrittenToFile = 0;
    
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

    public void ReadHistoryFromFile(string fileName)
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
        _lastHistoryWrittenToFile = history.RawData.Count;
    }
    
    private void AppendHistoryToFile(string fileName)
    {
        var historyToAppend = history.RawData.Skip(_lastHistoryWrittenToFile).ToList();
        var data = string.Join("\n", historyToAppend) + "\n";
        FileHelper.AppendAllText(fileName, data);
        _lastHistoryWrittenToFile = history.RawData.Count;
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