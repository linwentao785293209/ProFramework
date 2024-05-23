namespace ProFramework
{
    /// <summary>
    /// 默认日志配置类，实现了日志配置接口
    /// </summary>
    internal class ProLogDefaultConfig : IProLogConfig
    {
        /// <summary>
        /// 获取默认日志级别为调试级别
        /// </summary>
        public EProLogLevel LogLevel => EProLogLevel.Debug;

        /// <summary>
        /// 获取默认启用日志
        /// </summary>
        public bool IsLogEnabled => true;
    }
}