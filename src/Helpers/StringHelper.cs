namespace CodecraftersShell.Helpers;

public static class StringHelper
{
    public static string TrimTrailingEmptyLine(string input)
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