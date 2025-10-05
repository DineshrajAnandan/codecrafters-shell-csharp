var IOC = new IOC().InitContainer();
var commandInput = IOC.CommandInput;

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



