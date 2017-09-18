using CryptoHelper;
using H.Skeepy.Logging;
using NLog;
using System;

namespace H.Skeepy.API
{
    public sealed class Hasher
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public static string Hash(string value)
        {
            using (log.Timing($"Hash value of {value.Length} characters", LogLevel.Info))
            {
                return Crypto.HashPassword(value);
            }
        }

        public static bool Verify(string hash, string value)
        {
            using (log.Timing($"Verifying hash for value of {value.Length} characters", LogLevel.Info))
            {
                try
                {
                    return Crypto.VerifyHashedPassword(hash, value);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
