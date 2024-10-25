namespace ProFramework
{
    /// <summary>
    /// Info级日志配置
    /// </summary>
    internal class ProLogInfoConfig : IProLogConfig
    {
        public EProLogLevel LogLevel => EProLogLevel.Info;

        public bool IsLogEnabled => true;
    }
}