using System;

namespace SingletonLogger;

public interface IMessageCaseStrategy
{
    void Log(string date, LogLevel level, string message, StreamWriter writer);
}

public class UppercaseMessageStrategy : IMessageCaseStrategy
{
    public void Log(string date, LogLevel level, string message, StreamWriter writer)
    {
        writer.WriteLine($"{date} [{level}] {message.ToUpper()}");
        writer.Flush();
    }
}

public class LowercaseMessageStrategy : IMessageCaseStrategy
{
    public void Log(string date, LogLevel level, string message, StreamWriter writer)
    {
        writer.WriteLine($"{date} [{level}] {message}");
        writer.Flush();
    }
}