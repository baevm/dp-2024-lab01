using System;

namespace SingletonLogger;

public interface IFormatDateStrategy
{
    string GetDate();
}

public class DateWithTimeStrategy : IFormatDateStrategy
{
    public string GetDate()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}