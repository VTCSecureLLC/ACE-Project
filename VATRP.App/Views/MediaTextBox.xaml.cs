﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using com.vtcsecure.ace.windows.Model;
using com.vtcsecure.ace.windows.Services;
using com.vtcsecure.ace.windows.ViewModel;
using VATRP.Core.Extensions;
using VATRP.Core.Model;

namespace com.vtcsecure.ace.windows.Views
{
    /// <summary>
    /// Interaction logic for MediaTextWindow.xaml
    /// </summary>
    public partial class MediaTextWindow
    {
        #region Members

        private SimpleMessagingViewModel _viewModel;

        public delegate void MakeCallRequestedDelegate(string called_address);
        public event MakeCallRequestedDelegate MakeCallRequested;
        #endregion

        public MediaTextWindow(SimpleMessagingViewModel vm)
            : base(VATRPWindowType.MESSAGE_VIEW)
        {
            _viewModel = vm;
            DataContext = vm;
            InitializeComponent();
            ServiceManager.Instance.ChatService.ConversationUpdated += ChatManagerOnConversationUpdated;
        }

        private void ChatManagerOnConversationUpdated(object sender, VATRP.Core.Events.ConversationUpdatedEventArgs e)
        {
            ScrollToEnd();
        }

        private void ScrollToEnd()
        {
            if (ServiceManager.Instance.Dispatcher.Thread != Thread.CurrentThread)
            {
                ServiceManager.Instance.Dispatcher.BeginInvoke((Action)(ScrollToEnd));
                return;
            }

            MessageListView.SelectedIndex = MessageListView.Items.Count - 1;
            MessageListView.ScrollIntoView(MessageListView.SelectedItem);
        }

        private void OnChatSelected(object sender, SelectionChangedEventArgs e)
        {
            var contactModel = ContactsList.SelectedItem as ContactViewModel;

            if (contactModel != null)
            {
                _viewModel.SetActiveChatContact(contactModel.Contact, IntPtr.Zero);
                if (contactModel.Contact != null)
                    _viewModel.ReceiverAddress = contactModel.Contact.RegistrationName;
                ScrollToEnd();
            }
        }

        private void OnSendButtonClicked(object sender, RoutedEventArgs e)
        {
            //****************************************************************************************
            // Send button pressed from chat window.
            //*****************************************************************************************
            SendSimpleMessage();
        }

        private void SendSimpleMessage()
        {
            //****************************************************************************************
            // Sending a message by Chat window.
            //*****************************************************************************************
            if (_viewModel.CheckReceiverContact())
            {
                var contactModel = ContactsList.SelectedItem as ContactViewModel;
                if (contactModel != null && contactModel.Contact != _viewModel.ChatViewContact.Contact)
                    ContactsList.SelectedItem = _viewModel.ChatViewContact;

                _viewModel.SendSimpleMessage(_viewModel.MessageText);
            }
            else
            {
                MessageBox.Show("Can't send message to " + _viewModel.ReceiverAddress, "ACE", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Return:
                    SendSimpleMessage();
                    break;
            }
        }

        private void OnCallClick(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null && _viewModel.Chat != null && _viewModel.Chat.Contact != null)
                if (MakeCallRequested != null)
                    MakeCallRequested(_viewModel.Chat.Contact.RegistrationName);
        }
    }
}
