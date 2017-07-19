using H.Skeepy.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Core
{
    public class ClashUseCase
    {
        private readonly Clash clash;
        private readonly ConcurrentStack<Point> points = new ConcurrentStack<Point>();
        private Point[] cachedPoints = new Point[0];

        public ClashUseCase(Clash clash)
        {
            this.clash = clash ?? throw new InvalidOperationException("The Clash Use-Case must have an underlying Clash");
        }

        public Point[] Points
        {
            get
            {
                CheckPointsCache();
                return cachedPoints;
            }
        }

        public Point PointFor(Party party)
        {
            ValidateParty(party);
            var point = Point.NewFor(party);
            points.Push(point);
            return point;
        }

        private void ValidateParty(Party party)
        {
            if (!clash.Participants.Contains(party))
            {
                throw new InvalidOperationException("The given party is not part of this clash");
            }
        }

        private void CheckPointsCache()
        {
            if (cachedPoints.Length == points.Count)
            {
                return;
            }

            cachedPoints = points.ToArray();
        }
    }
}
