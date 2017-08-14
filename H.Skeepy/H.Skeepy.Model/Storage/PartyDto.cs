using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Model.Storage
{
    [Serializable]
    public class PartyDto : WithDetailsHolderDto, IAmASkeepyDtoFor<Party>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public void MorphFromSkeepy(Party model)
        {
            throw new NotImplementedException();
        }

        public Party ToSkeepy()
        {
            throw new NotImplementedException();
        }
    }
}
