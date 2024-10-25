namespace ProFramework
{
    /// <summary>
    /// 日志级别枚举
    /// </summary>
    public enum EProLogLevel
    {
        /// <summary>
        /// 调试级别 开发过程中输出详细的调试信息
        /// </summary>
        Debug,

        /// <summary>
        /// 信息级别 记录正常的程序运行信息
        /// </summary>
        Info,

        /// <summary>
        /// 警告级别 出现了一些不期望的情况
        /// </summary>
        Warning,

        /// <summary>
        /// 错误级别 运行中出现的错误情况
        /// </summary>
        Error
    }
}