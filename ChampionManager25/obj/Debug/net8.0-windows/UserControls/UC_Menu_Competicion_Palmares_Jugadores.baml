   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      K    EChampionManager25.UserControls.UC_Menu_Competicion_Palmares_Jugadores   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      -   5          >�Name$  palmaresJugadores��5      .��5      � 
������� � ColumnDefinitions 5	   
   �� 5
      $
��1845 �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   ���#1d6a7d$��1q�6D   $��10q�6X   ._�5      � 
������� 5      �� 5      $��* �6'    5      �� 5      $��50 �6$   �� 5      $��* �6$   .+�5      �� ���#1d6a7d$��0��6.   $��0��6>   ._�5      �� $��PALMARÉS JUGADORES��6    $��Center=�6:   $��Center=�6W   $��Cascadia Code SemiBold)�5       $��22(�6D   
������� �� TextAlignment$ Center=�6j   5      �� 5      $��0��6   $��1��6)   ._�5      �  5       �� 5!      $	��700 �6/   �� 5"      $��* �6/    5$      �� 5%      $��* �6,   .+�5)      �� $��0��6!   $��0��61   $��10q�6>   ���#1d6a7d$��1q�6`   ._�5*      �  5+   "   �� 5,   &   $��* �67   �� 5-   &   $	��120 �67    5/   "   �� 50   &   $	��220 �64   �� 51   &   $��* �64   .+�54   "   �� $��0��6(   $��0��65   $��2��6E   $	��220��6Y   $	��700��6f   $��Uniform=�6r   $��Center=�55   (   $��Center=�6E    � Cursor$
 Hand��6`   $&�� /Recursos/img/bannerBalonOro.png��56   (   � 58   "   $��0��6,   $��1��6<   $��2��6I   $��10q�6]   .+�59   &   &   System.Windows.Controls.DataGrid  -   5          Name$  dgPalmaresJugadoresTotales��59   /      
IsReadOnly$
 True��6S      AutoGenerateColumns$ False��6e      GridLinesVisibility$
 None=�5:   /   $��Cascadia Code Light)�6J   $��16(�6k   $	��Top=�6y   $	��588��6�   $
 Hand��6�   $	��658��6�   5;   #   5<      5=      �� 5?      $��1��6!   $��0��61   $��10q�6>   ���#1d6a7d$��1q�6`   ._�5@      � $��10q�6(   .+�5A   "     -   5        $ dgPalmaresJugadores��5A   +   $
 True��6H   $ False��6Z   $
 None=�5B   +   $��Cascadia Code Light)�6F   $��16(�6g   $	��Top=�6u   $	��811��6�   $
 Hand��6�   $
��1071��6�   5C      5D      5E      5F      5G      5H      5I      5J      