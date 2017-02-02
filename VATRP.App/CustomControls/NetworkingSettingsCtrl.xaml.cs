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
using System.Windows.Navigation;
using System.Windows.Shapes;
using com.vtcsecure.ace.windows.Interfaces;
using com.vtcsecure.ace.windows.Services;
using VATRP.Core.Model;

namespace com.vtcsecure.ace.windows.CustomControls
{
    /// <summary>
    /// Interaction logic for NetworkSettingsCtrl.xaml
    /// </summary>
    public partial class NetworkSettingsCtrl : ISettings
    {
        public NetworkSettingsCtrl()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (App.CurrentAccount == null)
                return;
            EnableStunCheckBox.IsChecked = App.CurrentAccount.EnableSTUN;
            StunHostnameBox.Text = App.CurrentAccount.STUNAddress;
            StunHostPortBox.Text = App.CurrentAccount.STUNPort.ToString();
        }

        #region ISettings

        public bool IsChanged()
        {
            if (App.CurrentAccount == null)
                return false;

            var enabled = EnableStunCheckBox.IsChecked ?? false;
            if (enabled != App.CurrentAccount.EnableSTUN)
                return true;

            if (StunHostnameBox.Text != App.CurrentAccount.STUNAddress)
                return true;
            ushort port = 0;
            ushort.TryParse(StunHostPortBox.Text, out port);

            if (port != App.CurrentAccount.STUNPort && port != 0)
                return true;

            return false;
        }

        public bool Save()
        {
            if (App.CurrentAccount == null)
                return false;
            var stunEnabled = EnableStunCheckBox.IsChecked ?? false;

            if (!stunEnabled)
                return true;

            if (string.IsNullOrWhiteSpace(StunHostnameBox.Text))
            {
                MessageBox.Show("Incorrect STUN address", "ACE", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            
            ushort port = 0;

            ushort.TryParse(StunHostPortBox.Text, out port);
            if ( port < 1 || port > 65535)
            {
                MessageBox.Show("Incorrect STUN port", "ACE", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            App.CurrentAccount.STUNAddress = StunHostnameBox.Text;
            App.CurrentAccount.STUNPort = port;
            App.CurrentAccount.EnableSTUN = stunEnabled;
            return true;
        }

        #endregion
    }
}
