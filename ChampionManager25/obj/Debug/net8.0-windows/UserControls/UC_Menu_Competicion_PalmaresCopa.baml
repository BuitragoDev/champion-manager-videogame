   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      E    ?ChampionManager25.UserControls.UC_Menu_Competicion_PalmaresCopa   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      -   5          >�Name$  palmaresCopa��5      .��5      � 
������� � ColumnDefinitions 5	   
   �� 5
      $
��1845 �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   
���  ��$��1q�6D   $��10q�6X   ._�5      � 
������� 5      �� 5      $��* �6'    5      �� 5      $��50 �6$   �� 5      $��* �6$   .+�5      �� 
���  ��$��0��6.   $��0��6>   ._�5      �� -   5         ��Name$ 	lblTitulo��5       $��PALMARÉS EQUIPOS��63   $��Center=�6K   $��Center=�6h   $��Cascadia Code SemiBold)�5      $��22(�68   
������� �� TextAlignment$ Center=�6^   5      �� 5      $��0��6   $��1��6)   ._�5      �  5       �� 5!      $	��700 �6/   �� 5"      $��* �6/    5$      �� 5%      $��* �6,   .+�5)      �� $��0��6!   $��0��61   $��10q�6>   
���  ��$��1q�6`   ._�5*      �  5+   "   �� 5,   &   $��* �67   �� 5-   &   $	��120 �67    5/   "   �� 50   &   $��70 �64   �� 51   &   $	��220 �64   �� 52   &   $��* �64   .+�55   "   �� $��0��6)   $��0��69   $��2��6F   ._�56   &   �  57   *   �� 58   .   $��* �6?   �� 59   .   $��* �6?    5;   *   �� 5<   .   $��* �6<   .+�5?   *   �� -   5         ��Name$ imgLogoLiga��5?   0   $��0��6E   $��0��6R   $��50��6b   $��50��6n   $��UniformToFill=�6y   $��Center=�5@   0   $��Center=�6M    � Cursor$
 Hand��6h   6v   �� 5B   *   -   5        $ imgLogoCopa��5B   0   $��0��6E   $��1��6R   $��50��6b   $��50��6n   $��UniformToFill=�6y   ${�False��6�   $��Center=�5C   0   $��Center=�6M   $
 Hand��6h   6v   5D   '   5E   #   �� 5G   "   $��1��6(   $��0��65   $��2��6E   $	��220��6Y   $	��700��6f   $��Uniform=�6r   $��Center=�5H   $   $��Center=�6A   $
 Hand��6\   $&�� /Recursos/img/bannerPalmares.png��5I   $   � 5K   "   $��0��6,   $��2��6<   $��2��6I   $��10q�6]   .+�5L   &   &   System.Windows.Controls.DataGrid  -   5          Name$ 
dgPalmares��5L   /      
IsReadOnly$
 True��6C   	   AutoGenerateColumns$	 False��6U   
   GridLinesVisibility$

 None=�5M   +   $��Cascadia Code Light)�6F   $��16(�6g   $	��Top=�6u   $	��531��6�   $
 Hand��6�   $	��658��6�   5N   #   5O      5P      �� 5R      $��1��6!   $��0��61   $��10q�6>   
���  ��$��1q�6`   ._�5S      � $��10q�6(   .+�5T   "     -   5        $ dgHistorialFinales��5T   +   $
 True��6G   $	 False��6Y   $

 None=�5U   '   $��Cascadia Code Light)�6B   $��16(�6c   $	��Top=�6q   $	��811��6�   $
 Hand��6�   $
��1071��6�   5V      5W      5X      5Y      5Z      5[      5\      5]      