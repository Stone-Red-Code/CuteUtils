using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CuteUtils.Logging;

/// <summary>
/// Class used for logging
/// </summary>
public class Logger
{
    /// <summary>
    /// Gets or sets the log configuration.
    /// </summary>
    public LogConfig Config { get; init; } = new LogConfig();

    /// <summary>
    /// Logs a message with the specified source, log severity, and additional caller information.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="source">The source of the log message.</param>
    /// <param name="logSeverity">The severity level of the log message.</param>
    /// <param name="memberName">The name of the calling member.</param>
    /// <param name="sourceFilePath">The path of the source file.</param>
    /// <param name="sourceLineNumber">The line number in the source file.</param>
    public void Log(string message, string source, LogSeverity logSeverity, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        WriteLog(message, source, logSeverity, memberName, sourceFilePath, sourceLineNumber);
    }

    /// <summary>
    /// Logs a message with the specified log severity and additional caller information.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="logSeverity">The severity level of the log message.</param>
    /// <param name="memberName">The name of the calling member.</param>
    /// <param name="sourceFilePath">The path of the source file.</param>
    /// <param name="sourceLineNumber">The line number in the source file.</param>
    public void Log(string message, LogSeverity logSeverity, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        WriteLog(message, string.Empty, logSeverity, memberName, sourceFilePath, sourceLineNumber);
    }

    /// <summary>
    /// Logs an informational message with the specified source and additional caller information.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="source">The source of the log message.</param>
    /// <param name="memberName">The name of the calling member.</param>
    /// <param name="sourceFilePath">The path of the source file.</param>
    /// <param name="sourceLineNumber">The line number in the source file.</param>
    public void LogInfo(string message, string source, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        WriteLog(message, source, LogSeverity.Info, memberName, sourceFilePath, sourceLineNumber);
    }

    /// <summary>
    /// Logs an informational message with additional caller information.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="memberName">The name of the calling member.</param>
    /// <param name="sourceFilePath">The path of the source file.</param>
    /// <param name="sourceLineNumber">The line number in the source file.</param>
    public void LogInfo(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        WriteLog(message, string.Empty, LogSeverity.Info, memberName, sourceFilePath, sourceLineNumber);
    }

    /// <summary>
    /// Logs a warning message with the specified source and additional caller information.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="source">The source of the log message.</param>
    /// <param name="memberName">The name of the calling member.</param>
    /// <param name="sourceFilePath">The path of the source file.</param>
    /// <param name="sourceLineNumber">The line number in the source file.</param>
    public void LogWarn(string message, string source, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        WriteLog(message, source, LogSeverity.Warn, memberName, sourceFilePath, sourceLineNumber);
    }

    /// <summary>
    /// Logs a warning message with additional caller information.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="memberName">The name of the calling member.</param>
    /// <param name="sourceFilePath">The path of the source file.</param>
    /// <param name="sourceLineNumber">The line number in the source file.</param>
    public void LogWarn(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        WriteLog(message, string.Empty, LogSeverity.Warn, memberName, sourceFilePath, sourceLineNumber);
    }

    /// <summary>
    /// Logs an error message with additional caller information.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="memberName">The name of the calling member.</param>
    /// <param name="sourceFilePath">The path of the source file.</param>
    /// <param name="sourceLineNumber">The line number in the source file.</param>
    public void LogError(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        WriteLog(message, string.Empty, LogSeverity.Error, memberName, sourceFilePath, sourceLineNumber);
    }

    /// <summary>
    /// Logs an error message with the specified source and additional caller information.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="source">The source of the log message.</param>
    /// <param name="memberName">The name of the calling member.</param>
    /// <param name="sourceFilePath">The path of the source file.</param>
    /// <param name="sourceLineNumber">The line number in the source file.</param>
    public void LogError(string message, string source, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        WriteLog(message, source, LogSeverity.Error, memberName, sourceFilePath, sourceLineNumber);
    }

    /// <summary>
    /// Logs a fatal error message with additional caller information.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="memberName">The name of the calling member.</param>
    /// <param name="sourceFilePath">The path of the source file.</param>
    /// <param name="sourceLineNumber">The line number in the source file.</param>
    public void LogFatal(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        WriteLog(message, string.Empty, LogSeverity.Fatal, memberName, sourceFilePath, sourceLineNumber);
    }

    /// <summary>
    /// Logs a fatal error message with the specified source and additional caller information.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="source">The source of the log message.</param>
    /// <param name="memberName">The name of the calling member.</param>
    /// <param name="sourceFilePath">The path of the source file.</param>
    /// <param name="sourceLineNumber">The line number in the source file.</param>
    public void LogFatal(string message, string source, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        WriteLog(message, source, LogSeverity.Fatal, memberName, sourceFilePath, sourceLineNumber);
    }

