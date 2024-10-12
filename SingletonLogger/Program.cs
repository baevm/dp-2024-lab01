
using SingletonLogger;

internal class Program
{
    static void Main(string[] args)
    {
        var fileName = $"DP.P1.{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.log";
        var logFile = new StreamWriter(fileName);
        Logger.SetOutput(logFile);

        var instance1 = Logger.Instance;
        instance1.LogInfo("info log");
    }
}