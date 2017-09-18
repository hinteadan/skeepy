using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.API.Contracts.Housekeeping;
using H.Skeepy.Core.Storage;
using H.Skeepy.Logging;
using NLog;
using System;
using System.Threading.Tasks;

namespace H.Skeepy.API.Housekeeping
{
    public class TokenJanitor : ImAJanitor
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private readonly ICanManageSkeepyStorageFor<Token> tokenStore;

        public TokenJanitor(ICanManageSkeepyStorageFor<Token> tokenStore)
        {
            this.tokenStore = tokenStore ?? throw new InvalidOperationException($"Must provide a {nameof(tokenStore)}");
        }

        public async Task Clean()
        {
            using (log.Timing($"Housekeeping tokens", LogLevel.Info))
            {
                foreach (var lazyToken in await tokenStore.Get())
                {
                    if (!lazyToken.Full.HasExpired())
                    {
                        continue;
                    }

                    using (log.Timing($"Zap token {lazyToken.Summary.Id} because it has expired", LogLevel.Info))
                    {
                        await tokenStore.Zap(lazyToken.Summary.Id);
                    }
                }
            }
        }
    }
}
