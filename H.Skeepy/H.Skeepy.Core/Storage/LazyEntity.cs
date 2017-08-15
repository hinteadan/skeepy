using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Core.Storage
{
    public class LazyEntity<T>
    {
        private readonly Func<T, T> entityLoader;
        private readonly T summary;

        public LazyEntity(T summary, Func<T, T> entityLoader)
        {
            this.summary = summary;
            this.entityLoader = entityLoader ?? throw new InvalidOperationException($"{nameof(entityLoader)} must be provided");
        }

        public T Summary
        {
            get { return summary; }
        }

        public T Full
        {
            get { return entityLoader(Summary); }
        }
    }
}
