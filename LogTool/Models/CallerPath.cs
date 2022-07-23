namespace Bygdrift.Tools.LogTool.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum CallerPath
    {
        /// The message doe not contain the path to the method that called the lod
        None,
        /// The message in the log, starting with the Namespace that send the log
        Namespace,
        /// The message in the log, starting with Namespace.Classname that send the log
        NamespaceClass,
        /// The message in the log, starting with Namepsace.Classname.MethodName that send the log
        NamespaceClassMethod
    }
}
