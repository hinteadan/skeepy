using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Core.Storage
{
    public interface ICanStoreSkeepy<TSkeepy> : IDisposable where TSkeepy : IHaveId
    {
        Task Put(TSkeepy model);
        Task Zap(string id);
    }
}
