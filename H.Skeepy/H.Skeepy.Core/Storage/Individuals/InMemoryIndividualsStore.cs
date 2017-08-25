using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H.Skeepy.Model;
using System.IO;
using System.Collections.Concurrent;
using System.Runtime.Serialization.Formatters.Binary;
using H.Skeepy.Model.Storage;
using System.Collections;

namespace H.Skeepy.Core.Storage.Individuals
{
    public class InMemoryIndividualsStore : ICanManageSkeepyStorageFor<Individual>
    {
        private static readonly BinaryFormatter serializer = new BinaryFormatter();
        private readonly ConcurrentDictionary<string, MemoryStream> storageSpace = new ConcurrentDictionary<string, MemoryStream>();

        public Task<bool> Any()
        {
            var taskSource = new TaskCompletionSource<bool>();
            taskSource.SetResult(!storageSpace.IsEmpty);
            return taskSource.Task;
        }

        public Task Put(Individual fed)
        {
            return Task.Run(() =>
            {
                var stream = new MemoryStream();
                serializer.Serialize(stream, fed.ToDto());
                storageSpace.AddOrUpdate(fed.Id, stream, (x, y) => stream);
            });
        }

        public Task<Individual> Get(string id)
        {
            return Task.Run(() => storageSpace.ContainsKey(id) ? LoadModel(storageSpace[id]) : null);
        }

        public Task<IEnumerable<LazyEntity<Individual>>> Get()
        {
            return Task.Run(() => storageSpace.Select(x => new LazyEntity<Individual>(Individual.Existing(x.Key, x.Key), y => LoadModel(storageSpace[y.Id]))));
        }

        public void Dispose()
        {
            foreach (var memory in storageSpace)
            {
                memory.Value.Dispose();
            }
        }

        private static Individual LoadModel(MemoryStream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            return ((IndividualDto)serializer.Deserialize(stream)).ToSkeepy();
        }

        public Task Zap(string id)
        {
            return Task.Run(() => { storageSpace.TryRemove(id, out MemoryStream old); });
        }
    }
}
