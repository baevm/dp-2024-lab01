namespace SingletonLogger;


public sealed class Logger
{
    private static StreamWriter _streamWriter = new StreamWriter(Console.OpenStandardOutput());
    private static IFormatMessageStrategy _formatMessageStrategy = new LowercaseMessageStrategy();
    private static IFormatDateStrategy _formatDateStrategy = new DateWithTimeStrategy();

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

    /// <summary>
    /// Стратегия вывода логов
    /// </summary>
    /// <param name="streamWriter"></param>
    public static void SetOutputStrategy(StreamWriter streamWriter)
    {
        _Mutex.WaitOne();
        _streamWriter = streamWriter;
        _Mutex.ReleaseMutex();
    }

    /// <summary>
    /// Стратегия форматирования сообщения в логах
    /// </summary>
    /// <param name="logStrategy"></param>
    public static void SetMessageCaseStrategy(IFormatMessageStrategy logStrategy)
    {
        _Mutex.WaitOne();
        _formatMessageStrategy = logStrategy;
        _Mutex.ReleaseMutex();
    }

    /// <summary>
    /// Стратегия форматирования даты в лог сообщениях
    /// </summary>
    /// <param name="formatDateStrategy"></param>
    public static void SetFormatDateStrategy(IFormatDateStrategy formatDateStrategy)
    {
        _Mutex.WaitOne();
        _formatDateStrategy = formatDateStrategy;
        _Mutex.ReleaseMutex();
    }

    private void LogWithLevel(LogLevel level, string message)
    {
        _Mutex.WaitOne();

        var date = _formatDateStrategy.GetDate();
        var logMessage = _formatMessageStrategy.FormatMessage(date, level, message);

        _streamWriter.WriteLine(logMessage);
        _streamWriter.Flush();

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
