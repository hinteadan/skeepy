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

        public Party Party
        {
            get
            {
                return party;
            }
            set
            {
                party = value;
                NotifyPropertyChanged(nameof(Party));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
