namespace SingletonLogger;

public sealed class Logger
{
    private static StreamWriter _Writer = new StreamWriter(Console.OpenStandardOutput());
    private static readonly Mutex _Mutex = new Mutex();

    // вызывается при загрузке класса в память
    private static readonly Logger _instance = new Logger();
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
        _Mutex.WaitOne();
        _Writer = streamWriter;
        _Mutex.ReleaseMutex();
    }

    private void LogWithLevel(LogLevel level, string message)
    {
        _Mutex.WaitOne();

        var date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        _Writer.WriteLine($"{date} [{level}] {message}");
        _Writer.Flush();

        _Mutex.ReleaseMutex();
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
