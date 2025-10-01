using System.Text.RegularExpressions;
using CodecraftersShell.Extensions;

namespace CodecraftersShell.Commands;

public class EchoCommand: ICommand
{
    public void Handle(string arguments)
    {
        var texts = Regex.Matches(arguments, @"[^\s""']+|'([^']*)'")
            .Cast<Match>()
            .Select(m => m.Value.Trim('\''))
            .ToArray();
        
        Console.WriteLine(string.Join(" ", texts));
        
        // var text = arguments.GetTextWithinSingleQuotes(out var hasQuotes);
        // if (hasQuotes)
        // {
        //     Console.WriteLine(text);
        //     return;
        // }
        //
        // var textArray = text.Split(' ')
        //     .Select(s => s.Trim())
        //     .Where(s => !string.IsNullOrEmpty(s));
        // var output = string.Join(" ", textArray);
        // Console.WriteLine(output);
    }
}