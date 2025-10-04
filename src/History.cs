namespace CodecraftersShell.Helpers;

public class History
{
    private readonly List<string> _history = [];
    
    public List<(int index, string command)> Data => _history.Select((c, i) => (i+1, c)).ToList();

    public void Add(string input)
    {
        _history.Add(input);
    }
    
}