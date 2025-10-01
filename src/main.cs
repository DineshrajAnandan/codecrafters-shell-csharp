using codecrafters_shell.Constants;

while (true)
{
    Console.Write("$ ");
    var input = Console.ReadLine();
    var inputArr = input.Split(' ', 2);
    var command = inputArr[0];
    var arguments = inputArr.Length == 1 ? null : inputArr[1];

    switch (command)
    {
        case Commands.EXIT:
        {
            var exitCode = arguments == null ? 0 : int.Parse(arguments);
            Environment.Exit(exitCode);
            return;
        }
        case Commands.TYPE:
        {
            var type = arguments;
            Console.WriteLine(Commands.BuiltInCommands.Contains(type)
                ? $"{type} is a shell builtin"
                : $"{type}: not found   ");
            break;
        }
        case Commands.ECHO:
        {
            Console.WriteLine(arguments);
            break;
        }
        default:
            Console.WriteLine($"{command}: command not found");
            break;
    }
}
