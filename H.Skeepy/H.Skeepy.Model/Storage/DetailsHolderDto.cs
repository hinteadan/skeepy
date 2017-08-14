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
        public KeyValuePair<string, string>[] Details { get; set; }
    }
}
