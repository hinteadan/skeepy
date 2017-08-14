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

namespace H.Skeepy.Core.Storage
{
    public class InMemoryIndividualsStore : IDisposable
    {
        private readonly ConcurrentDictionary<string, MemoryStream> storageSpace = new ConcurrentDictionary<string, MemoryStream>();

        public bool Any()
        {
            return false;
        }

        public async Task Put(Individual fed)
        {
            await Task.Run(() =>
            {
                var stream = new MemoryStream();
                new BinaryFormatter().Serialize(stream, fed.ToDto());
                storageSpace.AddOrUpdate(fed.Id, stream, (x, y) => stream);
            });
        }

        public async Task<Individual> Get(string id)
        {
            return await Task.Run(() => {
                storageSpace[id].Seek(0, SeekOrigin.Begin);
                return ((IndividualDto)new BinaryFormatter().Deserialize(storageSpace[id])).ToSkeepy();
            });
        }

        public void Dispose()
        {
            foreach (var memory in storageSpace)
            {
                memory.Value.Dispose();
            }
        }
    }
}
