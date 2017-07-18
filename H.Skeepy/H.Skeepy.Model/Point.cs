using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Model
{
    public class Point : DetailsHolder
    {
        private readonly string id;
        private readonly DateTime timestamp;
        private readonly Party forParty;

        public static Point NewFor(Party forParty)
        {
            return new Point(Guid.NewGuid().ToString(), DateTime.Now, forParty);
        }

        public static Point Existing(string id, DateTime at, Party forParty)
        {
            return new Point(id, at, forParty);
        }

        private Point(string id, DateTime at, Party forParty)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidOperationException("A Point must have an ID");
            }

            this.id = id;
            timestamp = at;
            this.forParty = forParty ?? throw new InvalidOperationException("A Point must count for a Party");
        }

        public string Id { get { return id; } }
        public DateTime At { get { return timestamp; } }

    }
}
