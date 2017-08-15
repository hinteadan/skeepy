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

namespace H.Skeepy.Core.Storage
{
    public class InMemoryIndividualsStore : IDisposable
    {
        private readonly ConcurrentDictionary<string, MemoryStream> storageSpace = new ConcurrentDictionary<string, MemoryStream>();

        public bool Any()
        {
            return !storageSpace.IsEmpty;
        }

        public Task Put(Individual fed)
        {
            return Task.Run(() =>
            {
                var stream = new MemoryStream();
                new BinaryFormatter().Serialize(stream, fed.ToDto());
                storageSpace.AddOrUpdate(fed.Id, stream, (x, y) => stream);
            });
        }

        public Task<Individual> Get(string id)
        {
            return Task.Run(() => LoadIndividual(storageSpace[id]));
        }

        public Task<IEnumerable<LazyEntity<Individual>>> Get()
        {
            return Task.Run(() => storageSpace.Select(x => new LazyEntity<Individual>(Individual.Existing(x.Key, x.Key), y => LoadIndividual(storageSpace[y.Id]))));
        }

        public void Dispose()
        {
            foreach (var memory in storageSpace)
            {
                memory.Value.Dispose();
            }
        }

        private static Individual LoadIndividual(MemoryStream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            return ((IndividualDto)new BinaryFormatter().Deserialize(stream)).ToSkeepy();
        }
    }
}
