   M S B A M L   `   `   `  ����?  ;ACE, Version=0.0.62.0, Culture=neutral, PublicKeyToken=null�Fclr-namespace:com.vtcsecure.ace.windows.CustomControls.UnifiedSettings8com.vtcsecure.ace.windows.CustomControls.UnifiedSettings  5      W    Qcom.vtcsecure.ace.windows.CustomControls.UnifiedSettings.UnifiedSettingsAudioCtrl   5   !   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      XlocalFclr-namespace:com.vtcsecure.ace.windows.CustomControls.UnifiedSettings     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5	      � .+�5
   
   �    RecentsPageMainBg#��[  6     � RowDefinitions  5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $
��360* �6#   �� 5      $	��40* �6#   .+�5      �� $��0��6   $��0��6$   $��2��61   $��In Call Settings��6E   $��Center=�6`   
�������$y�	Collapsed=�6�   5      �� 5      -   5        $��MuteMicrophoneLabel��5   /   $��0��5      $��1��6$   $��Mute Microphone��61   $��Center=�6K   $
��Left=�5      5      �� 5      -   5        $��MuteMicrophoneCheckBox��5   �   $��2��6   $��1��6'   $��Center=�6l   $��Center=�6�   6�   �� 5       -   5        $��MuteSpeakerLabel��5!   /   $��0��5       $��2��6$   $��Mute Speaker��61   $��Center=�6H   $
��Left=�5!      $y�	Collapsed=�6G   5"      �� 5#      -   5        $��MuteSpeakerCheckBox��5#   �   $��2��6   $��2��6'   $��Center=�6f   $��Center=�6�   $y�	Collapsed=�6�   �� 5&      $��0��6   $��3��6$   $��3��61   $��Audio Codecs��6F   $��Center=�6]   
�������5'      �� 5)      -   5         ��Name$ AudioCodecsListView��5)   }   $	��300��6   $	��280��6$   $��0��60   $��4��6A   $��2��6N   $
��Left=�6b    �� SelectionMode$ Single=�6�    �� View 5*      �� 5+      .P�5,      �� $
��Name��6)    �� Width$ 95��67    �� DisplayMemberBinding 6B   ��  �� Path$ 	CodecName��� 5-      $��	Rate (Hz)��6)   $ 70��6<    6G   �� $
 Rate��� 5.      $��Channels��6)   $ 70��6;    6F   �� $ Channels��� 5/      $��Status��6)   $ 45��69    �� CellTemplate 50      �� 51   "   .R�52   &   �� $��50��61   .+�53   *   �� -   5        $��Center=�54   /   $��Center=�6J   	 d� 	IsChecked	 53   3   �� $ Status�
 �� Mode$
 TwoWay=�54   g   55   '   56   #   58      59      5<      5=      5>      5?      