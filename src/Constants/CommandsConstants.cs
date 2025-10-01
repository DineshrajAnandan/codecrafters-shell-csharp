namespace codecrafters_shell.Constants;

public static class CommandsConstants
{
    public const string ECHO = "echo";
    public const string EXIT = "exit";
    public const string TYPE = "type";
    public const string PWD = "pwd";
    
    public static readonly HashSet<string> BuiltInCommands = 
        [ECHO, EXIT, TYPE, PWD];
}
