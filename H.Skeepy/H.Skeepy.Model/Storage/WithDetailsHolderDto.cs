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
    }
}
