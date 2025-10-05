namespace CodecraftersShell.Helpers;

public interface IExecutableFileHelper
{
    string ExecuteFile(string command, string arguments);
}
public class ExecutableFileHelper: IExecutableFileHelper
{
    public string ExecuteFile(string command, string arguments)
    {
        var process = new System.Diagnostics.Process
        {
            StartInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            }
        };
        process.Start();
        var output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        return output;
    }
}