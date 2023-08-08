using System.Management.Automation;

public static class Powershellhandler
{
    private readonly static PowerShell PS = PowerShell.Create();

    public static void Command(string script)
    {
        PS.AddScript(script);

        PS.Invoke();

        if (PS.HadErrors)
        {
            foreach (var errorRecord in PS.Streams.Error)
            {
                Console.WriteLine($"Script Error: {errorRecord}");
            }
            PS.Streams.Error.Clear();
        }
        PS.Commands.Clear();
    }
}