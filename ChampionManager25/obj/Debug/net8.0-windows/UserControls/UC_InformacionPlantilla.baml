   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      <    6ChampionManager25.UserControls.UC_InformacionPlantilla   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      -   5          >�Name$  informacionPlantilla��5      .��5      � 
������� � ColumnDefinitions 5	   
   �� 5
      $	��100 �6   �� 5      $��* �6   �� 5      $	��100 �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��* �6   �� 5      $��20 �6   .+�5   
   �� $��0��6   $��0��6!   $��3��6.   $��50��6B   $��Center=�6N   ���#1d6a7d._�5      �� $��DETALLES DE LA PLANTILLA��6   $��Cascadia Code SemiBold)�68   $��20(�6\   $��Center=�6j   
�������$��Center=�5   %   5      �� 5   
   $��1��6   $��1��6!   $	��690��6.   $��Center=�6;   ���#23282d$��1q�6l   ._�5      ]� 
�������$	��690��60   .+�5      �  5      �� 5      $��* �6+    5!      �� 5"      $��* �6(   .+�5$      &   System.Windows.Controls.DataGrid  -   5          Name$ dgPlantilla��5$      $��0��64   $��0��6D      
IsReadOnly$
 True��6Q      AutoGenerateColumns$
 True��6c      GridLinesVisibility$
 None=�5%      $��Cascadia Code SemiBold)�6:   $��16(�6^   $	��Top=�6l    � Cursor$
 Hand��6�   ��5'      �� 5(       �� 
TargetType"  6$   .�5)   "   �� 	 �� Property	 �@ RowBackground
 �� Value

 �����6B   �� 5+   "    	 �@ AlternatingRowBackground

 �����6M   �� 5-   "   	 � ��

 ���� 6@   �� 5/   "   	 � ��$
 0q�6D   �� 51   "   	 �@ 	CellStyle
 52   &   �� 53   *   *  $System.Windows.Controls.DataGridCell"  60   .�54   .   �� 	 � ��

 ���� 6L   �� 56   .   	 � ��$
 0q�6P   58   +   5:   #   5;      5=      5>      5?      5@      5A      5B      