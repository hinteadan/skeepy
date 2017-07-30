using H.Skeepy.Playbox.TesterApp.AppData;
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
using H.Skeepy.Model;
using System.ComponentModel;

namespace H.Skeepy.Playbox.TesterApp
{
    /// <summary>
    /// Interaction logic for DefineClashPage.xaml
    /// </summary>
    public partial class DefineClashPage : Page, INotifyPropertyChanged
    {
        public DefineClashPage()
        {
            InitializeComponent();
            (clashEditor.DataContext as ClashViewModel).Members.CollectionChanged += (sender, e) =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsClashValid"));
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsClashValid { get { return (clashEditor.DataContext as ClashViewModel).Members.Count > 0; } }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            AppState.Clash = Clash.New((clashEditor.DataContext as ClashViewModel).Members.Select(x => x.Party).ToArray());
            (Application.Current.MainWindow as NavigationWindow).NavigationService.Navigate(new Uri("ClashPointsPage.xaml", UriKind.Relative));
        }
    }
}
