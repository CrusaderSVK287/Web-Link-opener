   M S B A M L   `   `   `  ����J  FWeb Link opener, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Web_Link_openerWeb_Link_opener  5   	        Web_Link_opener.MainWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Web_Link_opener     5   	   -   5          � Title$  Web URL manager��5   	   $	��550��6!   $	��400��6.   .��5	      �  � RowDefinitions 5
      �� 5      $��25 �6   �� 5      �� 5      $��70 �6   .+�5      o� $��0��6	   .@�5      m� $	��New��6   $��1q�6   
�������$��25��6F   $��37��6R   .@�5      m� -   5        $��CreateFolder��5      $��Folder��6   63   m� 5      -   5        $��
CreateLink��5   /   $
��Link��6   ${�False��6   6A   5      m� 5      -   5        $��RefreshWindow��5   a   $��Refresh��6   $��1q�6   
�������$��25��6J   $��55��6V   6v   m� 5      -   5        $��Remove��5   `   $��Remove��6   $��1q�6   
�������$��25��6I   $��55��6U   6n   m� 5      -   5        $
��Exit��5   r   $
��Exit��6   $��1q�6   
�������$��25��6G   $��35��6S   6r   5      � 5      $��1��6	    � ColumnDefinitions 5      �� 5      $	��100��6   �� 5      $��3* �6   .+�5      �� $��0��6   .��5      �� -   5        $��	SPFolders��5   8   $��0��6   
�������$
��True��6I   $��Visible=�6n   5      �� 5       $��1��6   .��5!      �� -   5        $��SPLinks��5!   6   $��1��6   ���#EEEEEE$
��True��6E   $��Visible=�6j   5"      5#      � 5$      $��2��6	   ���#EEEEEE.+�5%      �� -	   5        $��Description��5%   -   $��5q�6   $��Description��6   
�������$
��Wrap=�6S   5&      � 5'      -
   5         �Name$ utilityGrid��5'   	   
������� J� Opacity$	 0.5Q�64   $y�	Collapsed=�6B   5(      5)      