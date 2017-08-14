using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace H.Skeepy.Model.Storage
{
    [Serializable]
    [XmlRoot("DetailsHolder")]
    public class DetailsHolderDto
    {
        private class ConcreteDetailsHolder : DetailsHolder { }

        [Serializable]
        [XmlType(TypeName = "DetailEntry")]
        public class Entry
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public Entry[] Details { get; set; }

        public static DetailsHolderDto From(DetailsHolder model)
        {
            return new DetailsHolderDto
            {
                Details = model.Details.Select(x => new Entry { Key = x.Key, Value = x.Value }).ToArray()
            };
        }

        public DetailsHolder ToSkeepy()
        {
            return new ConcreteDetailsHolder().SetDetails(Details.Select(x => (x.Key, x.Value)));
        }
    }
}
