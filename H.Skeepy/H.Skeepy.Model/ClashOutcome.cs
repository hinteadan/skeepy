using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Model
{
    public class ClashOutcome
    {
        private readonly Clash clash;

        private Party winningParty = null;

        public ClashOutcome(Clash clash)
        {
            this.clash = clash ?? throw new InvalidOperationException("The Clash Outcome must have an underlying clash");
        }

        public bool HasWinner { get { return winningParty != null; } }

        public Party Winner { get { return winningParty; } }

        public ClashOutcome WonBy(Party winningParty)
        {
            if(winningParty == null || !clash.Participants.Contains(winningParty))
            {
                throw new InvalidOperationException("The winning party must be part of the clash");
            }
            this.winningParty = winningParty;
            return this;
        }
    }
}
