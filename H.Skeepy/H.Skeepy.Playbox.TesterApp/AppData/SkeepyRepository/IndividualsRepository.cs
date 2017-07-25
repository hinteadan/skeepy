using H.Skeepy.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Playbox.TesterApp.AppData.SkeepyRepository
{
    public static class IndividualsRepository
    {
        private static ObservableCollection<Individual> individuals = new ObservableCollection<Individual>();

        public static ObservableCollection<Individual> All
        {
            get
            {
                return individuals;
            }
        }

        public static void Save(Individual guy)
        {
            individuals.Add(guy);
        }

        public static void Zap(Individual guy)
        {
            individuals.Remove(guy);
        }
    }
}
