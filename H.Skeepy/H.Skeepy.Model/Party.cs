using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Model
{
    public class Party : DetailsHolder, IHaveId
    {
        private readonly string id;
        private readonly string name;
        private readonly Individual[] individuals;
        private readonly ReadOnlyDictionary<string, Individual> individualsDictionary;

        public static Party New(string name, params Individual[] individuals)
        {
            return new Party(Guid.NewGuid().ToString(), name, individuals);
        }

        public static Party Existing(string id, string name, params Individual[] individuals)
        {
            return new Party(id, name, individuals);
        }

        private Party(string id, string name, params Individual[] individuals)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidOperationException("Existing party must have an ID");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException("Party must have a name");
            }

            if (!individuals.Any())
            {
                throw new InvalidOperationException("A party must have at least one individual");
            }

            this.id = id;
            this.name = name;

            this.individuals = individuals;
            this.individualsDictionary = new ReadOnlyDictionary<string, Individual>(individuals.ToDictionary(x => x.Id));
        }

        public string Id { get { return id; } }
        public string Name { get { return name; } }

        public Individual this[string id]
        {
            get
            {
                return individualsDictionary.ContainsKey(id) ? individualsDictionary[id] : null;
            }
        }

        public Individual[] Members
        {
            get
            {
                return individuals;
            }
        }
    }
}
