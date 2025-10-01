string[] builtInCommands = ["echo", "exit", "type"];

while (true)
{
    Console.Write("$ ");
    var command = Console.ReadLine();
    if (command == "exit 0")
    {
        break;
    }

    if (command.StartsWith("echo "))
    {
        Console.WriteLine(command.Replace("echo ", ""));
    }

    if (command.StartsWith("type "))
    {
        var type = command.Split(' ')[1];
        if (builtInCommands.Contains(type))
        {
            Console.WriteLine($"{type} is a shell builtin");
        }
        else
        {
            Console.WriteLine($"{type}: not found   ");
        }
    }
    else
        Console.WriteLine($"{command}: command not found");
}
