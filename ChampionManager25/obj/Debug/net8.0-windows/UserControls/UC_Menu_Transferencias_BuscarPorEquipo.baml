   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      K    EChampionManager25.UserControls.UC_Menu_Transferencias_BuscarPorEquipo   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      -   5        
�������.��5      � 
�������  � ColumnDefinitions  5	   
   �� 5
      $
��1845 �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   $��20q�6.   ._�5      � 
�������  5      �� 5      $��* �6'   �� 5      $��* �6'    5      �� 5      $��* �6$   .+�5      �� $��0��6   $��0��6)   $��5q�66   
���   �$��1q�6U   ._�5      �   5      �� 5      $��* �6/    5       �� 5!      $��40 �6,   �� 5"      $	��150 �6,   �� 5#      $��40 �6,   �� 5$      $��* �6,   .+�5'      �� $��0��6!   $��0��61   ���#1d6a7d._�5(      �� $��0��6(   $��0��68   $��SELECCIONA UNA LIGA��6E   $��Center=�6`   $��Center=�6}   $��Cascadia Code SemiBold)�5)   (   $��18(�6L   
������� �� TextAlignment$ Center=�6r   5*      �� 5-      $��0��6!   $��1��61   ._�5.      �   5/   "   �� 50   &   $��* �67   �� 51   &   $��* �67   �� 52   &   $��* �67   �� 53   &   $��* �67   �� 54   &   $��* �67    56   "   �� 57   &   $��* �64   .+�5:   "   �� -   5         ��Name$ imgLiga1��5:   (   $��0��6:   $��0��6J   $��80��6W   $��80��6b   $��Uniform=�6n   $��Center=�6�   $��Center=�5;   (   $��10q�6E    � Cursor$
 Hand��6Q   6_   �� 5=   "   -   5        $ imgLiga2��5=   (   $��1��6:   $��0��6J   $��80��6W   $��80��6b   $��Uniform=�6n   $��Center=�6�   $��Center=�5>   (   $��10q�6E   $
 Hand��6Q   6_   �� 5@   "   -   5        $ imgLiga3��5@   (   $��2��6:   $��0��6J   $��80��6W   $��80��6b   $��Uniform=�6n   $��Center=�6�   $��Center=�5A   (   $��10q�6E   $
 Hand��6Q   6_   �� 5C   "   -   5        $ imgLiga4��5C   (   $��3��6:   $��0��6J   $��80��6W   $��80��6b   $��Uniform=�6n   $��Center=�6�   $��Center=�5D   (   $��10q�6E   $
 Hand��6Q   $8��2/Recursos/img/logos_competiciones/80x80/euLogo.png��6_   6�   �� 5F   "   -   5        $ imgLiga5��5F   (   $��4��6:   $��0��6J   $��64��6W   $��64��6b   $��Uniform=�6n   $��Center=�6�   $��Center=�5G   (   $��10q�6E   $,��&/Recursos/img/icons/freeAgent_icon.png��6Q   $
 Hand��6�   6�   5H      5I      �� 5K      $��0��6!   $��2��61   ���#1d6a7d._�5L      �� $��0��6(   $��0��68   $��SELECCIONA UN EQUIPO��6E   $��Center=�6a   $��Center=�6~   $��Cascadia Code SemiBold)�5M   (   $��18(�6L   
�������$ Center=�6r   5N      � 5Q      -   5        $��wrapPanelEquipos��5Q   $   $��0��6<   $��3��6L   $
��Left=�6Y   $
��auto��6t    � 	ItemWidth$ 70��5R   $    � 
ItemHeight$ 70��63   $	��870��6C   $��20,5,0,0q�6O   5S      5T      �� 5W      $��1��6   $��0��6)   $��5q�66   
���   �$��1q�6U   ._�5X      �   5Y      �� 5Z      $��* �6/    5\      �� 5]      $��40 �6,   �� 5^      $��* �6,   .+�5a      �� $��0��6!   $��0��61   ���#1d6a7d._�5b      �� -   5         ��Name$ txtListadoJugadores��5b   (   $��0��6E   $��0��6U   $��LISTADO DE JUGADORES��6b   $��Center=�6~   $��Center=�6�   $��Cascadia Code SemiBold)�5c   (   $��18(�6L   
�������$ Center=�6r   5d      � 5f      $��1��6$   $��Center=�61   $	��870��6N   .+�5g      &   System.Windows.Controls.DataGrid  -	   5          Name$ dgJugadores��5g   '   $	��870��6<   	   
IsReadOnly$
	 True��6H   
   AutoGenerateColumns$
 False��6Z      GridLinesVisibility$
 None=�5h   '   $��Cascadia Code Light)�6B   $��16(�6c   $	��Top=�6q   $	��813��5i   '   $
 Hand��64   $��0,20,0,0q�6B   $y�Hidden=�6T   5j      �� 5k      -
   5        $ dgJugadoresTexto��5k   $   $��1��6>   $.��(Aún no hay ningún equipo seleccionado.��6K   $��Center=�6y   $��Center=�6�   $��Cascadia Code Light)�5l   $   $��16(�6E   
���   �$ Center=�6f   5m      5n      5o      5p      5q      5r      