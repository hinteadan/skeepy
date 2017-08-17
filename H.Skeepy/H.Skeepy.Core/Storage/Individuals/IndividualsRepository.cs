using H.Skeepy.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Core.Storage.Individuals
{
    public class IndividualsRepository : IDisposable
    {
        private const int defaultCount = 100;

        private readonly ICanManageSkeepyStorageFor<Individual> storage;
        private readonly ConcurrentDictionary<string, Individual> inMemoryIndividuals = new ConcurrentDictionary<string, Individual>();

        public IndividualsRepository(ICanManageSkeepyStorageFor<Individual> storage)
        {
            this.storage = storage ?? throw new InvalidOperationException("The repository must have an underlying storage");
        }

        public void Dispose()
        {
            storage.Dispose();
        }

        public Task<IEnumerable<Individual>> All(int count, int from)
        {
            return storage.Get().ContinueWith(t => t.Result.Skip(from).Take(count).Select(LoadIndividual));
        }

        public Task<IEnumerable<Individual>> All()
        {
            return All(defaultCount, 0);
        }

        public Task<IEnumerable<Individual>> All(int count)
        {
            return All(count, 0);
        }

        public Task Save(Individual individual)
        {
            return storage
                .Put(individual)
                .ContinueWith(t =>
                {
                    inMemoryIndividuals.AddOrUpdate(individual.Id, individual, (x, y) => individual);
                });
        }

        private Individual LoadIndividual(LazyEntity<Individual> lazyGuy)
        {
            if (inMemoryIndividuals.ContainsKey(lazyGuy.Summary.Id))
            {
                return inMemoryIndividuals[lazyGuy.Summary.Id];
            }

            var guy = lazyGuy.Full;
            return inMemoryIndividuals.AddOrUpdate(lazyGuy.Summary.Id, guy, (x, y) => guy);
        }
    }
}
