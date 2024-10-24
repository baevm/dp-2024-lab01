namespace SingletonLogger;

public interface IFormatMessageStrategy
{
    string FormatMessage(string date, LogLevel level, string message);
}

public class UppercaseMessageStrategy : IFormatMessageStrategy
{
    public string FormatMessage(string date, LogLevel level, string message)
    {
        return $"{date} [{level}] {message.ToUpper()}";
    }
}

public class LowercaseMessageStrategy : IFormatMessageStrategy
{
    public string FormatMessage(string date, LogLevel level, string message)
    {
        return $"{date} [{level}] {message}";
    }
}