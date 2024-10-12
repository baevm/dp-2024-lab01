
using System.Text;

namespace SingletonLogger.Tests;

public class LoggerTest
{
    [Fact]
    public void Test_singleton_same_instance()
    {
        var instance1 = Logger.Instance;
        var instance2 = Logger.Instance;

        Assert.Same(instance1, instance2);
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

        instance1.Logln(expected);

        var actual = Encoding.UTF8.GetString(mockStringWriter.ToArray());

        Assert.Contains(expected, actual);
    }
}