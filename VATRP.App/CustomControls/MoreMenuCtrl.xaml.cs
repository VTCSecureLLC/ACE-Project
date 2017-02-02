﻿using System;
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
using com.vtcsecure.ace.windows.ViewModel;

namespace com.vtcsecure.ace.windows.CustomControls
{
    /// <summary>
    /// Interaction logic for MoreMenuCtrl.xaml
    /// </summary>
    public partial class MoreMenuCtrl : UserControl
    {
        private MenuViewModel _viewModel;

        public event EventHandler ResourceClicked;
        public event EventHandler SettingsClicked;
        public event EventHandler SelfViewClicked;
        public event EventHandler VideoMailClicked;

        public MoreMenuCtrl()
        {
            InitializeComponent();
        }
        public void SetDataContext(MenuViewModel viewModel)
        {

            //****************************************************************************************
            // Setting the More Menu view.
            //*****************************************************************************************
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void OnResourcesClicked(object sender, RoutedEventArgs e)
        {

            //************************************************************************************************************************************
            // On Resources item clicked from More==>Settings==>Resources
            //************************************************************************************************************************************
            if (ResourceClicked != null) 
                ResourceClicked(this, EventArgs.Empty);
        }

        private void OnSettingsClicked(object sender, RoutedEventArgs e)
        {
            //*************************************************************************************************************************************
            // Settings menu item clicked from "More" menu item.
            //*************************************************************************************************************************************
            if (SettingsClicked != null)
                SettingsClicked(this, EventArgs.Empty);
        }

        private void OnVmClicked(object sender, RoutedEventArgs e)
        {
            //************************************************************************************************************************************
            // On VideoMail clicked from  More==>Settings==>VideoMail
            //************************************************************************************************************************************
            if (VideoMailClicked != null)
                VideoMailClicked(this, EventArgs.Empty);
        }

        private void OnSelfViewClicked(object sender, RoutedEventArgs e)
        {

            //************************************************************************************************************************************
            // On Self-Preview clicked from  More==>Settings==>Self-Preview
            //************************************************************************************************************************************
            if (SelfViewClicked != null)
                SelfViewClicked(this, EventArgs.Empty);
        }
    }
}
