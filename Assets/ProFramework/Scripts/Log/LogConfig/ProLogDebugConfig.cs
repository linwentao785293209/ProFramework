namespace ProFramework
{
    /// <summary>
    /// Debug级日志配置
    /// </summary>
    internal class ProLogDebugConfig : IProLogConfig
    {
        public EProLogLevel LogLevel => EProLogLevel.Debug;

        public bool IsLogEnabled => true;
    }
}