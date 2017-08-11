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
        private ScoreBoard<int> score;

        public ClashOutcomeViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                var clash = Clash.New(Individual.New("Federer").ToParty(), Individual.New("Rafa").ToParty());
                Outcome = new ClashOutcome(clash);
                Score = new ScoreBoard<int>(clash).SetScore((clash.Participants[0], 6), (clash.Participants[1], 4));
                return;
            }

            Outcome = AppState.Outcome;
            Score = AppState.Score;
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

        public ScoreBoard<int> Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
                NotifyPropertyChanged(nameof(Score));
                NotifyPropertyChanged(nameof(ScoreLabel));
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

        public string ScoreLabel
        {
            get
            {
                return string.Join(Environment.NewLine, score.Scores.OrderByDescending(x => x.Item2).Select(x => $"{x.Item1.Name}: {x.Item2}"));
            }
        }
    }
}
