using H.Skeepy.Core;
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
                return string.Join(" vs. ", clash.Participants.Select(x => x.Name));
            }
        }
    }
}
