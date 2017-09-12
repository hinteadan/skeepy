using H.Skeepy.API.Authentication;
using H.Skeepy.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Housekeeping
{
    public class TokenJanitor
    {
        private readonly ICanManageSkeepyStorageFor<Token> tokenStore;

        public TokenJanitor(ICanManageSkeepyStorageFor<Token> tokenStore)
        {
            this.tokenStore = tokenStore ?? throw new InvalidOperationException($"Must provide a {nameof(tokenStore)}");
        }

        public async Task Clean()
        {
            foreach (var lazyToken in await tokenStore.Get())
            {
                if (!lazyToken.Full.HasExpired())
                {
                    continue;
                }

                await tokenStore.Zap(lazyToken.Summary.Id);
            }
        }
    }
}
