using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace H.Skeepy.Model.Storage
{
    [Serializable]
    public class DetailsHolderDto : IAmASkeepyDtoFor<DetailsHolder>
    {
        private class ConcreteDetailsHolder : DetailsHolder { }

        [Serializable]
        public class Entry
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public Entry[] Details { get; set; } = new Entry[0];

        public DetailsHolder ToSkeepy()
        {
            return new ConcreteDetailsHolder().SetDetails(Details.Select(x => (x.Key, x.Value)));
        }

        public void MorphFromSkeepy(DetailsHolder model)
        {
            this.Details = model.Details.Select(x => new Entry { Key = x.Key, Value = x.Value }).ToArray();
        }
    }
}
