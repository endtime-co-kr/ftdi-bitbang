   M S B A M L   `   `   `  ����I  EFTDIControlGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:FTDIControlGUIFTDIControlGUI  5   	       FTDIControlGUI.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:FTDIControlGUI     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��554��6)    � DataContext 5	      "   FTDIControlGUI.MainViewModel  5
   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��560* �6   �� 5      $
��241* �6   .+�5   
   &   System.Windows.Controls.DataGrid     AutoGenerateColumns$ False��6:   $y�	Collapsed=�6V   $��2��6m   ��6   �� *FtdiCollection+   Columns 5      4  .System.Windows.Controls.DataGridCheckBoxColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Num.��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *
IsOutput+ �� Mode$ TwoWay=�  5      $
 Name��6)    67   �� *IsHigh+$ TwoWay=�5      �� 5   
   $��	0,0,151,0q�6>   $��2��6Q   ��6   �� *FtdiCollection+��5      �� 5      .R�5      ��  �� Orientation$ Vertical=�6!   5      ��5      �� 5      .R�5       �� $ 
Horizontal=�6!   .+�5!      �� $��40��6$   	 �� TextAlignment$	 Center=�6I   ��6/   �� *
DeviceId+6I   �� 5"      $��30��6$   $	 Center=�6K   ��6/   �� *
PortNumber+6K   �� 5#      $��80��6#   $��Output��6.   $��Center=�6?   
 d� 	IsChecked
 6a   �� *
IsOutput+�� 5$      $��80��6#   $
��High��6.   $��Center=�6=   
 6_   �� *IsHigh+{�6|   �� *
IsOutput+�� 5%      $��30��6$   $	 Center=�6H   ��6/   �� *	ReadVal+6H   5&      5'      5)      �� 5*   
   $��0,188,0,341q�60   $��1��6E   $��Stretch=�6U   $	��NaN��6s   ��6   �� *
RunMessage+6s   �� 5+   
   $
��Open��61   $��0,262,0,272q�6@   $��1��6U   $��Stretch=�6e   ��6   �� *CommandOpen+6e   5,      5-      