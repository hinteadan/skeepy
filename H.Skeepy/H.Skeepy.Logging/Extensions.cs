using NLog;

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
    }
}
