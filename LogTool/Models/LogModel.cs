using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bygdrift.Tools.LogTool.Models
{
    /// <summary>
    /// Handles where calls comes from like Namspace.Class.Method
    /// </summary>
    public class LogModel
    {
        /// <summary>
        /// The model - primary used internal
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="stack"></param>
        /// <param name="messageTemplate"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public LogModel(LogType logType, StackFrame stack, string messageTemplate, Exception exception, params object[] args)
        {
            LogType = logType;
            MessageTemplate = messageTemplate;
            Exception = exception;
            Caller = new Caller(stack);

            if (args != null && args.Any())
                Arguments = args.ToList();
        }

        /// <summary>
        /// Handles where calls comes from like Namspace.Class.Method
        /// </summary>
        private Caller Caller { get; set; }

        /// <summary>
        /// Eventual arugments that followed the log
        /// </summary>
        public List<object> Arguments { get; }

        /// <summary>
        /// Eventual exceptions
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// The template that builds up the log. An log with this template: log.LogError("-{A}- {B}-", "a", 1), will produce "-a- 1-";
        /// </summary>
        public string MessageTemplate { get; }

        /// <summary>
        /// The type of log - is it an information or error etc.
        /// </summary>
        public LogType LogType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callerPath">If null or empty, then true.</param>
        public bool ContainsCallerPath(string callerPath)
        {
            if (string.IsNullOrEmpty(callerPath))
                return true;

            return Caller.FullCallerPath.ToUpper().StartsWith(callerPath.ToUpper());
        }

        /// <summary>
        /// The message in the log
        /// </summary>
        public string Message(CallerPath callerPath = CallerPath.None)
        {
            var res = ReplaceBracketContentWithArgs(MessageTemplate, Arguments);
            return callerPath != CallerPath.None ? Caller.GetCallerPath(callerPath) + ": " + res : res;
        }

        private string CreateMessage(bool includeNamespace, bool includeClassName, bool includeMethodName)
        {
            var path = (includeNamespace ? Caller.NamespaceName : string.Empty) + (includeClassName ? Caller.ClassName : string.Empty) + (includeMethodName ? "." + Caller.MethodName : string.Empty);
            var res = ReplaceBracketContentWithArgs(MessageTemplate, Arguments);
            return !string.IsNullOrEmpty(path) ? path + ": " + res : res;
        }

        private string ReplaceBracketContentWithArgs(string input, List<object> args = null)
        {
            if (args == null)
                return input;

            var expression = new Regex(@"\{.*?\}");
            var matches = expression.Matches(input);
            var res = new StringBuilder();
            var start = 0;
            for (int i = 0; i < matches.Count; i++)
            {
                res.Append(input[start..matches[i].Index]);  //The sam as res.Append(input.Substring(start, matches[i].Index - start));
                if (args != null && args.Count - 1 >= i)
                    res.Append(args[i]);
                else
                    res.Append(matches[i].Value);

                start = matches[i].Index + matches[i].Length;
            }
            if (start < input.Length)
                res.Append(input[start..]);  //The same as res.Append(input.Substring(start, input.Length - start));

            return res.ToString();
        }
    }
}
