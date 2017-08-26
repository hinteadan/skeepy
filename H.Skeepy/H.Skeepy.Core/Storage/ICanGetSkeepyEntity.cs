using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Core.Storage
{
    public interface ICanGetSkeepyEntity<TSkeepy> : IDisposable where TSkeepy : IHaveId
    {
        Task<TSkeepy> Get(string id);
    }
}
