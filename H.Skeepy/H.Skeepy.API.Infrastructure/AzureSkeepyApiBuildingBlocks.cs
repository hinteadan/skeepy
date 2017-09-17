using H.Skeepy.API.Authentication;
using H.Skeepy.API.Authentication.Storage;
using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.API.Contracts.Notifications;
using H.Skeepy.API.Contracts.Registration;
using H.Skeepy.API.Notifications;
using H.Skeepy.API.Registration.Storage;
using H.Skeepy.Azure.Storage;
using H.Skeepy.Core.Storage;
using H.Skeepy.Core.Storage.Individuals;
using H.Skeepy.Model;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Infrastructure
{
    public static class AzureSkeepyApiBuildingBlocks
    {
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
        }

        public static void RegisterWithTinyIoc()
        {
            RegisterWithTinyIoc(TinyIoCContainer.Current);
        }
    }
}
