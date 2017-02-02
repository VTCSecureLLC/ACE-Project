﻿using com.vtcsecure.ace.windows.Services;
using System;
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
using VATRP.Core.Model;

namespace com.vtcsecure.ace.windows.CustomControls.UnifiedSettings
{
    /// <summary>
    /// Interaction logic for UnifiedSettingsTextCtrl.xaml
    /// </summary>
    public partial class UnifiedSettingsTextCtrl : BaseUnifiedSettingsPanel
    {
        public CallViewCtrl CallControl;

        public UnifiedSettingsTextCtrl()
        {
            InitializeComponent();
            Title = "Text";
            this.Loaded += UnifiedSettingsTextCtrl_Loaded;
        }

        void UnifiedSettingsTextCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
            this.EnableRealTimeTextCheckbox.IsChecked = ServiceManager.Instance.ConfigurationService.Get(Configuration.ConfSection.GENERAL,
                Configuration.ConfEntry.USE_RTT, true);

            var textSendMode = ServiceManager.Instance.ConfigurationService.Get(Configuration.ConfSection.GENERAL,
                Configuration.ConfEntry.TEXT_SEND_MODE, "Real Time Text");

            foreach (var item in TextSendModeComboBox.Items)
            {
                var tb = item as TextBlock;
                if (tb != null)
                {
                    string itemString = tb.Text;
                    if (itemString.Equals(textSendMode))
                    {
                        TextSendModeComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
            var rttFontName = string.Empty;

            int rttFontSize = 18;

            var defaultFont = Fonts.SystemFontFamilies.FirstOrDefault();
            if (defaultFont != null)
                rttFontName = defaultFont.Source;
            
            if (App.CurrentAccount != null)
            {
                rttFontName = App.CurrentAccount.RTTFontFamily;
                rttFontSize = App.CurrentAccount.RTTFontSize;
            }

            foreach (var fontItem in TextFontFamilyComboBox.Items)
            {
                var ff = fontItem as FontFamily;
                if (ff != null)
                {
                    if (ff.Source.Equals(rttFontName))
                    {
                        TextFontFamilyComboBox.SelectedItem = ff;
                        break;
                    }
                }
            }

            //********************************************************
            //Font Size

            foreach (var fontItem in TextFontSizeComboBox.Items)
            {

                int ff = int.Parse(((ComboBoxItem)fontItem).Content.ToString());
                //ComboBoxItem ci = (ComboBoxItem)fontItem))ff.ToString();

                
                if (ff != null)
                {
                    if (ff == rttFontSize)
                    {
                        TextFontSizeComboBox.SelectedItem = fontItem;
                        break;
                    }
                }
            }
            //*********************************************************
        }

        private void OnEnableRealTimeText(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Enable Real Time Text Call Clicked");
            bool enabled = EnableRealTimeTextCheckbox.IsChecked ?? false;
            ServiceManager.Instance.ConfigurationService.Set(Configuration.ConfSection.GENERAL,
                Configuration.ConfEntry.USE_RTT, enabled);
            ServiceManager.Instance.ConfigurationService.SaveConfig();

            if (CallControl != null && CallControl.IsLoaded)
                CallControl.EnableRTTButton(enabled);
        }

        private void OnTextsendMode(object sender, SelectionChangedEventArgs e)
        {
            var textSendModeLabel = TextSendModeComboBox.SelectedItem as TextBlock;
            if (textSendModeLabel != null)
            {
                ServiceManager.Instance.ConfigurationService.Set(Configuration.ConfSection.GENERAL,
                    Configuration.ConfEntry.TEXT_SEND_MODE, textSendModeLabel.Text);

                ServiceManager.Instance.ConfigurationService.SaveConfig();
            }
        }

        private void OnTextFontChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_initialized || App.CurrentAccount == null)
                return;


            var ff = new FontFamily(TextFontFamilyComboBox.Text);

           // var ff = TextFontFamilyComboBox.SelectedItem as FontFamily;
            if (ff != null)
            {
                if (App.CurrentAccount.RTTFontFamily == ff.Source)
                    return;
                App.CurrentAccount.RTTFontFamily = ff.Source;
                ServiceManager.Instance.AccountService.Save();
                ServiceManager.Instance.ChatService.UpdateRTTFontFamily(ff.Source);
            }
        }

        private void OnTextFontSizeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_initialized || App.CurrentAccount == null)
                return;
            //var ff = TextFontSizeComboBox.SelectedItem  ;
            int ff = int.Parse(((ComboBoxItem)TextFontSizeComboBox.SelectedItem).Content.ToString());

            if (ff != null)
            {
                if (App.CurrentAccount.RTTFontSize == ff)
                    return;
                App.CurrentAccount.RTTFontSize = ff;
               ServiceManager.Instance.AccountService.Save();
               ServiceManager.Instance.ChatService.UpdateRTTFontSize(ff);

               // ServiceManager.Instance.ChatService.UpdateRTTFontSize(ff);
            }
        }
        


    }
}
