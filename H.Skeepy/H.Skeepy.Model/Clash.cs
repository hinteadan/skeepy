using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Model
{
    public class Clash<PointType> where PointType : Point
    {
        private readonly string id;

        private readonly ReadOnlyDictionary<string, Party> partiesDictionary;
        private readonly Party[] parties;

        public static Clash<PointType> New(params Party[] parties)
        {
            return new Clash<PointType>(Guid.NewGuid().ToString(), parties);
        }

        public static Clash<PointType> Existing(string id, params Party[] parties)
        {
            return new Clash<PointType>(id, parties);
        }

        private Clash(string id, params Party[] parties)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidOperationException("A Clash must have an ID");
            }

            if (!parties.Any())
            {
                throw new InvalidOperationException("A Clash must have at least one party");
            }

            this.id = id;
            this.parties = parties;
            partiesDictionary = new ReadOnlyDictionary<string, Party>(parties.ToDictionary(x => x.Id));
        }

        public string Id { get { return id; } }

        public Party[] Participants { get { return parties; } }

        public Party Participant(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            return partiesDictionary.ContainsKey(id) ? partiesDictionary[id] : null;
        }
    }
}
