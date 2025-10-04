namespace CodecraftersShell.Helpers;

public class History
{
    private readonly LinkedList<string> _history = [];
    
    public List<(int index, string command)> Data => _history.Select((c, i) => (i+1, c)).ToList();
    public LinkedListNode<string>? Last => _history.Last;
    public LinkedListNode<string>? First => _history.First;

    public void Add(string input)
    {
        _history.AddLast(input);
    }
    
}