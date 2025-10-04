using System.Text;

namespace CodecraftersShell.Helpers;

public class CommandInput
{
    private readonly StringBuilder _inputBuilder = new();
    private string Input => _inputBuilder.ToString();
    private readonly Processor _processor;
    private readonly History _history;
    private LinkedListNode<string>? _currentHistoryNode = null;

    public CommandInput(Processor processor, History history)
    {
        _processor = processor;
        _history = history;
        Console.Write("$ ");
    }
    
    public void Append<T>(T text)
    {
        _inputBuilder.Append(text);
        Console.Write(text);
    }

    public void RemoveLastChar()
    {
        if(_inputBuilder.Length <= 0)
            return;
        _inputBuilder.Remove(_inputBuilder.Length - 1, 1);
        Console.Write("\b \b");
    }

    public void TryAutoComplete(bool showAllCommands = false)
    {
        InputCommandHelper.TryAutoCompleteCommand(
            Input, 
            out var remainingSubString,
            out var commands);
        
        if (!string.IsNullOrEmpty(remainingSubString))
        {
            Append(remainingSubString);
            if(commands.Count == 1)
                Append(" ");
            return;
        }
        
        if (commands.Count > 0 && showAllCommands)
        {
            Console.WriteLine($"\n{string.Join("  ", commands)}");
            Console.Write($"$ {Input}");
            return;
        }
        
        Beep();
        
    }

    public void Process()
    {
        _history.Add(Input);
        Console.Write("\n");
        _processor.Process(Input);
        NewLine();
    }

    public void HistoryScrollUp(bool isNewScroll = true)
    {
        if (_currentHistoryNode == _history.First)
            return;
        _currentHistoryNode = isNewScroll ? _history.Last : _currentHistoryNode?.Previous;
        OverwriteInput(_currentHistoryNode?.Value ?? string.Empty);
    }
    
    public void HistoryScrollDown()
    {
        _currentHistoryNode = _currentHistoryNode?.Next;
        OverwriteInput(_currentHistoryNode?.Value ?? string.Empty);
    }

    private void OverwriteInput(string input)
    {
        ClearLine();
        if (!string.IsNullOrEmpty(input))
            Append(input);
    }

    private void ClearLine()
    {
        Console.Write(string.Join("", Enumerable.Repeat("\b \b", _inputBuilder.Length)));
        _inputBuilder.Clear();
    }
    
    private void Beep()
    {
        Console.Write("\x07");
    }
    
    private void NewLine()
    {
        _inputBuilder.Clear();
        Console.Write("$ ");
    }
}