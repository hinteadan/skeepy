using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Model
{
    public class ClashOutcome
    {
        private readonly Clash clash;

        private readonly ConcurrentDictionary<string, Party> winningParties = new ConcurrentDictionary<string, Party>();
        private Party[] winningPartiesCache = new Party[0];

        public ClashOutcome(Clash clash)
        {
            this.clash = clash ?? throw new InvalidOperationException("The Clash Outcome must have an underlying clash");
        }

        public bool HasWinner { get { return winningParties.Any(); } }

        public Party Winner
        {
            get
            {
                return winningParties.Count == 1 ? winningParties.Single().Value : throw new InvalidOperationException("This Clash has multiple winners");
            }
        }

        public Party[] Winners { get { EnsureWinningPartiesCache(); return winningPartiesCache; } }

        public bool IsTie { get { return clash.Participants.Length == winningParties.Count; } }

        public ClashOutcome WonBy(Party winningParty)
        {
            if (winningParty == null || !clash.Participants.Contains(winningParty))
            {
                throw new InvalidOperationException("The winning party must be part of the clash");
            }
            winningParties.AddOrUpdate(winningParty.Id, winningParty, (x, y) => winningParty);
            return this;
        }

        public ClashOutcome WonBy(IEnumerable<Party> winningParties)
        {
            foreach (var p in winningParties)
            {
                WonBy(p);
            }
            return this;
        }

        public ClashOutcome WonBy(params Party[] winningParties)
        {
            return WonBy(winningParties.AsEnumerable());
        }

        private void EnsureWinningPartiesCache()
        {
            if (winningPartiesCache.Length == winningParties.Count)
            {
                return;
            }
            winningPartiesCache = winningParties.Values.ToArray();
        }
    }
}
