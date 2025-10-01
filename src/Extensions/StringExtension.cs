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
}