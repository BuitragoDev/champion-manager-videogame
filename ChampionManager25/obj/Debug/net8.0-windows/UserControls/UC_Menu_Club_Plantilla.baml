   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      K*clr-namespace:ChampionManager25.MisMetodosChampionManager25.MisMetodos  5      ;    5ChampionManager25.UserControls.UC_Menu_Club_Plantilla   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      A
misMetodos*clr-namespace:ChampionManager25.MisMetodos     5      -   5          � 	Resources  5	      2   ,ChampionManager25.MisMetodos.LesionConverter   LesionConverter3   -ChampionManager25.MisMetodos.SancionConverter  SancionConverter��%:   &	        &	        5
   
   6%     5   
   6&   5      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $
��1845 �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   ���#1d6a7d$��1q�6D   $��10q�6X   ._�5      � 
������� 5      �� 5      $��* �6'   �� 5      $��90 �6'    5      �� 5      $��30 �6$   �� 5      $��* �6$   .+�5       �� ���#1d6a7d$��0��6.   $��0��6>   $��2��6K   ._�5!      �� $��JUGADORES EN PLANTILLA��6    $��Center=�6>   $��Center=�6[   $��Cascadia Code SemiBold)�5"       $��18(�6D   
������� �� TextAlignment$ Center=�6j   5#      �� 5&      ���	#FF23282D$��1��60   $��1��6@   $��2��6M   ._�5'      �  5(      �� 5)      $��* �6/    5+      �� 5,      $��30 �6,   �� 5-      $��70 �6,   �� 5.      $��30 �6,   �� 5/      $��70 �6,   �� 50      $��30 �6,   �� 51      $��70 �6,   �� 52      $��* �6,   .+�55      �� -   5         ��Name$ imgPlantillaPuntuacion��55       $��0��6@   $��1��6P   $��64��6]   $��64��6h   $��Center=�6t   $��Center=�56       $2��,/Recursos/img/icons/plantilla_puntuacion.png��6=   ${�False��6s    � Cursor$
 Hand��6�   6�   �� 58      -   5        $ imgPlantillaDatos��58       $��0��6;   $��3��6K   $��64��6X   $��64��6c   $��Center=�6o   $
 Hand��6�   $��Center=�59       $-��'/Recursos/img/icons/plantilla_datos.png��6=   6n   �� 5;      -   5        $ imgPlantillaEstadisticas��5;       $��0��6B   $��5��6R   $��64��6_   $��64��6j   $��Center=�6v   $
 Hand��6�   $��Center=�5<       $4��./Recursos/img/icons/plantilla_estadisticas.png��6=   6u   5=      5>      � 5A      $��0��6   $��1��6,   $��10q�69   .+�5B      &   System.Windows.Controls.DataGrid  -   5          Name$ dgPlantilla��5B      $
��1703��64      
IsReadOnly$
 True��6A      AutoGenerateColumns$ False��6S   	   GridLinesVisibility$
	 None=�5C      $��Cascadia Code Light)�6:   $��16(�6[   $	��Top=�6i   $	��863��6~   $
 Hand��6�   5D      5E      5F      5G      5H      