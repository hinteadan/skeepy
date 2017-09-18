using NLog;
using System;

namespace H.Skeepy.Logging
{
    public static class Extensions
    {
        public static LogTiming Timing(this Logger log, string actionName, LogLevel logLevel)
        {
            return new LogTiming(log, actionName, logLevel);
        }

        public static LogTiming Timing(this Logger log, string actionName)
        {
            return new LogTiming(log, actionName);
        }

        public static T AndLog<T>(this T instance, Action<T> logAction)
        {
            logAction(instance);
            return instance;
        }
    }
}
