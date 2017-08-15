using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Core.Storage
{
    public interface ICanLoadSkeepy<TSkeepy> : IDisposable where TSkeepy : IHaveId
    {
        Task<bool> Any();
        Task<TSkeepy> Get(string id);
        Task<IEnumerable<LazyEntity<TSkeepy>>> Get();
    }
}
