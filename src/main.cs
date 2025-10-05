using CodecraftersShell.Commands;
using CodecraftersShell.Helpers;

var history = new History();

IExitCommand exitCommand = new ExitCommand();
IEchoCommand echoCommand = new EchoCommand();
ITypeCommand typeCommand  = new TypeCommand();
IPwdCommand pwdCommand = new PwdCommand();
ICdCommand cdCommand  = new CdCommand();
ICustomCommand customCommand  = new CustomCommand();
IHistoryCommand historyCommand = new HistoryCommand(history);

var processor = new Processor(exitCommand, echoCommand, typeCommand, pwdCommand, cdCommand, customCommand, historyCommand);
var commandInput = new CommandInput(processor, history);

historyCommand.ReadHistoryFromFile(Environment.GetEnvironmentVariable("HISTFILE"));

var prevKeyInfo = new ConsoleKeyInfo();

while (true)
{
    var keyInfo = Console.ReadKey(true);

    switch (keyInfo.Key)
    {
        case ConsoleKey.Tab:
        {
            commandInput.TryAutoComplete(prevKeyInfo.Key == ConsoleKey.Tab);
            break;
        }
        case ConsoleKey.Enter:
        {
            commandInput.Process();
            break;
        }
        case ConsoleKey.UpArrow:
        {
            commandInput.HistoryScrollUp();
            break;
        }
        case ConsoleKey.DownArrow:
        {
            commandInput.HistoryScrollDown();
            break;
        }
        case ConsoleKey.Backspace:
            commandInput.RemoveLastChar();
            break;
        default:
            commandInput.Append(keyInfo.KeyChar);
            break;
    }
    
    prevKeyInfo =  keyInfo;
}



