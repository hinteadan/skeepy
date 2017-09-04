using H.Skeepy.Core.Storage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Registration.Storage
{
    public class InMemoryRegistrationStore : ICanManageSkeepyStorageFor<RegisteredUser>
    {
        private readonly ConcurrentDictionary<string, RegisteredUser> repo = new ConcurrentDictionary<string, RegisteredUser>();

        public Task<bool> Any()
        {
            var taskSource = new TaskCompletionSource<bool>();
            taskSource.SetResult(!repo.IsEmpty);
            return taskSource.Task;
        }

        public void Dispose()
        {
        }

        public Task<IEnumerable<LazyEntity<RegisteredUser>>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<RegisteredUser> Get(string id)
        {
            var taskSource = new TaskCompletionSource<RegisteredUser>();
            taskSource.SetResult(repo.ContainsKey(id) ? repo[id] : null);
            return taskSource.Task;
        }

        public Task Put(RegisteredUser model)
        {
            repo.AddOrUpdate(model.Id, model, (x, y) => model);
            return Task.CompletedTask;
        }

        public Task Zap(string id)
        {
            repo.TryRemove(id, out var user);
            return Task.CompletedTask;
        }
    }
}
