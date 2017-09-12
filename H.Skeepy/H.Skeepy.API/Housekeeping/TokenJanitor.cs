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

        public Task Clean()
        {
            return Task.CompletedTask;
        }
    }
}
