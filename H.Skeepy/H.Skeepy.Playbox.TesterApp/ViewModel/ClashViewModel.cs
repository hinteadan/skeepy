using H.Skeepy.Model;
using H.Skeepy.Playbox.TesterApp.AppData.SkeepyRepository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace H.Skeepy.Playbox.TesterApp.ViewModel
{
    public class ClashViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PartyViewModel> members = new ObservableCollection<PartyViewModel>();
        private ObservableCollection<Party> availableParties = new ObservableCollection<Party>(TeamsRepository.All);
        private Party selectedPartyToAdd = null;

        public ClashViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                members.Add(new PartyViewModel());
                members.Add(new PartyViewModel());
            }
        }

        public ObservableCollection<PartyViewModel> Members
        {
            get
            {
                return members;
            }
        }

        public ObservableCollection<Party> AvailableTeams
        {
            get
            {
                return availableParties;
            }
        }

        public Party SelectedPartyToAdd {
            get
            {
                return selectedPartyToAdd;
            }
            set
            {
                selectedPartyToAdd = value;
                NotifyPropertyChanged(nameof(SelectedPartyToAdd));
                NotifyPropertyChanged(nameof(CanAddParty));
            }
        }

        public bool CanAddParty { get { return SelectedPartyToAdd != null; } }

        public void AddMember()
        {
            if(SelectedPartyToAdd == null)
            {
                return;
            }

            members.Add(new PartyViewModel(SelectedPartyToAdd));
            availableParties.Remove(SelectedPartyToAdd);
            SelectedPartyToAdd = availableParties.Any() ? availableParties[0] : null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
