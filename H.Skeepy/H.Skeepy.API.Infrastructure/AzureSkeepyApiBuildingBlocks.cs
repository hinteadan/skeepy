using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.API.Contracts.Registration;
using H.Skeepy.Azure.Storage;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;
using Nancy.TinyIoc;
using NLog;
using System;

namespace H.Skeepy.API.Infrastructure
{
    public static class AzureSkeepyApiBuildingBlocks
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private static readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=hskeepy;AccountKey=lSntXcIU/ktONP3iPpte8MbxQkKDtTF2p7dU3vxZAGw3eM5mexaV25lZws2AV5U4ljpLueBQMMbZRAELV25kAg==;EndpointSuffix=core.windows.net";
        
        public static readonly Func<ICanManageSkeepyStorageFor<Token>> TokenStorage = () => new AzureTableStorageTokenStore(connectionString);
        public static readonly Func<ICanManageSkeepyStorageFor<RegisteredUser>> UserStore = () => new AzureTableStorageRegistrationStore(connectionString);
        public static readonly Func<ICanManageSkeepyStorageFor<Individual>> SkeepyIndividualStore = () => new AzureTableStorageIndividualsStore(connectionString);
        public static readonly Func<ICanStoreSkeepy<Credentials>> CredentialStore = () => new AzureTableStorageCredentialStore(connectionString);

        public static void RegisterWithTinyIoc(TinyIoCContainer container)
        {
            container.Register(TokenStorage());
            container.Register(UserStore());
            container.Register(SkeepyIndividualStore());
            container.Register(CredentialStore());

            log.Info("Registered Skeepy with Azure Building Blocks");
        }

        public static void RegisterWithTinyIoc()
        {
            RegisterWithTinyIoc(TinyIoCContainer.Current);
        }
    }
}
