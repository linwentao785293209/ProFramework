namespace ProFramework
{
    /// <summary>
    /// 关闭日志配置类，实现了日志配置接口
    /// </summary>
    public class ProLogCloseConfig : IProLogConfig
    {
        /// <summary>
        /// 获取关闭日志级别为错误级别
        /// </summary>
        public EProLogLevel LogLevel => EProLogLevel.Error;

        /// <summary>
        /// 获取关闭日志
        /// </summary>
        public bool IsLogEnabled => false;
    }
}