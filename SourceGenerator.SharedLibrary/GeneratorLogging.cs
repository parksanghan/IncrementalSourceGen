using System;
using System.Collections.Generic;
using System.IO;

namespace SourceGenerator.SharedLibrary;

public class GeneratorLogging
{
    private static readonly List<string> _logMessages = new List<string>();
    private static string? logFilePath = null;
    private static readonly object _lock = new();

    private static string logInitMessage = "[+] Generated Log File this file contains log messages from the source generator\n\n";
    private static LoggingLevel _loggingLevel = LoggingLevel.Info;
    
    public static void SetLoggingLevel(LoggingLevel level)
    {
        _loggingLevel = level;
    }
    
    public static void SetLogFilePath(string path)
    {
        logFilePath = path;
    }
    
    public static LoggingLevel GetLoggingLevel()
    {
        return _loggingLevel;
    }

    public static void LogMessage(string message, LoggingLevel messageLogLevel = LoggingLevel.Info)
    {
        lock (_lock)
        {
            try
            { 
                if (logFilePath is null)
                {
                    return;
                }
                if (File.Exists(logFilePath) is false)
                {
                    File.WriteAllText(logFilePath, logInitMessage);
                    File.AppendAllText(logFilePath, $"Logging started at {GetDateTimeUtc()}\n\n");
                }
                if (messageLogLevel < _loggingLevel)
                {
                    return;
                }
                string _logMessage = message + "\n";
                if (messageLogLevel > LoggingLevel.Info)
                {
                    _logMessage = $"[{messageLogLevel} start]\n" + _logMessage + $"[{messageLogLevel} end]\n\n";
                }
                if (!_logMessages.Contains(_logMessage))
                {
                    File.AppendAllText(logFilePath, _logMessage);
                    _logMessages.Add(_logMessage);
                }
            }
            catch (Exception ex)
            {
                if (logFilePath is null)
                {
                    return;
                }
                File.AppendAllText(logFilePath, $"[-] Exception occurred in logging: {ex.Message} \n");
            }
        }
    }
    
    public static void EndLogging()
    {
        if (logFilePath is null)
        {
            return;
        }
        if (File.Exists(logFilePath))
        {
            File.AppendAllText(logFilePath, $"[+] Logging ended at {GetDateTimeUtc()}\n");
        }
    }
    
    public static string GetDateTimeUtc()
    {
        return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
    }
}

public enum LoggingLevel
{
    Trace,
    Debug,
    Info,
    Warning,
    Error,
    Fatal
}