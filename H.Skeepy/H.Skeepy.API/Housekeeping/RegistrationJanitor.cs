using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.API.Contracts.Housekeeping;
using H.Skeepy.API.Contracts.Registration;
using H.Skeepy.Core.Storage;
using H.Skeepy.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H.Skeepy.API.Housekeeping
{
    public class RegistrationJanitor : ImAJanitor
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private readonly ICanManageSkeepyStorageFor<Token> tokenStore;
        private readonly ICanManageSkeepyStorageFor<RegisteredUser> userStore;

        public RegistrationJanitor(ICanManageSkeepyStorageFor<Token> tokenStore, ICanManageSkeepyStorageFor<RegisteredUser> userStore)
        {
            this.tokenStore = tokenStore ?? throw new InvalidOperationException($"Must provide a {nameof(tokenStore)}");
            this.userStore = userStore ?? throw new InvalidOperationException($"Must provide a {nameof(userStore)}");
        }

        public async Task Clean()
        {
            using (log.Timing($"Housekeeping user registrations", LogLevel.Info))
            {
                var tokens = await FetchTokens();

                foreach (var user in await FetchUsers())
                {
                    if (user.IsConfirmed()) continue;

                    var token = tokens.SingleOrDefault(x => x.UserId == user.Id);

                    if (token != null && !token.HasExpired()) continue;

                    using (log.Timing($"Zap registration application for {user.Id} because it was not confirmed and has expired", LogLevel.Info))
                    {
                        await userStore.Zap(user.Id);
                    }
                }
            }
        }

        private async Task<IEnumerable<RegisteredUser>> FetchUsers()
        {
            return (await userStore.Get()).Select(x => x.Full);
        }

        private async Task<Token[]> FetchTokens()
        {
            using (log.Timing("Fetch tokens for user registration housekeeping", LogLevel.Info))
            {
                return (await tokenStore.Get()).Select(x => x.Full).ToArray();
            }
        }
    }
}
