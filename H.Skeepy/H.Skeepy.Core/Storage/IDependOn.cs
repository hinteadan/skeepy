using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Core.Storage
{
    public interface IDependOn<TDependency> where TDependency : IHaveId
    {
        IDependOn<TDependency> WithDependency(Func<string, TDependency> dependencyProvider);
    }
}
