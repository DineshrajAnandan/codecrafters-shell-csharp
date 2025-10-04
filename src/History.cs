namespace CodecraftersShell.Helpers;

public class History
{
    private readonly List<string> _history = [];
    
    public List<string> Data => _history;

    public void Add(string input)
    {
        _history.Add(input);
    }
    
}