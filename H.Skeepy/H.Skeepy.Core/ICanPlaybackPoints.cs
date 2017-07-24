﻿using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Core
{
    public interface ICanPlaybackPoints
    {
        Point[] PointsOf(Party party);
        Point[] Points { get; }
    }
}
