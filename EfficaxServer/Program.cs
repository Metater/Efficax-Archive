Console.WriteLine("Hello, World!");

NetworkOutputHandler networkOutputHandler = new();
Stopwatch sw = new();
Thread t = new(() =>
{
    try
    {
        networkOutputHandler.Start();
    }
    catch (Exception e)
    {
        sw.Stop();
        Console.WriteLine(sw.ElapsedTicks);
        Console.WriteLine(networkOutputHandler.sends);
        Console.WriteLine(networkOutputHandler.resets);
        Console.WriteLine(e.ToString());
    }
});
t.Start();
sw.Start();
for (int i = 0; i < 1000; i++)
{
    networkOutputHandler.Send(null);
}
Thread.Sleep(10);
networkOutputHandler.Send(null);

Thread.Sleep(100000);