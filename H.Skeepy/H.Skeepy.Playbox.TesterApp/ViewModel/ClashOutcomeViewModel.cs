using H.Skeepy.Model;
using H.Skeepy.Playbox.TesterApp.AppData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace H.Skeepy.Playbox.TesterApp.ViewModel
{
    public class ClashOutcomeViewModel : SkeepyTesterViewModel
    {
        private ClashOutcome outcome;

        public ClashOutcomeViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                Outcome = new ClashOutcome(Clash.New(Individual.New("Federer").ToParty(), Individual.New("Rafa").ToParty()));
                return;
            }

            Outcome = AppState.Outcome;
        }

        public ClashOutcome Outcome
        {
            get
            {
                return outcome;
            }
            set
            {
                outcome = value;
                NotifyPropertyChanged(nameof(Outcome));
                NotifyPropertyChanged(nameof(Winner));
            }
        }

        public string Winner
        {
            get
            {
                if (!outcome.HasWinner)
                {
                    return "Clash is not won by anyone";
                }

                return $"Clash is {(outcome.IsTie ? "tied between" : "won by")} {string.Join(" & ", outcome.Winners.Select(x => x.Name))}";
            }
        }
    }
}
