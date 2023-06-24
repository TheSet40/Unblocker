using System.Management.Automation;

public static class Powershellhandler
{
    private readonly static PowerShell PS = PowerShell.Create();

    public static void Command(string script)
    {
        PS.AddScript(script);

        PS.AddCommand("Out-string");

        IAsyncResult result = PS.BeginInvoke<PSObject, PSObject>(null, null);

        PS.EndInvoke(result);

        PS.Commands.Clear();
    }
}