using System;
using NLog;

namespace H.Skeepy.Logging
{
    public class LogTiming : IDisposable
    {
        private readonly Logger log;
        private readonly LogLevel logLevel;
        private readonly string actionName;
        private DateTime startedAt = DateTime.MinValue;
        private DateTime endedAt = DateTime.MinValue;

        public LogTiming(Logger log, string actionName, LogLevel logLevel)
        {
            this.log = log ?? throw new InvalidOperationException("A logger must be provided");
            this.logLevel = logLevel;
            this.actionName = actionName;

            Start();
        }

        public LogTiming(Logger log, string actionName)
            : this(log, actionName, LogLevel.Debug)
        { }

        private void Start()
        {
            startedAt = DateTime.Now;
            log.Log(logLevel, "Running {0} ...", actionName);
        }

        private void Stop()
        {
            endedAt = DateTime.Now;
            log.Log(logLevel, "Finished {0} in {1}", actionName, endedAt - startedAt);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
