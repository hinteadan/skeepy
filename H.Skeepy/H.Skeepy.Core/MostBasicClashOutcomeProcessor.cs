﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H.Skeepy.Model;

namespace H.Skeepy.Core
{
    public class MostBasicClashOutcomeProcessor : ICanProcessClashOutcome
    {
        private readonly ICanPlaybackPoints pointsPool;
        private readonly Clash clash;

        public MostBasicClashOutcomeProcessor(Clash clash, ICanPlaybackPoints pointsPool)
        {
            this.clash = clash ?? throw new InvalidOperationException("Outcome Processor must have an underlying clash");
            this.pointsPool = pointsPool ?? throw new InvalidOperationException("Points pool must be provided");
        }

        public ClashOutcome ProcessOutcome()
        {
            if (!pointsPool.Points.Any())
            {
                return new ClashOutcome(clash).WonBy(clash.Participants);
            }

            var score = pointsPool
                .Points
                .GroupBy(x => x.For)
                .ToDictionary(x => x.Key, x => x.Count());
            var maxScore = score.Values.Max();

            return new ClashOutcome(clash).WonBy(score.Where(x => x.Value == maxScore).Select(x => x.Key));
        }
    }
}
