using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using H.Skeepy.API.Registration.Storage;
using H.Skeepy.Core.Storage;
using H.Skeepy.Azure.Storage;
using H.Skeepy.API.Contracts.Registration;
using FluentAssertions;

namespace H.Skeepy.Testicles.Core.Storage.Registration
{
    [TestClass]
    public class AzureRegistrationStoreOperations : AzureStoreOperationsBase<RegisteredUser>
    {
        public AzureRegistrationStoreOperations()
            : base("SkeepyRegistrationTest", collection => new AzureTableStorageRegistrationStore(ConnectionString, collection))
        {
        }

        protected override RegisteredUser CreateModel()
        {
            return FakeData.GenerateRegisteredUser();
        }

        [TestMethod]
        public void AzureRegistrationStore_CanLoadARegisteredUserWithNullDetails()
        {
            var applicant = new ApplicantDto
            {
                Email = "hintee@skeepy.ro",
                FirstName = "Hintee",
                FacebookDetails = null,
            };
            store.Put(new RegisteredUser(applicant));
            store.Get(applicant.Email).ShouldBeEquivalentTo(new RegisteredUser(applicant));
        }
    }
}
