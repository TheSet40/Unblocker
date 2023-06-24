
while (true)
{
    Console.Write("write path to main folder: ");
    string originalpath = Console.ReadLine()!;
    try
    {
        string[] dirs = Directory.GetDirectories(originalpath, "*", SearchOption.AllDirectories);
        Console.WriteLine("working...");
        foreach (string dir in dirs)
        {
            Powershellhandler.Command($"get-childitem \"{dir}\" | unblock-File");
        }
        Powershellhandler.Command($"get-childitem \"{originalpath}\" | unblock-File");
        Console.WriteLine("work completed\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"non valid directory!\nerror: {ex.ToString().Split('\n')[0]}\n");
    }
}