
using SingletonLogger;

internal class Program
{
    static void Main(string[] args)
    {
        LogWithThreadingToFile();
    }

    static void LogWithThreadingToConsole()
    {
        var numOfThreads = 5;
        var countdown = new CountdownEvent(numOfThreads);

        for (int i = 0; i < numOfThreads; i++)
        {
            new Thread(delegate ()
            {
                var logger = Logger.Instance;
                logger.LogTrace($"trace log from thread {Thread.CurrentThread.ManagedThreadId}");
                countdown.Signal();
            }).Start();
        }

        countdown.Wait();
        Logger.Instance.LogInfo("Finished");
    }

    static void LogWithThreadingToFile()
    {
        var fileName = $"DP.P1.{DateTime.Now.ToString("yyyy-MM-dd.HH-mm-ss")}.log";
        var logFile = new StreamWriter(fileName);
        Logger.SetOutput(logFile);


        var numOfThreads = 5;
        var countdown = new CountdownEvent(numOfThreads);

        for (int i = 0; i < numOfThreads; i++)
        {
            new Thread(delegate ()
            {
                var logger = Logger.Instance;
                logger.LogTrace($"trace log from thread {Thread.CurrentThread.ManagedThreadId}");
                countdown.Signal();
            }).Start();
        }

        countdown.Wait();
        Logger.Instance.LogInfo("Finished");
    }
}