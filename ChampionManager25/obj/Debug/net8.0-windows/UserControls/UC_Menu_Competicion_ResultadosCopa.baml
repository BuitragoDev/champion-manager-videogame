   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      G    AChampionManager25.UserControls.UC_Menu_Competicion_ResultadosCopa   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      -   5          >�Name$  resultadosCopa��5      .��5      � 
������� � ColumnDefinitions 5	   
   �� 5
      $
��1845 �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   
���  ��$��1q�6D   $��10q�6X   ._�5      � 
������� 5      �� 5      $��* �6'    5      �� 5      $��50 �6$   �� 5      $	��100 �6$   �� 5      $��* �6$   .+�5      �� 
���  ��$��0��6.   $��0��6>   ._�5      �� -   5         ��Name$ 	lblTitulo��5       $��
RESULTADOS��63   $��Center=�6E   $��Center=�6b   $��Cascadia Code SemiBold)�5      $��22(�6@   
������� �� TextAlignment$ Center=�6f   5      �� 5      $��0��6   $��1��6)   ._�5       � 
������� 5!      �� 5"      $��* �6/   �� 5#      $��* �6/   �� 5$      $��* �6/   �� 5%      $��* �6/   �� 5&      $��* �6/   �� 5'      $��* �6/    5)      �� 5*      $��* �6,   .+�5-      �� -   5         ��Name$ imgLogoLiga��5-       $��0��65   $��1��6B   $��80��6R   $��80��6^   $��UniformToFill=�6i   $��Center=�5.       $��Center=�6=    � Cursor$
 Hand��6X   6f   �� 50      $��0��6    $��2��6-   $��80��6=   $��80��6I   $��UniformToFill=�6T   $��Center=�51       $��Center=�6=   $;��5/Recursos/img/logos_competiciones/80x80/copa80off.png��6X   �� 53      -   5        $ imgLogoCopaEuropa1��53       $��0��6<   $��3��6I   $��80��6Y   $��80��6e   $��UniformToFill=�6p   $��Center=�54       $��Center=�6=   $
 Hand��6X   6f   �� 56      -   5        $ imgLogoCopaEuropa2��56       $��0��6<   $��4��6I   $��80��6Y   $��80��6e   $��UniformToFill=�6p   $��Center=�57       $��Center=�6=   $
 Hand��6X   6f   58      59      �� 5;      $��0��6   $��2��6)   ._�5<      � ��5=      �� 5>       �� ImageSource$' !/Recursos/img/fondoResultados.png��6)    j� Stretch$ UniformToFill=�6Y   	 j� TileMode$
	 None=�6q    5@      �� 5A      $��* �6/    5C      �� 5D      $��60 �6,   �� 5E      $��* �6,   .+�5I      �� $��0��6!   $��0��61   
���  ��._�5J      ]� -   5        
 ]�Name$
 panelNavegadorCalendario��5J   (   $��Center=�6J   $��Center=�6e   .+�5K   "   �  5L   &   �� 5M   *   $��* �6;   �� 5N   *   $	��550 �6;   �� 5O   *   $��* �6;    5Q   &   �� 5R   *   $��* �68   .+�5U   &   �� -   5         ��Name$ btnJornadaAnterior��5U   -   $��0��6I   $��0��6Y   $	��◀��6f   $��50��6r   $��30��5V   -   $��5q�69   $
 Hand��6D   $��Center=�6R   $��Right=�6m   6�   �� 5W   &   -   5        $ txtJornadaActual��5W   0   $��1��6J   $��22(�6Z   $��	Jornada 1��6h   $��Center=�5X   0   $��Center=�6K   $ Center=�6h   $��Cascadia Code SemiBold)�5Y   0   
�������$	��530��6l   �� 5Z   &   -	   5        $ btnJornadaSiguiente��5Z   -   $��2��6J   $��0��6Z   $	��▶��6g   $��50��5[   -   $��30��68   $��5q�6D   $
 Hand��6O   $��Center=�6]   $
��Left=�6x   6�   5\   #   5]      5^      �� 5a      $��1��6!   $
��1400��6.   $
��auto��6;   $	��Top=�6I   $��Center=�6a   $��	0,10,0,10q�6~   ._�5b      � -
   5         �Name$ gridPartidos��5b   #   $��0,0,0,0q�69    5d   "   �� 5e   &   $��* �67   �� 5f   &   $��* �67    5i   "   �� 5j   &   $��* �64   �� 5k   &   $��* �64   �� 5l   &   $��* �64   �� 5m   &   $��* �64   �� 5n   &   $��* �64   �� 5o   &   $��* �64   �� 5p   &   $��* �64   �� 5q   &   $��* �64   �� 5r   &   $��* �64   �� 5s   &   $��* �64   �� 5t   &   $��* �64   �� 5u   &   $��* �64   �� 5v   &   $��* �64   �� 5w   &   $��* �64   �� 5x   &   $��* �64   �� 5y   &   $��* �64   5{      5|      5}      5~      5      5�      5�      5�      