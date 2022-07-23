using Bygdrift.Tools.LogTool.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Bygdrift.Tools.LogTool
{
    /// <summary>
    /// Handle all logs. With this log, you can read the log - you cannot do that in a nomral ILogger
    /// </summary>
    public partial class Log
    {
        /// <summary>
        /// Create a log
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="stack"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2254:Template should be a static expression", Justification = "<Pending>")]
        private LogModel Add(LogType logType, StackTrace stack, Exception exception, string message, params object[] args)
        {
            var log = new LogModel(logType, stack, message, exception, args);
            _logs.Add(log);
            Debug.WriteLine(logType + ": " + log.Message());
            if (logType == LogType.Critical)
                Logger?.LogCritical(exception, log.Message());
            else if (logType == LogType.Error)
                Logger?.LogError(exception, log.Message());
            else if (logType == LogType.Information)
                Logger?.LogInformation(exception, log.Message());
            else if (logType == LogType.Warning)
                Logger?.LogWarning(exception, log.Message());

            return log;
        }

        /// <summary>
        /// Create a log
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        public LogModel Add(LogType logType, string message)
        {
            return Add(logType, new StackTrace(), null, message, null, null);
        }

        /// <summary>
        /// Create a log
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public LogModel Add(LogType logType, string message, params object[] args)
        {
            return Add(logType, new StackTrace(), null, message, null, args);
        }

        /// <summary>
        /// Create a log information
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogInformation(string message, params object[] args)
        {
            Add(LogType.Information, new StackTrace(), null, message, args);
        }

        /// <summary>
        /// Create a log information
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogInformation(Exception exception, string message, params object[] args)
        {
            Add(LogType.Information, new StackTrace(), exception, message, args);
        }

        /// <summary>
        /// Create a log warning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogWarning(string message, params object[] args)
        {
            Add(LogType.Warning, new StackTrace(), null, message, args);
        }

        /// <summary>
        /// Create a log warning
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogWarning(Exception exception, string message, params object[] args)
        {
            Add(LogType.Warning, new StackTrace(), exception, message, args);
        }

        /// <summary>
        /// Create a log error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogError(string message, params object[] args)
        {
            Add(LogType.Error, new StackTrace(), null, message, args);
        }

        /// <summary>
        /// Create a log error
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogError(Exception exception, string message, params object[] args)
        {
            Add(LogType.Error, new StackTrace(), exception, message, args);
        }

        /// <summary>
        /// Create a log critical. This also throws an error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogCritical(string message, params object[] args)
        {
            var log = Add(LogType.Critical, new StackTrace(), null, message, args);
            throw new Exception($"App log has thrown a critical error and stops. The error: {log.Message}");
        }

        /// <summary>
        /// Create a log critical
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogCritical(Exception exception, string message, params object[] args)
        {
            var log = Add(LogType.Critical, new StackTrace(), exception, message, args);
            throw new Exception($"App log has thrown a critical error and stops. The error: {log.Message}");
        }
    }
}
