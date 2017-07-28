using H.Skeepy.Model;
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
    public class ClashViewModel
    {
        private ObservableCollection<PartyViewModel> members = new ObservableCollection<PartyViewModel>();

        public ClashViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                members.Add(new PartyViewModel());
                members.Add(new PartyViewModel());
                members.Add(new PartyViewModel());
                members.Add(new PartyViewModel());
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
    }
}
