using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Model.Storage
{
    public interface IAmASkeepyDtoFor<T>
    {
        void MorphFromSkeepy(T model);
        T ToSkeepy();
    }
}
