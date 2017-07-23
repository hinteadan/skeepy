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

        public ClashOutcome(Clash clash)
        {
            this.clash = clash ?? throw new InvalidOperationException("The Clash Outcome must have an underlying clash");
        }

        public object HasWinner { get { return true; } }
    }
}
