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
        if(string.IsNullOrEmpty(input))
            return elements;
        var current = new StringBuilder();
        var i = 0;
        
        while (i < input.Length)
        {
            char c = input[i];
            
            // Handle single quotes - add quoted content as single element
            if (c == '\'')
            {
                // Add any accumulated text before the quote
                if (current.Length > 0)
                {
                    elements.Add(current.ToString());
                    current.Clear();
                }
                
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
            // Handle double quotes - add quoted content as single element
            else if (c == '"')
            {
                // Add any accumulated text before the quote
                if (current.Length > 0)
                {
                    elements.Add(current.ToString());
                    current.Clear();
                }
                
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
            // Handle space - add as separate element
            else if (c == ' ')
            {
                // Add any accumulated text before the space
                if (current.Length > 0)
                {
                    elements.Add(current.ToString());
                    current.Clear();
                }
                
                elements.Add(" ");
                i++;
            }
            // Regular character - accumulate
            else
            {
                current.Append(c);
                i++;
            }
        }
        
        // Add last accumulated text if any
        if (current.Length > 0)
        {
            elements.Add(current.ToString());
        }
        
        return elements;
    }
}