using H.Skeepy.API.Authentication;
using H.Skeepy.API.Registration.Storage;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Testicles.Core.Storage
{
    public static class FakeData
    {
        private static readonly Bogus.DataSets.Commerce commerceGenerator = new Bogus.DataSets.Commerce();
        private static readonly Bogus.DataSets.Name namesGenerator = new Bogus.DataSets.Name();
        private static readonly Bogus.DataSets.Address addressGenerator = new Bogus.DataSets.Address();
        private static readonly Bogus.DataSets.Internet internetGenerator = new Bogus.DataSets.Internet();
        private static readonly Bogus.Randomizer random = new Bogus.Randomizer();

        public static Individual GenerateIndividual()
        {
            var model = Individual.New(namesGenerator.FirstName(), namesGenerator.LastName());
            model.SetDetail("Rank", random.Int(0, 5000).ToString()).SetDetail("Country", addressGenerator.CountryCode());
            return model;
        }

        public static Party GenerateParty()
        {
            return Party.New(commerceGenerator.ProductName(), Enumerable.Range(0, random.Int(1, 10)).Select(x => GenerateIndividual()).ToArray());
        }

        public static RegisteredUser GenerateRegisteredUser()
        {
            return new RegisteredUser
            {
                Email = internetGenerator.Email(),
                FirstName = namesGenerator.FirstName(),
                LastName = namesGenerator.LastName(),
                SkeepyId = Guid.NewGuid().ToString(),
                Status = RegisteredUser.AccountStatus.PendingValidation
            };
        }

        public static Credentials GenerateCredentials()
        {
            return new Credentials(internetGenerator.UserName(), internetGenerator.Password());
        }

        public static void FillStorage(ICanManageSkeepyStorageFor<Individual> storage, int count = 1000)
        {
            Enumerable
                .Range(0, count)
                .AsParallel()
                .ForAll(x => storage.Put(GenerateIndividual()).Wait());
        }
    }
}
