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

        public ClashOutcomeProcessor(ICanPlaybackPoints pointsPool)
        {
            this.pointsPool = pointsPool ?? throw new InvalidOperationException("Points pool must be provided");
        }

        public abstract ClashOutcome ProcessOutcome();
    }
}
