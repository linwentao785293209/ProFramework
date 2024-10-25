namespace ProFramework
{
    /// <summary>
    /// Error级日志配置
    /// </summary>
    internal class ProLogErrorConfig : IProLogConfig
    {
        public EProLogLevel LogLevel => EProLogLevel.Error;

        public bool IsLogEnabled => true;
    }
}