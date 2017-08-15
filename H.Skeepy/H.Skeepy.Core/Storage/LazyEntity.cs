using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Core.Storage
{
    public class LazyEntity<T>
    {
        private readonly Lazy<T> fullEntity;
        private readonly T summary;

        public LazyEntity(T summary, Func<T, T> entityLoader)
        {
            if (entityLoader == null)
            {
                throw new InvalidOperationException($"{nameof(entityLoader)} must be provided");
            }

            this.summary = summary;
            fullEntity = new Lazy<T>(() => entityLoader(this.summary));
        }

        public T Summary
        {
            get { return summary; }
        }

        public T Full
        {
            get { return fullEntity.Value; }
        }
    }
}
