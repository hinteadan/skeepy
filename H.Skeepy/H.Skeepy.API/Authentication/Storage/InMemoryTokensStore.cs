using H.Skeepy.Core.Storage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication.Storage
{
    public class InMemoryTokensStore : ICanManageSkeepyStorageFor<Token>
    {
        private readonly ConcurrentDictionary<string, Token> tokens;

        public InMemoryTokensStore(params Token[] tokens)
        {
            this.tokens = new ConcurrentDictionary<string, Token>(tokens.ToDictionary(x => x.Public));
        }

        public Task<bool> Any()
        {
            return Task.Run(() => tokens.Any());
        }

        public void Dispose()
        {

        }

        public Task<Token> Get(string id)
        {
            var taskSource = new TaskCompletionSource<Token>();
            taskSource.SetResult(tokens.ContainsKey(id) ? tokens[id] : null);
            return taskSource.Task;
        }

        public Task<IEnumerable<LazyEntity<Token>>> Get()
        {
            return Task.Run(() => tokens.Values.Select(t => new LazyEntity<Token>(t, s => t)));
        }

        public Task Put(Token model)
        {
            return Task.Run(() => tokens.AddOrUpdate(model.Id, model, (x, y) => model));
        }

        public Task Zap(string id)
        {
            return Task.Run(() => tokens.TryRemove(id, out var token));
        }
    }
}
