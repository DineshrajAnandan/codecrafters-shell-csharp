using System.Text;

namespace CodecraftersShell.Extensions;

public static class StringExtension
{
    public static string TrimTrailingEmptyLine(this string input)
    {
        if (string.IsNullOrEmpty(input)) 
            return input;
        
        var lines = input.Split('\n');
        if (lines.Length > 0 && string.IsNullOrWhiteSpace(lines[^1]))
        {
            input = string.Join('\n', lines, 0, lines.Length - 1);
        }
        return input;
    }
    
    public static List<string> SplitArguments(this string input)
    {
        var elements = new List<string>();
        var i = 0;
        
        while (i < input.Length)
        {
            char c = input[i];
            
            // Handle single quotes
            if (c == '\'')
            {
                
                i++; // Skip opening quote
                var quoted = new StringBuilder();
                while (i < input.Length && input[i] != '\'')
                {
                    quoted.Append(input[i]);
                    i++;
                }
                elements.Add(quoted.ToString());
                if (i < input.Length) i++; // Skip closing quote
            }
            // Handle double quotes
            else if (c == '"')
            {
                
                i++; // Skip opening quote
                var quoted = new StringBuilder();
                while (i < input.Length && input[i] != '"')
                {
                    quoted.Append(input[i]);
                    i++;
                }
                elements.Add(quoted.ToString());
                if (i < input.Length) i++; // Skip closing quote
            }
            // Handle whitespace - collapse to single space
            else if (char.IsWhiteSpace(c))
            {
                // Skip all consecutive whitespace
                while (i < input.Length && char.IsWhiteSpace(input[i]))
                {
                    i++;
                }
                // Add single space element if there's more content
                if (i < input.Length)
                {
                    elements.Add(" ");
                }
            }
            // Regular unquoted text
            else
            {
                
                var text = new StringBuilder();
                while (i < input.Length && !char.IsWhiteSpace(input[i]) && input[i] != '\'' && input[i] != '"')
                {
                    text.Append(input[i]);
                    i++;
                }
                elements.Add(text.ToString());
            }
        }
        
        return elements;
    }
}