using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace H.Skeepy.Model.Storage
{
    [Serializable]
    [XmlRoot("Individual")]
    public class IndividualDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [XmlElement(nameof(DetailsHolder), Type = typeof(DetailsHolderDto))]
        public DetailsHolderDto DetailsHolder { get; set; } = new DetailsHolderDto();
    }
}
