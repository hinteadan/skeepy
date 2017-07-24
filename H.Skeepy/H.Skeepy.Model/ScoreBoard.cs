using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Model
{
    public class ScoreBoard<T> : DetailsHolder
    {
        private readonly Clash clash;
        private readonly ConcurrentDictionary<Party, T> scorePerParty;
        private (Party, T)[] cachedScores = new(Party, T)[0];

        public ScoreBoard(Clash clash)
        {
            this.clash = clash ?? throw new InvalidOperationException("The ScoreBoard must have an underlying clash");
            scorePerParty = new ConcurrentDictionary<Party, T>(this.clash.Participants.ToDictionary(x => x, x => default(T)));
        }

        public T this[Party p]
        {
            get
            {
                ValidateParty(p);
                return scorePerParty[p];
            }
        }

        public (Party, T)[] Scores
        {
            get
            {
                CheckScoresCache();
                return cachedScores;
            }
        }

        public ScoreBoard<T> SetScore(Party forParty, T score)
        {
            ValidateParty(forParty);
            scorePerParty.AddOrUpdate(forParty, score, (x, y) => score);
            return this;
        }

        public ScoreBoard<T> SetScore(IEnumerable<(Party, T)> scores)
        {
            foreach (var x in scores)
            {
                SetScore(x.Item1, x.Item2);
            }
            return this;
        }

        public ScoreBoard<T> SetScore(params (Party, T)[] scores)
        {
            return SetScore(scores.AsEnumerable());
        }

        private void ValidateParty(Party party)
        {
            if (!clash.Participants.Contains(party))
            {
                throw new InvalidOperationException("The party must be part of the clash");
            }
        }
        private void CheckScoresCache()
        {
            if (cachedScores.Length == scorePerParty.Count)
            {
                return;
            }

            cachedScores = scorePerParty.Select(x => (x.Key, x.Value)).ToArray();
        }

    }
}
