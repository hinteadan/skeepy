using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Model.Storage
{
    [Serializable]
    public abstract class WithDetailsHolderDto
    {
        public DetailsHolderDto DetailsHolder { get; set; } = new DetailsHolderDto();

        public virtual void MorphFromSkeepy(DetailsHolder model)
        {
            DetailsHolder.Details = model.Details.Select(x => new DetailsHolderDto.Entry { Key = x.Key, Value = x.Value }).ToArray();
        }

        protected T ToSkeepy<T>(T model) where T:DetailsHolder
        {
            model.SetDetails(DetailsHolder.Details.Select(x => (x.Key, x.Value)));
            return model;
        }
    }
}
