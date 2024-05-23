using System;
using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// 日志类，用于记录和输出日志信息
    /// </summary>
    public class ProLog
    {
        // 用于加锁的对象
        protected static readonly object lockObj = new object();

        // 日志配置对象
        private static IProLogConfig _logConfig;

        // 日志配置属性
        private static IProLogConfig LogConfig
        {
            get
            {
                // 如果日志配置对象为空，则进行初始化
                if (_logConfig == null)
                {
                    // 使用锁确保线程安全
                    lock (lockObj)
                    {
                        // 再次检查以避免竞态条件
                        if (_logConfig == null)
                        {
                            // 根据编译环境选择不同的默认日志配置
                            #if UNITY_EDITOR
                            _logConfig = new ProLogDefaultConfig();
                            #else
                            _logConfig = new ProLogCloseConfig();
                            #endif
                        }
                    }
                }

                return _logConfig;
            }
        }

        /// <summary>
        /// 格式化日志消息
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <param name="messages">日志信息</param>
        /// <returns>格式化后的日志消息</returns>
        private static string FormatMessage(EProLogLevel logLevel, params object[] messages) =>
            $"[{logLevel.ToString().ToUpper()}] {string.Join(" ", messages)}";

        /// <summary>
        /// 输出日志方法
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <param name="messages">日志信息</param>
        private static void Log(EProLogLevel logLevel, params object[] messages)
        {
            // 如果日志未启用或者日志级别低于当前配置级别，则直接返回
            if (!LogConfig.IsLogEnabled || logLevel < LogConfig.LogLevel)
                return;

            // 根据日志级别选择相应的输出方法
            switch (logLevel)
            {
                case EProLogLevel.Debug:
                    Debug.Log($"{FormatMessage(logLevel, messages)}");
                    break;
                case EProLogLevel.Info:
                    Debug.Log($"{FormatMessage(logLevel, messages)}");
                    break;
                case EProLogLevel.Warning:
                    Debug.LogWarning($"{FormatMessage(logLevel, messages)}");
                    break;
                case EProLogLevel.Error:
                    Debug.LogError($"{FormatMessage(logLevel, messages)}");
                    break;
                default:
                    Debug.LogWarning($"[Unsupported log logLevel: {logLevel}]");
                    break;
            }
        }

        /// <summary>
        /// 输出调试日志方法
        /// </summary>
        /// <param name="messages">日志信息</param>
        public static void LogDebug(params object[] messages) => Log(EProLogLevel.Debug, messages);

        /// <summary>
        /// 输出信息日志方法
        /// </summary>
        /// <param name="messages">日志信息</param>
        public static void LogInfo(params object[] messages) => Log(EProLogLevel.Info, messages);

        /// <summary>
        /// 输出警告日志方法
        /// </summary>
        /// <param name="messages">日志信息</param>
        public static void LogWarning(params object[] messages) => Log(EProLogLevel.Warning, messages);

        /// <summary>
        /// 输出错误日志方法
        /// </summary>
        /// <param name="messages">日志信息</param>
        public static void LogError(params object[] messages) => Log(EProLogLevel.Error, messages);
    }
}