using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Model
{
    public sealed class Party : DetailsHolder
    {
        private readonly Individual[] individuals;
        private readonly ReadOnlyDictionary<string, Individual> individualsDictionary;

        public Individual this[string id]
        {
            get
            {
                return individualsDictionary.ContainsKey(id) ? individualsDictionary[id] : null;
            }
        }

        public Party(params Individual[] individuals)
        {
            if (!individuals.Any())
            {
                throw new InvalidOperationException("A party must have at least one individual");
            }

            this.individuals = individuals;
            this.individualsDictionary = new ReadOnlyDictionary<string, Individual>(individuals.ToDictionary(x => x.Id));
        }
    }
}
