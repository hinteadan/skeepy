﻿using H.Skeepy.Core;
using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace H.Skeepy.Playbox.TesterApp.ViewModel
{
    public class ClashPointsViewModel : SkeepyTesterViewModel
    {
        private Clash clash;
        private PointTracker pointTracker;

        public ClashPointsViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                clash = Clash.New(Individual.New("Federer").ToParty(), Individual.New("Nadal").ToParty());
                Points = clash.Participants.OrderBy(x => x.Name).ToDictionary(x => x, x => 0);
                NotifyPropertyChanged(nameof(Points));
            }
        }

        public Clash Clash
        {
            get
            {
                return clash;
            }
            set
            {
                clash = value;
                pointTracker = new PointTracker(clash);
                NotifyPropertyChanged(nameof(Clash));
                NotifyPropertyChanged(nameof(ClashTitle));
            }
        }

        public string ClashTitle
        {
            get
            {
                return string.Join(" vs. ", clash.Participants.OrderBy(x => x.Name).Select(x => x.Name));
            }
        }

        public Dictionary<Party, int> Points { get; private set; }

        public void ScorePointFor(Party party)
        {
            pointTracker.PointFor(party);
            Points = pointTracker.Points
                .GroupBy(x => x.For)
                .OrderBy(x => x.Key.Name)
                .ToDictionary(x => x.Key, x => x.Count());
            NotifyPropertyChanged(nameof(Points));
        }
    }
}
