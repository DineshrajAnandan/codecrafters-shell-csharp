namespace CodecraftersShell.Extensions;

public static class StringExtension
{
    public static string TrimTrailingEmptyLine(this string input)
    {
        if (!string.IsNullOrEmpty(input))
        {
            var lines = input.Split('\n');
            if (lines.Length > 0 && string.IsNullOrWhiteSpace(lines[^1]))
            {
                input = string.Join('\n', lines, 0, lines.Length - 1);
            }
        }
        return input;
    }

    public static string GetTextWithinSingleQuotes(this string input, out bool hasQuotes)
    {
        hasQuotes = false;
        if (!input.StartsWith("'") || !input.EndsWith("'") || input.Length <= 1) 
            return input;
        
        hasQuotes = true;
        return input.Substring(1, input.Length - 2);
    }
}