﻿using H.Skeepy.Model;
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
    /// Interaction logic for ClashEditor.xaml
    /// </summary>
    public partial class ClashEditor : UserControl
    {
        public ClashEditor()
        {
            InitializeComponent();
        }

        private void AddPartyClick(object sender, RoutedEventArgs e)
        {
            (DataContext as ClashViewModel).AddMember();
        }
    }
}
