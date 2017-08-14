using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Model.Storage
{
    [Serializable]
    public class DetailsHolderDto
    {
        private class ConcreteDetailsHolder : DetailsHolder { }


        public KeyValuePair<string, string>[] Details { get; set; }

        public static DetailsHolderDto From(DetailsHolder model)
        {
            return new DetailsHolderDto
            {
                Details = model.Details.ToArray()
            };
        }

        public DetailsHolder ToSkeepy()
        {
            return new ConcreteDetailsHolder().SetDetails(Details);
        }
    }
}
