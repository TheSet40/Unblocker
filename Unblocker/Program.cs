using System.Collections.ObjectModel;
using System.Management.Automation;

while (true)
{
    Console.WriteLine("use -v for output info");
    Console.Write("write path to main folder: ");
    string originalpath = Console.ReadLine()!.Trim();
    bool shouldLog = originalpath.EndsWith("-v");
    if (shouldLog) originalpath = originalpath.Split('-')[0].Trim();
    try
    {
        string[] dirs = Directory.GetDirectories(originalpath, "*", SearchOption.AllDirectories);
        Console.WriteLine("working...");
        foreach (string dir in dirs)
        {
            if (shouldLog) { Console.WriteLine(dir); }
            Powershellhandler.Command($"get-childitem \"{dir}\" | unblock-File");
        }
        Powershellhandler.Command($"get-childitem \"{originalpath}\" | unblock-File");
        Console.WriteLine("work completed\n");
    }
    catch (Exception error)
    {
        Console.WriteLine($"non valid directory!\nerror: {error}\n");
    }
}