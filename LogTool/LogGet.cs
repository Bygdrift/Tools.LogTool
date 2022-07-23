using Bygdrift.Tools.LogTool.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bygdrift.Tools.LogTool
{
    /// <summary>
    /// Handle all logs. With this log, you can read the log - you cannot do that in a nomral ILogger
    /// </summary>
    public partial class Log
    {
        /// <summary>
        /// Return all errors and critical logs
        /// </summary>
        /// <param name="callerPath">Only returns logs that conatains a path from the callerpath. Ignored if null or empty</param>
        /// <param name="includeCallerPath"></param>
        public IEnumerable<string> GetErrorsAndCriticals(string callerPath = null, CallerPath includeCallerPath = CallerPath.None)
        {
            foreach (var item in Logs.Where(o => (o.LogType == LogType.Error || o.LogType == LogType.Critical) && o.ContainsCallerPath(callerPath)))
                yield return item.Message(includeCallerPath);
        }

        /// <summary>
        /// Get all logs
        /// </summary>
        /// <param name="callerPath">Only returns logs that conatains a path from the callerpath. Ignored if null or empty</param>
        /// <param name="includeCallerPath">writes in from what class the log has been send</param>
        public IEnumerable<string> GetLogs(string callerPath = null, CallerPath includeCallerPath = CallerPath.None)
        {
            foreach (var item in Logs.Where(o => o.ContainsCallerPath(callerPath)))
                yield return item.Message(includeCallerPath);
        }

        /// <summary>
        /// Get all logs
        /// </summary>
        /// <param name="logType">What kind of log to return</param>
        /// <param name="callerPath">Only returns logs that conatains a path from the callerpath. Ignored if null or empty</param>
        /// <param name="includeCallerPath">writes in from what class the log has been send</param>
        public IEnumerable<string> GetLogs(LogType logType, string callerPath = null, CallerPath includeCallerPath = CallerPath.None)
        {
            foreach (var item in Logs.Where(o => o.LogType == logType && o.ContainsCallerPath(callerPath)))
                yield return item.Message(includeCallerPath);
        }

        /// <summary>
        /// If there are any errors in the log
        /// </summary>
        /// <param name="callerPath">Only returns logs that conatains a path from the callerpath. Ignored if null or empty</param>
        public bool HasErrorsOrCriticals(string callerPath = null)
        {
            return Logs.Any(o => (o.LogType == LogType.Error || o.LogType == LogType.Critical) && o.ContainsCallerPath(callerPath));
        }
    }
}
