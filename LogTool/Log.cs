using Bygdrift.Tools.LogTool.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Bygdrift.Tools.LogTool
{
    /// <summary>
    /// Handle all logs. With this log, you can read the log - you cannot do that in a nomral ILogger
    /// </summary>
    public partial class Log
    {
        private readonly List<LogModel> _logs;

        /// <summary>
        /// The constructor
        /// </summary>
        public Log(ILogger logger = null)
        {
            _logs = new List<LogModel>();
            Logger = logger;
        }

        /// <summary>
        /// The saved logs
        /// </summary>
        public List<LogModel> Logs
        {
            get { return _logs; }
        }

        /// <summary>
        /// The formal logger
        /// </summary>
        public ILogger Logger { get; set; }
    }
}
