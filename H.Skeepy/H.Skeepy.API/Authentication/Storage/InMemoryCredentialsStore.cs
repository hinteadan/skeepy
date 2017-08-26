using H.Skeepy.Core.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication.Storage
{
    public class InMemoryCredentialsStore : ICanGetSkeepyEntity<Credentials>
    {
        private readonly ReadOnlyDictionary<string, Credentials> users;

        public InMemoryCredentialsStore(params Credentials[] users)
        {
            this.users = new ReadOnlyDictionary<string, Credentials>(users.ToDictionary(x => x.Username));
        }

        public void Dispose()
        {

        }

        public Task<Credentials> Get(string id)
        {
            var taskSource = new TaskCompletionSource<Credentials>();
            taskSource.SetResult(users.ContainsKey(id) ? users[id] : null);
            return taskSource.Task;
        }
    }
}
