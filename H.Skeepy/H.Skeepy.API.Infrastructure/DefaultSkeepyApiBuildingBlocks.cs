using H.Skeepy.API.Authentication;
using H.Skeepy.API.Authentication.Storage;
using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.API.Contracts.Housekeeping;
using H.Skeepy.API.Contracts.Notifications;
using H.Skeepy.API.Contracts.Registration;
using H.Skeepy.API.Housekeeping;
using H.Skeepy.API.Notifications;
using H.Skeepy.API.Registration.Storage;
using H.Skeepy.Core.Storage;
using H.Skeepy.Core.Storage.Individuals;
using H.Skeepy.Model;
using Nancy.TinyIoc;
using NLog;
using System;

namespace H.Skeepy.API.Infrastructure
{
    public static class DefaultSkeepyApiBuildingBlocks
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public static readonly Func<ICanGenerateTokens<string>> TokenGenerator = () => new JsonWebTokenGenerator(TimeSpan.FromHours(24));
        public static readonly Func<ICanManageSkeepyStorageFor<Token>> TokenStorage = () => new InMemoryTokensStore();
        public static readonly Func<ICanManageSkeepyStorageFor<RegisteredUser>> UserStore = () => new InMemoryRegistrationStore();
        public static readonly Func<ICanManageSkeepyStorageFor<Individual>> SkeepyIndividualStore = () => new InMemoryIndividualsStore();
        public static readonly Func<ICanStoreSkeepy<Credentials>> CredentialStore = () => new InMemoryCredentialsStore();
        public static readonly Func<ICanNotify> Notifier = () => new EmailNotifier();

        public static void RegisterWithTinyIoc(TinyIoCContainer container)
        {
            container.Register(TokenGenerator());
            container.Register(TokenStorage());
            container.Register(UserStore());
            container.Register(SkeepyIndividualStore());
            container.Register(CredentialStore());
            container.Register(Notifier());
            container.RegisterMultiple<ImAJanitor>(new[] { typeof(TokenJanitor), typeof(RegistrationJanitor) });

            log.Info("Registered Skeepy with Default Building Blocks");

#if (!DEBUG)
            AzureSkeepyApiBuildingBlocks.RegisterWithTinyIoc(container);
#endif
        }

        public static void RegisterWithTinyIoc()
        {
            RegisterWithTinyIoc(TinyIoCContainer.Current);
        }
    }
}
