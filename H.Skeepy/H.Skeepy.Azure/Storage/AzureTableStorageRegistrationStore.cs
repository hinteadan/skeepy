using H.Skeepy.API.Registration.Storage;
using H.Skeepy.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Azure.Storage
{
    public class AzureTableStorageRegistrationStore : ICanManageSkeepyStorageFor<RegisteredUser>
    {
        public Task<bool> Any()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LazyEntity<RegisteredUser>>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<RegisteredUser> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task Put(RegisteredUser model)
        {
            throw new NotImplementedException();
        }

        public Task Zap(string id)
        {
            throw new NotImplementedException();
        }
    }
}
