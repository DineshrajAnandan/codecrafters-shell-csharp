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

    private bool TryAutoCompleteFromExecutableFile(string prefix, out string remainingSubString)
    {
        var result = FileHelper.SearchFileNameInPathsByPrefix(Input);
        remainingSubString = result?.Substring(Input.Length) ?? string.Empty;
        return !string.IsNullOrEmpty(remainingSubString);
    }

    public void TryAutoComplete()
    {
        string remainingSubString;
        var found = CommandsConstants.TryAutoCompleteCommand(Input, out remainingSubString) ||
                            TryAutoCompleteFromExecutableFile(Input, out remainingSubString);
        if (!found)
        {
            Beep();
            return;
        }
        Append(remainingSubString + " ");
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
}