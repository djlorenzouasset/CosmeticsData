using System.Runtime.CompilerServices;

namespace CosmeticsData.Utils;

public static class Logger
{
    private static readonly object ObjectLooking = new();
    private static readonly Queue<Action> LogMessage = new();
    private static readonly DateTime DT;
    private static bool loggerThread = true;
    private static DateTime currentDate;

    static Logger()
    {
        DT = currentDate = DateTime.Now;
        initilizeThread();
    }

    private enum LoggerLevel
    {
        Process,
        Path,
        GarbageCollector,
        Info,
        Error,
        Success
    }

    public static void InitilizeInfo(string msg, [CallerMemberName] string callingMethodName = default) =>
        QueueAction(() => Log(LoggerLevel.Info, msg, DateTime.Now, callingMethodName));
    public static void InitilizeSuccess(string msg, [CallerMemberName] string callingMethodName = default) =>
        QueueAction(() => Log(LoggerLevel.Success, msg, DateTime.Now, callingMethodName));
    public static void InitilizePath(string msg, [CallerMemberName] string callingMethodName = default) =>
        QueueAction(() => Log(LoggerLevel.Path, msg, DateTime.Now, callingMethodName));
    public static void InitilizeProccess(string msg, [CallerMemberName] string callingMethodName = default) =>
        QueueAction(() => Log(LoggerLevel.Process, msg, DateTime.Now, callingMethodName));
    public static void InitilizeError(string msg, [CallerMemberName] string callingMethodName = default) =>
        QueueAction(() => Log(LoggerLevel.Error, msg, DateTime.Now, callingMethodName));

    public static void Time() => QueueAction(() =>
    {
        Console.WriteLine($"All tasks were finished in {Convert.ToInt32((DateTime.Now - DT).TotalSeconds)}seconds");
        Thread.Sleep(100);
        loggerThread = false;
    });
    
    private static void QueueAction(Action action)
    {
        lock (ObjectLooking)
        {
            LogMessage.Enqueue(action);
        }
    }

    public static void initilizeThread()
    {
        Thread loggerThread = new(() =>
        {
            while (Logger.loggerThread)
            {
                lock (ObjectLooking)
                {
                    if (LogMessage.TryDequeue(out Action logInvoke))
                    {
                        logInvoke?.Invoke();
                    }
                }
            }
        });

        loggerThread.Start();
    }
    
    private static void Log(LoggerLevel state, string content, DateTime dateTime, string methodName)
    {
        string toWrite = state == LoggerLevel.Info ? $"[{dateTime:G}]" : $"[{dateTime:G} ({Convert.ToInt32((dateTime - currentDate).TotalMilliseconds)}ms)]";
        currentDate = dateTime;
        Console.ForegroundColor = state switch
        {
            LoggerLevel.Process => ConsoleColor.DarkCyan,
            LoggerLevel.Info => ConsoleColor.Cyan,
            LoggerLevel.Error => ConsoleColor.Red,
            LoggerLevel.Path => ConsoleColor.Yellow,
            LoggerLevel.GarbageCollector => ConsoleColor.Cyan,
            LoggerLevel.Success => ConsoleColor.Green,
            _ => throw new ArgumentException("Unsupported LoggerLevel")
        };
        Console.WriteLine($"{toWrite} [{state}] ({methodName}) {content}");
        Console.ForegroundColor = ConsoleColor.White;
    }
}