using H.Skeepy.Core.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication.Storage
{
    public class InMemoryTokensStore : ICanGetSkeepyEntity<Token>
    {
        private readonly ReadOnlyDictionary<string, Token> tokens;

        public InMemoryTokensStore(params Token[] tokens)
        {
            this.tokens = new ReadOnlyDictionary<string, Token>(tokens.ToDictionary(x => x.Id));
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
    }
}
