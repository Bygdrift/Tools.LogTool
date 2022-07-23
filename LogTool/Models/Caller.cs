using System.Diagnostics;

namespace Bygdrift.Tools.LogTool.Models
{
    public class Caller
    {
        /// <summary>The name of the namespace that called this method</summary>
        public readonly string NamespaceName;

        /// <summary>The name of the class that called this method</summary>
        public readonly string ClassName;

        /// <summary>The name of the method that called this method</summary>
        public readonly string MethodName;

        private string _fullCallerPath;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stack"></param>
        public Caller(StackFrame stack)
        {
            var method = stack.GetMethod();
            NamespaceName = method.ReflectedType.Namespace;
            ClassName = method.ReflectedType.Name;
            MethodName = method.Name;
        }

        /// <summary>Namespace.Class</summary>
        public string NamespaceClassName { get { return NamespaceName + "." + ClassName; } }

        /// <summary>Namespace.Class.Method</summary>
        public string NamespaceClassMethodName { get { return NamespaceName + "." + ClassName + "." + MethodName; } }


        /// <summary>
        /// The message in the log
        /// </summary>
        public string GetCallerPath(CallerPath callerPath = CallerPath.None)
        {
            if (callerPath == CallerPath.NamespaceName) return NamespaceName;
            if (callerPath == CallerPath.NamespaceClassName) return NamespaceClassName;
            if (callerPath == CallerPath.NamespaceClassMethodName) return NamespaceClassMethodName;
            return string.Empty;
        }


        /// <summary>Get the full path to the calling method like: Namespace.Class.Method</summary>
        public string FullCallerPath
        {
            get
            {
                if (_fullCallerPath == null && !string.IsNullOrEmpty(NamespaceName))
                {
                    _fullCallerPath = NamespaceName;
                    if (!string.IsNullOrEmpty(ClassName))
                    {
                        _fullCallerPath += "." + ClassName;
                        if (!string.IsNullOrEmpty(MethodName))
                            _fullCallerPath += "." + MethodName;
                    }
                }
                return _fullCallerPath;
            }
        }
    }
}
