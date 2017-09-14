using H.Skeepy.API.Authentication;
using H.Skeepy.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Azure.Storage
{
    public class AzureTableStorageCredentialStore : ICanManageSkeepyStorageFor<Credentials>
    {
        public Task<bool> Any()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LazyEntity<Credentials>>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Credentials> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task Put(Credentials model)
        {
            throw new NotImplementedException();
        }

        public Task Zap(string id)
        {
            throw new NotImplementedException();
        }
    }
}
