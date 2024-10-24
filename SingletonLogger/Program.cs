
using SingletonLogger;

internal class Program
{
    static void Main(string[] args)
    {
        LogToFileWithUppercase();
    }

    static void LogToConsole()
    {
        var logger = Logger.Instance;
        logger.LogInfo("hello world");
    }

    static void LogToFile()
    {
        var fileName = $"DP.P1.{DateTime.Now.ToString("yyyy-MM-dd.HH-mm-ss")}.log";
        var logFile = new StreamWriter(fileName);

        Logger.SetOutputStrategy(logFile);

        var logger = Logger.Instance;
        logger.LogInfo("hello world");
    }

    static void LogToFileWithUppercase()
    {
        var fileName = $"DP.P1.{DateTime.Now.ToString("yyyy-MM-dd.HH-mm-ss")}.log";
        var logFileStrategy = new StreamWriter(fileName);
        var uppercaseLogStrategy = new UppercaseMessageStrategy();

        Logger.SetOutputStrategy(logFileStrategy);
        Logger.SetMessageCaseStrategy(uppercaseLogStrategy);

        var logger = Logger.Instance;
        logger.LogInfo("hello world");
    }
}