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
        private const int defaultCount = 1000;

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

        public Task<Individual> this[string id]
        {
            get
            {
                return LoadIndividual(id);
            }
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

        private Task<Individual> LoadIndividual(string id)
        {
            if (inMemoryIndividuals.ContainsKey(id))
            {
                var taskSource = new TaskCompletionSource<Individual>();
                taskSource.SetResult(inMemoryIndividuals[id]);
                return taskSource.Task;
            }

            return storage
                .Get(id)
                .ContinueWith(t => inMemoryIndividuals.AddOrUpdate(id, t.Result, (x, y) => t.Result));
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

        public Task Remove(string id)
        {
            return storage
                .Zap(id)
                .ContinueWith(t =>
                {
                    inMemoryIndividuals.TryRemove(id, out Individual guy);
                });
        }
    }
}
