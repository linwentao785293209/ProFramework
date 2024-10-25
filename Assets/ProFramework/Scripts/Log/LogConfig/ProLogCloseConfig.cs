namespace ProFramework
{
    /// <summary>
    /// 关闭日志配置
    /// </summary>
    public class ProLogCloseConfig : IProLogConfig
    {
        public EProLogLevel LogLevel => EProLogLevel.Error;
        public bool IsLogEnabled => false;
    }
}