    /// <summary>
    /// Logs a debug message with additional caller information.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="memberName">The name of the calling member.</param>
    /// <param name="sourceFilePath">The path of the source file.</param>
    /// <param name="sourceLineNumber">The line number in the source file.</param>
    public void LogDebug(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        WriteLog(message, string.Empty, LogSeverity.Debug, memberName, sourceFilePath, sourceLineNumber);
    }

    /// <summary>
    /// Logs a debug message with the specified source and additional caller information.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="source">The source of the log message.</param>
    /// <param name="memberName">The name of the calling member.</param>
    /// <param name="sourceFilePath">The path of the source file.</param>
    /// <param name="sourceLineNumber">The line number in the source file.</param>
    public void LogDebug(string message, string source, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        WriteLog(message, source, LogSeverity.Debug, memberName, sourceFilePath, sourceLineNumber);
    }

    /// <summary>
    /// Logs a message with the specified source, log severity, and additional caller information if the condition is met.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="source">The source of the log message.</param>
    /// <param name="logSeverity">The severity level of the log message.</param>
    /// <param name="memberName">The name of the calling member.</param>
    /// <param name="sourceFilePath">The path of the source file.</param>
    /// <param name="sourceLineNumber">The line number in the source file.</param>
    public void LogIf(bool condition, string message, string source, LogSeverity logSeverity, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        if (condition)
        {
            WriteLog(message, source, logSeverity, memberName, sourceFilePath, sourceLineNumber);
        }
    }

    /// <summary>
    /// Logs a message with the specified log severity and additional caller information if the condition is met.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="logSeverity">The severity level of the log message.</param>
    /// <param name="memberName">The name of the calling member.</param>
    /// <param name="sourceFilePath">The path of the source file.</param>
    /// <param name="sourceLineNumber">The line number in the source file.</param>
    public void LogIf(bool condition, string message, LogSeverity logSeverity, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        if (condition)
        {
            WriteLog(message, string.Empty, logSeverity, memberName, sourceFilePath, sourceLineNumber);
        }
    }

    /// <summary>
    /// Clears the log file for the specified log severity.
    /// </summary>
    /// <param name="logSeverity">The severity level of the log messages to clear.</param>
    public void ClearLogFile(LogSeverity logSeverity)
    {
        OutputConfig outputConfig = GetOutputConfig(logSeverity);

        lock (outputConfig)
        {
            if (File.Exists(outputConfig.FilePath))
            {
                File.WriteAllText(outputConfig.FilePath, string.Empty);
            }
        }
    }

    private static string GetFormattedString(string format, LogSeverity logSeverity, string source, string message, string memberName, string sourceFilePath, int sourceLineNumber)
    {
        format = format
           .Replace(LogFormatType.DateTime, "0")
           .Replace(LogFormatType.LogSeverity, "1")
           .Replace(LogFormatType.LineNumber, "2")
           .Replace(LogFormatType.FilePath, "3")
           .Replace(LogFormatType.MemberName, "4")
           .Replace(LogFormatType.Source, "5")
           .Replace(LogFormatType.Message, "6");

        return string.Format(format, DateTime.Now, logSeverity.ToString().ToUpper(), sourceLineNumber, sourceFilePath, memberName, source, message);
    }

    private void WriteLog(string message, string source, LogSeverity logSeverity, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        string consoleOutput = GetFormattedString(Config.FormatConfig.ConsoleFormat, logSeverity, source, message, memberName, sourceFilePath, sourceLineNumber);
        string debugOutput = GetFormattedString(Config.FormatConfig.DebugConsoleFormat, logSeverity, source, message, memberName, sourceFilePath, sourceLineNumber);
        string fileOutput = GetFormattedString(Config.FormatConfig.FileFormat, logSeverity, source, message, memberName, sourceFilePath, sourceLineNumber);

        OutputConfig outputConfig = GetOutputConfig(logSeverity);

        if ((outputConfig.LogTarget & LogTarget.Console) == LogTarget.Console)
        {
            ConsoleExt.WriteLine(consoleOutput, outputConfig.ConsoleColor);
        }

        if ((outputConfig.LogTarget & LogTarget.DebugConsole) == LogTarget.DebugConsole)
        {
            Trace.WriteLine(debugOutput);
        }

        lock (outputConfig)
        {
            if ((outputConfig.LogTarget & LogTarget.File) == LogTarget.File)
            {
                if (!File.Exists(outputConfig.FilePath))
                {
                    File.Create(outputConfig.FilePath).Close();
                }

                File.AppendAllLines(outputConfig.FilePath, new[] { fileOutput });
            }
        }
    }

    private OutputConfig GetOutputConfig(LogSeverity logSeverity)
    {
        return logSeverity switch
        {
            LogSeverity.Fatal => Config.FatalConfig,
            LogSeverity.Error => Config.ErrorConfig,
            LogSeverity.Warn => Config.WarnConfig,
            LogSeverity.Info => Config.InfoConfig,
            _ => Config.DebugConfig
        };
    }
}
