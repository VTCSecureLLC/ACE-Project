﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using com.vtcsecure.ace.windows.ViewModel;

namespace com.vtcsecure.ace.windows.Views
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class AboutView : Window
    {
        public AboutView()
        {
            //***********************************************************************************************************************
            // Initilize of about view. Called when about view is displaying.
            //***********************************************************************************************************************
            InitializeComponent();
            DataContext = new AboutViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //***********************************************************************************************************************
            // This method is called when "Ok" button is clicked in About view.
            //***********************************************************************************************************************
            Close();
        }
    }
}
