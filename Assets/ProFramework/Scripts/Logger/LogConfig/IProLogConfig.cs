namespace ProFramework
{
    /// <summary>
    /// 日志配置接口
    /// </summary>
    internal interface IProLogConfig
    {
        /// <summary>
        /// 获取或设置日志级别
        /// </summary>
        EProLogLevel LogLevel { get; }

        /// <summary>
        /// 获取或设置是否启用日志
        /// </summary>
        bool IsLogEnabled { get; }
    }
}