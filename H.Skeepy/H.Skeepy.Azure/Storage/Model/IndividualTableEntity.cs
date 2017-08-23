using H.Skeepy.Model;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Azure.Storage.Model
{
    internal class IndividualTableEntity : TableEntity
    {
        public IndividualTableEntity(Individual individual)
        {
            RowKey = individual.Id;
            PartitionKey = individual.Id;
            Id = individual.Id;
            FirstName = individual.FirstName;
            LastName = individual.LastName;
            Details = JsonConvert.SerializeObject(individual.Details);
        }

        public IndividualTableEntity() { }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Details { get; set; }

        public Individual ToSkeepy()
        {
            var individual = Individual.Existing(Id, FirstName, LastName);
            individual.SetDetails(JsonConvert.DeserializeObject<Dictionary<string, string>>(Details).Select(x => (x.Key, x.Value)));
            return individual;
        }
    }
}
