﻿using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Playbox.TesterApp.AppData
{
    public static class AppState
    {
        public static Clash Clash { get; set; }

        public static ClashOutcome Outcome { get; set; }

        public static ScoreBoard<int> Score { get; set; }
    }
}
