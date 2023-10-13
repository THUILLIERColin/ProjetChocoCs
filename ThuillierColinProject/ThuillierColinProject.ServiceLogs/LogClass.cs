namespace ThuillierColinProject.ServiceLogs;

using NLog;
public class LogClass: Logger
{
    public enum TypeMessage { Info, Warn, Error, Fatal };
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    public LogClass()
    {
        var config = new NLog.Config.LoggingConfiguration();

        // Targets where to log to: File and Console
        var logfile = new NLog.Targets.FileTarget("logfile") { FileName = @"./Data/logs/" + DateTime.Now.ToString("yyyy-M-d HH:mm:ss") + ".log" };

        // Rules for mapping loggers to targets            
        config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);

        // Apply config           
        NLog.LogManager.Configuration = config;
    }

    public bool Log(string message, TypeMessage type)
    {
        switch (type)
        {
            case TypeMessage.Info:
                Logger.Info(message);
                break;
            case TypeMessage.Warn:
                Logger.Warn(message);
                break;
            case TypeMessage.Error:
                Logger.Error(message);
                break;
            case TypeMessage.Fatal:
                Logger.Fatal(message);
                break;
        }
        return true;
    }
    
}