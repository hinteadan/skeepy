using H.Skeepy.Model;
using H.Skeepy.Playbox.TesterApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace H.Skeepy.Playbox.TesterApp
{
    /// <summary>
    /// Interaction logic for ClashPointsPage.xaml
    /// </summary>
    public partial class ClashPointsPage : Page
    {
        private readonly ClashPointsViewModel viewModel;

        public ClashPointsPage()
        {
            InitializeComponent();
            viewModel = (ClashPointsViewModel)DataContext;
        }

        private void NewPoint_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ScorePointFor(PartyFor(sender));
        }

        private Party PartyFor(object senderButton)
        {
            return ((KeyValuePair<Party, int>)((FrameworkElement)senderButton).DataContext).Key;
        }

        private void ProcessOutcome_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ProcessCurrentOutcome();
        }
    }
}
