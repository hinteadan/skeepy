using H.Skeepy.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Core
{
    public sealed class PointTracker
    {
        private readonly Clash clash;
        private readonly ConcurrentStack<Point> points = new ConcurrentStack<Point>();
        private readonly ReadOnlyDictionary<Party, ConcurrentStack<Point>> pointsPerParty;
        private Point[] cachedPoints = new Point[0];

        public PointTracker(Clash clash)
        {
            this.clash = clash ?? throw new InvalidOperationException("The Clash Use-Case must have an underlying Clash");
            pointsPerParty = new ReadOnlyDictionary<Party, ConcurrentStack<Point>>(clash.Participants.ToDictionary(x => x, x => new ConcurrentStack<Point>()));
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
            pointsPerParty[party].Push(point);
            return point;
        }

        public Point[] PointsOf(Party party)
        {
            ValidateParty(party);
            return pointsPerParty[party].ToArray();
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

        public PointTracker Undo()
        {
            if (points.IsEmpty)
            {
                return this;
            }
            if (points.TryPop(out var point))
            {
                pointsPerParty[point.For].TryPop(out point);
            }
            return this;
        }
    }
}
