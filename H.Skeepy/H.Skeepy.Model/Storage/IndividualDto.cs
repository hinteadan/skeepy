using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace H.Skeepy.Model.Storage
{
    [Serializable]
    public class IndividualDto : IAmASkeepyDtoFor<Individual>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DetailsHolderDto DetailsHolder { get; set; } = new DetailsHolderDto();

        public void MorphFromSkeepy(Individual model)
        {
            throw new NotImplementedException();
        }

        public Individual ToSkeepy()
        {
            throw new NotImplementedException();
        }
    }
}
