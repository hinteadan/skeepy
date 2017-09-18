using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public class GuidTokenGenerator : ICanGenerateTokens<Credentials>, ICanGenerateTokens<string>
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public Token Generate(Credentials payload)
        {
            return Generate(payload.Id);
        }

        public Token Generate(string payload)
        {
            using (log.Timing($"Generate new token for {payload}", LogLevel.Info))
            {
                return new Token(payload, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            }
        }
    }
}
