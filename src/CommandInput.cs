using System.Text;
using CodecraftersShell.Constants;

namespace CodecraftersShell.Helpers;

public class CommandInput
{
    private StringBuilder _inputBuilder = new();
    public string Input => _inputBuilder.ToString();
    private Processor _processor;

    public CommandInput(Processor processor)
    {
        _processor = processor;
        Console.Write("$ ");
    }

    public void NewLine()
    {
        _inputBuilder.Clear();
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
        var found = InputCommandHelper.TryAutoCompleteCommand(
            Input, 
            out var remainingSubString,
            showAllCommands,
            out IEnumerable<string> commands);
        if (!found)
        {
            Beep();
            return;
        }

        if (!showAllCommands)
        {
            Append(remainingSubString + " ");
            return;
        }
        
        Console.WriteLine($"\n{string.Join("  ", commands)}");
        Console.Write($"$ {Input}");
        
    }

    public void Process()
    {
        Console.Write("\n");
        _processor.Process(Input);
        NewLine();
    }
    
    private void Beep()
    {
        Console.Write("\x07");
    }

    private void Bell()
    {
        Console.Write("\a");
    }
}