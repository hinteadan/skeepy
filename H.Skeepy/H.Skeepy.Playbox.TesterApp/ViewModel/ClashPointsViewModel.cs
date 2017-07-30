using H.Skeepy.Core;
using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Playbox.TesterApp.ViewModel
{
    public class ClashPointsViewModel : SkeepyTesterViewModel
    {
        private Clash clash;
        private PointTracker pointTracker;

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
            }
        }
    }
}
