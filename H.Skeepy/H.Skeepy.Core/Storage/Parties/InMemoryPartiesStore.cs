using H.Skeepy.Model;
using H.Skeepy.Model.Storage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Core.Storage.Parties
{
    public class InMemoryPartiesStore : ICanManageSkeepyStorageFor<Party>
    {
        private static readonly BinaryFormatter serializer = new BinaryFormatter();
        private readonly ConcurrentDictionary<string, MemoryStream> storageSpace = new ConcurrentDictionary<string, MemoryStream>();

        public Task<bool> Any()
        {
            var taskSource = new TaskCompletionSource<bool>();
            taskSource.SetResult(!storageSpace.IsEmpty);
            return taskSource.Task;
        }

        public void Dispose()
        {
            foreach (var memory in storageSpace)
            {
                memory.Value.Dispose();
            }
        }

        public Task<Party> Get(string id)
        {
            return Task.Run(() => LoadModel(storageSpace[id]));
        }

        public Task<IEnumerable<LazyEntity<Party>>> Get()
        {
            return Task.Run(() => storageSpace.Select(x => new LazyEntity<Party>(Party.Existing(x.Key, x.Key, Individual.New("_dummy")), y => LoadModel(storageSpace[y.Id]))));
        }

        public Task Put(Party model)
        {
            return Task.Run(() =>
            {
                var stream = new MemoryStream();
                serializer.Serialize(stream, model.ToDto());
                storageSpace.AddOrUpdate(model.Id, stream, (x, y) => stream);
            });
        }

        private static Party LoadModel(MemoryStream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            return ((PartyDto)serializer.Deserialize(stream)).ToSkeepy();
        }
    }
}
