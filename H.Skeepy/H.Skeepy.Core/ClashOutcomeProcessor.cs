using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Core
{
    public abstract class ClashOutcomeProcessor
    {
        protected readonly ICanPlaybackPoints pointsPool;
        protected readonly Clash clash;

        public ClashOutcomeProcessor(Clash clash, ICanPlaybackPoints pointsPool)
        {
            this.clash = clash ?? throw new InvalidOperationException("Outcome Processor must have an underlying clash");
            this.pointsPool = pointsPool ?? throw new InvalidOperationException("Points pool must be provided");
        }

        public abstract ClashOutcome ProcessOutcome();
    }
}
