using CodecraftersShell.Helpers;

namespace CodecraftersShell.Commands;

public interface IHistoryCommand : ICommand
{
    void UpdateHistoryFile();
}
public class HistoryCommand: IHistoryCommand
{
    private int _lastHistoryWrittenToFile;
    private readonly History _history;
    private readonly IFileHelper _fileHelper;

    public HistoryCommand(History history, IFileHelper fileHelper)
    {
        _history = history;
        _fileHelper = fileHelper;
        try
        {
            var historyFile = Environment.GetEnvironmentVariable("HISTFILE");
            if(!string.IsNullOrEmpty(historyFile)) ReadHistoryFromFile(historyFile);
        }
        catch (Exception)
        {
            // ignore
        }
    }
    
    public void Handle(string arguments)
    {
        if (string.IsNullOrEmpty(arguments))
        {
            PrintHistory(_history.Data);
            return;
        }
        
        if (TryParseHistoryLimit(arguments, out var limitCount))
        {
            PrintHistory(_history.Data.Skip(_history.Data.Count - limitCount));
            return;
        }

        HandleFileOperations(arguments);
    }

    public void UpdateHistoryFile()
    {
        try
        {
            var historyFile = Environment.GetEnvironmentVariable("HISTFILE");
            if(!string.IsNullOrEmpty(historyFile)) AppendHistoryToFile(historyFile);
        }
        catch (Exception)
        {
            // ignore
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
    
    private void ReadHistoryFromFile(string fileName)
    {
        var data = _fileHelper.ReadAllText(fileName);
        if (string.IsNullOrEmpty(data))
            return;
        var historyList = data.Split('\n')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        foreach (var item in historyList)
        {
            _history.Add(item);
        }
        _lastHistoryWrittenToFile = _history.RawData.Count;
    }

    private void WriteHistoryToFile(string fileName)
    {
        var data = string.Join("\n", _history.RawData) + "\n";
        _fileHelper.WriteAllText(fileName, data);
        _lastHistoryWrittenToFile = _history.RawData.Count;
    }
    
    private void AppendHistoryToFile(string fileName)
    {
        var historyToAppend = _history.RawData.Skip(_lastHistoryWrittenToFile).ToList();
        var data = string.Join("\n", historyToAppend) + "\n";
        _fileHelper.AppendAllText(fileName, data);
        _lastHistoryWrittenToFile = _history.RawData.Count;
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