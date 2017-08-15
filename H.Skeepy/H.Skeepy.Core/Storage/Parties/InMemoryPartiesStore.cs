using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Core.Storage.Parties
{
    public class InMemoryPartiesStore : ICanManageSkeepyStorageFor<Party>
    {
        public Task<bool> Any()
        {
            var taskSource = new TaskCompletionSource<bool>();
            taskSource.SetResult(false);
            return taskSource.Task;
        }

        public void Dispose()
        {

        }

        public Task<Party> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LazyEntity<Party>>> Get()
        {
            throw new NotImplementedException();
        }

        public Task Put(Party model)
        {
            throw new NotImplementedException();
        }
    }
}
