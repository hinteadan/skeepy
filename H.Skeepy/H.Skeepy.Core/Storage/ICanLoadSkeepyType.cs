using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Core.Storage
{
    public interface ICanLoadSkeepy<TSkeepy> : ICanGetSkeepyEntity<TSkeepy>, IDisposable where TSkeepy : IHaveId
    {
        Task<bool> Any();
        Task<IEnumerable<LazyEntity<TSkeepy>>> Get();
    }
}
