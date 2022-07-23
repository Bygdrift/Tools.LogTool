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
        /// <summary>Add logs</summary>
        public Log Add(LogModel log)
        {
            _logs.Add(log);
            Debug.WriteLine(log.LogType + ": " + log.Message());
            if (log.LogType == LogType.Critical)
                Logger?.LogCritical(log.Exception, log.Message());
            else if (log.LogType == LogType.Error)
                Logger?.LogError(log.Exception, log.Message());
            else if (log.LogType == LogType.Information)
                Logger?.LogInformation(log.Exception, log.Message());
            else if (log.LogType == LogType.Warning)
                Logger?.LogWarning(log.Exception, log.Message());

            return this;
        }

        /// <summary>Add logs</summary>
        public Log Add(Log log)
        {
            foreach (var item in log.Logs)
                Add(item);

            return this;
        }

        /// <summary>Add logs</summary>
        public Log Add(LogType logType, Exception exception, string message, params object[] args)
        {
            return Add(new LogModel(logType, new StackTrace().GetFrame(1), message, exception, args));
        }

        /// <summary>Add logs</summary>
        public Log Add(LogType logType, string message)
        {
            return Add(new LogModel(logType, new StackTrace().GetFrame(1), message, null, null));
        }

        /// <summary>Add logs</summary>
        public Log Add(LogType logType, string[] messages)
        {
            if (messages != null)
            {
                var stack = new StackTrace().GetFrame(1);
                foreach (var item in messages)
                    Add(new LogModel(logType, stack, item, null, null));
            }

            return this;
        }

        /// <summary>Add logs</summary>
        public Log Add(LogType logType, string message, params object[] args)
        {
            return Add(new LogModel(logType, new StackTrace().GetFrame(1), message, null, args));
        }

        /// <summary>
        /// Create a log information
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogInformation(string message, params object[] args)
        {
            Add(new LogModel(LogType.Information, new StackTrace().GetFrame(1), message, null, args));
        }

        /// <summary>
        /// Create a log information
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogInformation(Exception exception, string message, params object[] args)
        {
            Add(new LogModel(LogType.Information, new StackTrace().GetFrame(1), message, exception, args));
        }

        /// <summary>
        /// Create a log warning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogWarning(string message, params object[] args)
        {
            Add(new LogModel(LogType.Warning, new StackTrace().GetFrame(1), message, null, args));
        }

        /// <summary>
        /// Create a log warning
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogWarning(Exception exception, string message, params object[] args)
        {
            Add(new LogModel(LogType.Warning, new StackTrace().GetFrame(1), message, exception, args));
        }

        /// <summary>
        /// Create a log error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogError(string message, params object[] args)
        {
            Add(new LogModel(LogType.Error, new StackTrace().GetFrame(1), message, null, args));
        }

        /// <summary>
        /// Create a log error
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogError(Exception exception, string message, params object[] args)
        {
            Add(new LogModel(LogType.Error, new StackTrace().GetFrame(1), message, exception, args));
        }

        /// <summary>
        /// Create a log critical. This also throws an error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogCritical(string message, params object[] args)
        {
            var logModel = new LogModel(LogType.Critical, new StackTrace().GetFrame(1), message, null, args);
            Add(logModel);
            throw new Exception($"App log has thrown a critical error and stops. The error: {logModel.Message()}");
        }

        /// <summary>
        /// Create a log critical
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogCritical(Exception exception, string message, params object[] args)
        {
            var logModel = new LogModel(LogType.Critical, new StackTrace().GetFrame(1), message, exception, args);
            Add(logModel);
            throw new Exception($"App log has thrown a critical error and stops. The error: {logModel.Message()}");
        }
    }
}
