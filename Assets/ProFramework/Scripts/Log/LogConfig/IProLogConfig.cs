namespace ProFramework
{
    /// <summary>
    /// 日志配置接口
    /// </summary>
    public interface IProLogConfig
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        EProLogLevel LogLevel { get; }

        /// <summary>
        /// 是否启用日志
        /// </summary>
        bool IsLogEnabled { get; }
    }
}