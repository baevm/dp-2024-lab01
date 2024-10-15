
using System.Text;

namespace SingletonLogger.Tests;

public class LoggerTest
{
    [Fact]
    public void Test_singleton_same_instance()
    {
        var instance1 = Logger.Instance;
        var instance2 = Logger.Instance;

        Assert.True(instance1.Equals(instance2));
    }

    [Fact]
    public void Test_Logger_Println()
    {
        /* mocks */
        var mockStringWriter = new MemoryStream();
        var streamWriter = new StreamWriter(mockStringWriter);

        Logger.SetOutput(streamWriter);

        var expected = "Hello world!";
        var instance1 = Logger.Instance;

        instance1.LogInfo(expected);

        var actual = Encoding.UTF8.GetString(mockStringWriter.ToArray());

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void Test_Logger_Thread_Safe()
    {
        /* mocks */
        var mockStringWriter = new MemoryStream();
        var streamWriter = new StreamWriter(mockStringWriter);

        Logger.SetOutput(streamWriter);

        var numOfThreads = 5;
        var countdown = new CountdownEvent(numOfThreads);

        for (int i = 0; i < numOfThreads; i++)
        {
            new Thread(delegate ()
            {
                var instance1 = Logger.Instance;
                instance1.LogInfo($"thread {Thread.CurrentThread.ManagedThreadId}");
                countdown.Signal();
            }).Start();
        }

        countdown.Wait();

        var actual = Encoding.UTF8.GetString(mockStringWriter.ToArray()).Split('\n');

        // 5 строк + 1 пустая
        Assert.Equal(numOfThreads + 1, actual.Length);
    }
}