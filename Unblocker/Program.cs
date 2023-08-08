

namespace Unblocker
{
    class UnblockerProgram
    {
        static async Task DisplayWorking(string message, CancellationToken cancellationToken)
        {
            byte i = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.Clear();
                Console.WriteLine(message + new string('.', i % 4));
                i++;
                i %= 4;
                await Task.Delay(250);
            }
        }
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("use -v for output info");
                Console.Write("write path to main folder: ");
                string originalpath = Console.ReadLine()!.Trim();
                bool shouldLog = originalpath.EndsWith("-v");
                if (shouldLog) originalpath = originalpath.Split('-')[0].Trim();
                try
                {
                    var ctsDisplay = new CancellationTokenSource();
                    var displayTask = DisplayWorking("checking directories", ctsDisplay.Token);
                    string[] dirs = Directory.GetDirectories(originalpath!, "*", SearchOption.AllDirectories);
                    ctsDisplay.Cancel();
                    await displayTask;
                    var ctsWorking = new CancellationTokenSource();
                    if (!shouldLog) displayTask = DisplayWorking("working", ctsWorking.Token);
                    for (long i = 0; i < dirs.Length; i++)
                    {
                        if (shouldLog) Console.WriteLine(dirs[i]);
                        Powershellhandler.Command($"get-childitem \"{dirs[i]}\" | unblock-File");
                    }
                    Powershellhandler.Command($"get-childitem \"{originalpath}\" | unblock-File");
                    ctsWorking.Cancel();
                    await displayTask;
                    Console.WriteLine("Work completed\n");
                }
                catch (Exception error)
                {
                    Console.WriteLine($"Non valid directory!\nError: {error}\n");
                }
            }
        }
    }
}