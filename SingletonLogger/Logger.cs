namespace SingletonLogger;

public sealed class Logger
{
    private static StreamWriter Writer = new StreamWriter(Console.OpenStandardOutput());

    // вызывается при загрузке класса в память
    private static Logger _instance = new Logger();
    public static Logger Instance
    {
        get
        {
            return _instance;
        }
    }

    private Logger() { }

    public static void SetOutput(StreamWriter streamWriter)
    {
        Writer = streamWriter;
    }

    private void LogWithLevel(LogLevel level, string message)
    {
        var date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Writer.WriteLine($"{date} [{level}] {message}");
        Writer.Flush();
    }

    public void Logln(string message)
    {
        Writer.WriteLine(message);
        Writer.Flush();
    }

    public void LogTrace(string message)
    {
        LogWithLevel(LogLevel.TRACE, message);
    }

    public void LogInfo(string message)
    {
        LogWithLevel(LogLevel.INFO, message);
    }

    public void LogWarn(string message)
    {
        LogWithLevel(LogLevel.WARN, message);
    }

    public void LogError(string message)
    {
        LogWithLevel(LogLevel.ERROR, message);
    }

    public void LogFatal(string message)
    {
        LogWithLevel(LogLevel.FATAL, message);
    }
}
