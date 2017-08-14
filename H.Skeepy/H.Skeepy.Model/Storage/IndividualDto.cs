using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace H.Skeepy.Model.Storage
{
    [Serializable]
    public class IndividualDto : WithDetailsHolderDto, IAmASkeepyDtoFor<Individual>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void MorphFromSkeepy(Individual model)
        {
            base.MorphFromSkeepy(model);

            Id = model.Id;
            FirstName = model.FirstName;
            LastName = model.LastName;
        }

        public Individual ToSkeepy()
        {
            return ToSkeepy(Individual.Existing(Id, FirstName, LastName));
        }
    }
}
