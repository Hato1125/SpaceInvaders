namespace SpaceInvaders.Logger;

internal static class Log
{
    private static StreamWriter? writer;

    public static void Initializing(string fileName)
    {
        var dire = Path.GetDirectoryName(fileName);
        if (!string.IsNullOrWhiteSpace(dire) && !Directory.Exists(dire))
            Directory.CreateDirectory(dire);

        writer = new(fileName, true);
    }

    public static void Finalizing()
        => writer?.Dispose();

    public static void WriteFatal(string message)
        => WriteLog(Level.FATAL, message);

    public static void WriteError(string message)
        => WriteLog(Level.ERROR, message);

    public static void WriteWarning(string message)
        => WriteLog(Level.WARN, message);

    public static void WriteInfo(string message)
        => WriteLog(Level.INFO, message);

    public static void WriteDebug(string message)
        => WriteLog(Level.DEBUG, message);

    public static void WriteTrace(string message)
        => WriteLog(Level.TRACE, message);

    private static void WriteLog(Level level, string message)
    {
        string nowTime = DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss");
        string writeMessage = $"{level} {nowTime} {message}";

#if DEBUG
        Console.WriteLine(writeMessage);
#endif

        writer?.WriteLine(writeMessage);
    }

    private enum Level
    {
        FATAL,
        ERROR,
        WARN,
        INFO,
        DEBUG,
        TRACE,
    }
}