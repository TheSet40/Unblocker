using System.Management.Automation;

public static class Powershellhandler
{
    private readonly static PowerShell PS = PowerShell.Create();

    public static void Command(string script)
    {
        PS.AddScript(script);

        PS.Invoke();

        // Print any error records to the console
        if (PS.HadErrors)
        {
            foreach (var errorRecord in PS.Streams.Error)
            {
                Console.WriteLine($"Script Error: {errorRecord}");
            }
        }
        PS.Commands.Clear();
    }
}