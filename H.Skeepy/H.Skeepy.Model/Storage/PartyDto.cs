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
        public IndividualDto[] Members { get; set; }

        public void MorphFromSkeepy(Party model)
        {
            base.MorphFromSkeepy(model);
            Id = model.Id;
            Name = model.Name;
            Members = model.Members.Select(x => x.ToDto()).ToArray();
        }

        public Party ToSkeepy()
        {
            return ToSkeepy(Party.Existing(Id, Name, Members.Select(x => x.ToSkeepy()).ToArray()));
        }
    }
}
