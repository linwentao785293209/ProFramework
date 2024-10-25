namespace ProFramework
{
    /// <summary>
    /// Warning级日志配置
    /// </summary>
    internal class ProLogWarningConfig : IProLogConfig
    {
        public EProLogLevel LogLevel => EProLogLevel.Warning;

        public bool IsLogEnabled => true;
    }
}