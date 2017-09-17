using H.Skeepy.API.Authentication;
using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.API.Contracts.Housekeeping;
using H.Skeepy.API.Contracts.Registration;
using H.Skeepy.API.Registration.Storage;
using H.Skeepy.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Housekeeping
{
    public class RegistrationJanitor : ImAJanitor
    {
        private readonly ICanManageSkeepyStorageFor<Token> tokenStore;
        private readonly ICanManageSkeepyStorageFor<RegisteredUser> userStore;

        public RegistrationJanitor(ICanManageSkeepyStorageFor<Token> tokenStore, ICanManageSkeepyStorageFor<RegisteredUser> userStore)
        {
            this.tokenStore = tokenStore ?? throw new InvalidOperationException($"Must provide a {nameof(tokenStore)}");
            this.userStore = userStore ?? throw new InvalidOperationException($"Must provide a {nameof(userStore)}");
        }

        public async Task Clean()
        {
            var tokens = await FetchTokens();

            foreach (var user in (await userStore.Get()).Select(x => x.Full))
            {
                if (user.IsConfirmed()) continue;

                var token = tokens.SingleOrDefault(x => x.UserId == user.Id);

                if (token != null && !token.HasExpired()) continue;

                await userStore.Zap(user.Id);
            }
        }

        private async Task<Token[]> FetchTokens()
        {
            return (await tokenStore.Get()).Select(x => x.Full).ToArray();
        }
    }
}
