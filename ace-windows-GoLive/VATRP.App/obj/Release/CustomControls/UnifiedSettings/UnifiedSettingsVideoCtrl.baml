   M S B A M L   `   `   `  ���� ?  ;ACE, Version=0.0.62.0, Culture=neutral, PublicKeyToken=null�Fclr-namespace:com.vtcsecure.ace.windows.CustomControls.UnifiedSettings8com.vtcsecure.ace.windows.CustomControls.UnifiedSettings  W    Qcom.vtcsecure.ace.windows.CustomControls.UnifiedSettings.UnifiedSettingsVideoCtrl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     XlocalFclr-namespace:com.vtcsecure.ace.windows.CustomControls.UnifiedSettings     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .��� .+��    RecentsPageMainBg#��[    � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $	��240 ��� $
��120* ��� $��40 �.+��� $��0��$��0��$��3��$��General Video Settings��$��Center=�
��������� -   $��AutomaticallyStartLabel��$��0��$��1��$��2��$��Automatically Start��$��Center=�$
��Left=��� -   $ ��AutomaticallyStartCheckBox��$��2��$��1��$��Center=�$��Center=��� -   $��AutomaticallyAcceptLabel��$��0��$��2��$��2��$��Automatically Accept��$��Center=�$
��Left=��� -   $!��AutomaticallyAcceptCheckBox��$��2��$��2��$��Center=�$��Center=��� -   $��ShowSelfViewLabel��$��0��$��3��$��2��$��Show Self View��$��Center=�$
��Left=��� -   $��ShowSelfViewCheckBox��$��2��$��3��$��Center=�$��Center=��� -   $��PreferredFPSLabels��$��0��$��4��$��Preferred FPS��$��Center=�$
��Left=��� -   $��PreferredFPSTextBox��$��1��$��4��$��2��$��Stretch=�$��23��$��0,0,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�
����  ��� -	   $��VideoPresetLabel��$��0��$��4��$��Video Preset��$��Center=�$
��Left=�$y�	Collapsed=��� -
    ��Name$ VideoPresetComboBox��$��4��$��1��$��2��$��14(�$��0,3,0,3q�$	��120��$��24��$��Center=�$
��Left=�$y�	Collapsed=�.@��� 	default�� 
high-fps�� -   $��RtcpFeedbackLabel��$��0��$��5��$��RTCP Feedback��$��Center=�$
��Left=��� -   $ RtcpFeedbackComboBox��$��5��$��1��$��2��$��14(�$��0,3,0,3q�$	��120��$��24��$��Center=�$
��Left=�.@��� Off�� 
Explicit�� 
Implicit�� $��0��$��6��$��3��$��Video Codecs��$��Center=�
��������� -    ��Name$ VideoCodecsListView��#��[  $��0��$��7��$��3��$	��200��$	��280��$
��Left=� �� SelectionMode$ Single=� �� View �� .P��� $
��Name�� �� Width$ 95�� �� DisplayMemberBinding �� 	 �� Path$	 	CodecName��� $��	Rate (Hz)��$ 70�� �� $
	 Rate��� $��Channels��$ 70�� �� $	 Channels��� $��Status��$ 45��
 �� CellTemplate
 �� .R��� $��50��.+��� -   $��Center=�$��Center=� d� 	IsChecked �� $	 Status� �� Mode$ TwoWay=