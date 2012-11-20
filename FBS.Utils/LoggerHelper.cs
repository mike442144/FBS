using System;

namespace FBS.Utils
{
    /// <summary>
    /// 日志辅助类
    /// </summary>
    public class LoggerHelper
    {
        static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 普通信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="error">异常</param>
        public static void Info(object message, Exception error)
        {
            log.Info(message, error);
        }

        /// <summary>
        /// 普通信息
        /// </summary>
        /// <param name="message">消息</param>
        public static void Info(object message)
        {
            log.Info(message);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">消息</param>
        public static void Error(object message)
        {
            log.Error(message);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="error">异常</param>
        public static void Error(object message, Exception error)
        {
            log.Error(message, error);
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="error">异常</param>
        public static void Debug(object message, Exception error)
        {
            log.Debug(message, error);
        }
    }
}
