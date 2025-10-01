while (true)
{
    Console.Write("$ ");
    var command = Console.ReadLine();
    if (command == "exit 0")
    {
        break;
    }
    else if (command.StartsWith("echo "))
    {
        Console.WriteLine(command.Replace("echo ",""));
    }
    else 
        Console.WriteLine($"{command}: command not found");
}
