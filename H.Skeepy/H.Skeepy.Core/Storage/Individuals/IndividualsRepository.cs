using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Core.Storage.Individuals
{
    public class IndividualsRepository : IDisposable
    {
        private readonly ICanManageSkeepyStorageFor<Individual> storage;

        public IndividualsRepository(ICanManageSkeepyStorageFor<Individual> storage)
        {
            this.storage = storage ?? throw new InvalidOperationException("The repository must have an underlying storage");
        }

        public void Dispose()
        {
            storage.Dispose();
        }

        public Task<IEnumerable<Individual>> All()
        {
            return storage.Get().ContinueWith(t => t.Result.Select(x => x.Full));
        }
    }
}
