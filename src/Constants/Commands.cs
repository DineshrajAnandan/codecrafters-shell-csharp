namespace codecrafters_shell.Constants;

public static class Commands
{
    public const string ECHO = "echo";
    public const string EXIT = "exit";
    public const string TYPE = "type";
    
    public static readonly HashSet<string> BuiltInCommands = 
        [ECHO, EXIT, TYPE];
}
