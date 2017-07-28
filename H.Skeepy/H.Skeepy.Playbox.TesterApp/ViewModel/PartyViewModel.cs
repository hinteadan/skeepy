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
    public class PartyViewModel : INotifyPropertyChanged
    {
        private Party party;
        private string members = string.Empty;

        public PartyViewModel()
        {
            if(DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                Party = Party.New("Awesome Party", Individual.New("Roger", "Federer"), Individual.New("Rafael", "Nadal"));
            }
        }

        public PartyViewModel(Party party)
        {
            Party = party;
        }

        public Party Party
        {
            get
            {
                return party;
            }
            set
            {
                party = value;
                members = string.Join(", ", party?.Members.OrderBy(x => x.FullName).Select(x => x.FullName) ?? Enumerable.Empty<string>());
                NotifyPropertyChanged(nameof(Party));
                NotifyPropertyChanged(nameof(Members));
            }
        }

        public string Members
        {
            get
            {
                return members;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
