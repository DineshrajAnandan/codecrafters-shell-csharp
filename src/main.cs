using CodecraftersShell.Helpers;

var processor = new Processor();
var commandInput = new CommandInput(processor);

while (true)
{
    var keyInfo = Console.ReadKey(true);

    switch (keyInfo.Key)
    {
        case ConsoleKey.Tab:
        {
            commandInput.TryAutoComplete();
            break;
        }
        case ConsoleKey.Enter:
        {
            commandInput.Process();
            break;
        }
        case ConsoleKey.Backspace:
            commandInput.RemoveLastChar();
            break;
        default:
            commandInput.Append(keyInfo.KeyChar);
            break;
    }
}



