   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      :    4ChampionManager25.UserControls.UC_Menu_Manager_Ficha   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      -   5        .��5      � 
�������  � ColumnDefinitions  5	   
   �� 5
      $	��700 �6   �� 5      $
��1135 �6    � RowDefinitions 5   
   �� 5      $	��465 �6   �� 5      $	��465 �6   .+�5   
   �� 
�������$��2q�5      ���#1d6a7d$��5,5,5,5q�5      $��0��5      $��0��6   $	��Top=�6*   $	��455��6B   ._�5      �   5      �� 5      $��* �6'    5      �� 5      $��42 �6$   �� 5      $��* �6$   �� 5      $	��100 �6$   .+�5"      �� $��0��6   $��0��6)   ���#1d6a7d$��0q�6K   ���#1d6a7d._�5#      �� -   5         ��Name$ txtInfo��5#       $��16(�61   $
��INFO��6?   $��Center=�6K   $��Center=�5$       $��Cascadia Code SemiBold)�6=   
�������6a   5%      ]� 5(      $��0��6   $��1��6,   .+�5)      �   5*      �� 5+      $	��140 �6/   �� 5,      $��* �6/    5.      �� 5/      $��* �6,   �� 50      $��40 �6,   �� 51      $��40 �6,   �� 52      $��40 �6,   �� 53      $��40 �6,   �� 54      $��40 �6,   �� 55      $��* �6,   .+�58      �� -   5         ��Name$ 	imgAvatar��58       $��0��63   $��0��6@   $��7��6P   $	��128��6a   $	��128��6n   $��Right=�59       $��Center=�6<    � Cursor$
 Hand��6W   $$��/Recursos/img/icons/avatar.png��5:       �� 5;      -   5        $ txtNombreManager��5;   $   $��1��6>   $��1��6N   $��24(�6[   $��Nombre Manager��6i   $	��Top=�6   $
��Left=�5<   $   $��Cascadia Code SemiBold)�6?   
���   �$��50,0,0,0q�6v   �� 5=      -   5        $ 
imgBandera��5=       $
 Hand��64   $��1��6B   $��2��6R   $��Center=�6_   $
��Left=�5>   $   $
��Left=�6?   $��35��6U   $��35��6`   $��50,0,0,0q�6l   �� 5?      -   5        $ txtNacionalidad��5?   $   $��1��6=   $��2��6M   $��18(�6Z   $��Nacionalidad��6h   $��Center=�6|   $
��Left=�5@   &   $��Cascadia Code SemiBold)�6A   
���   �$��	100,0,0,0q�6x   �� 5A      -   5        $ txtFechaNacimiento��5A   $   $��1��6@   $��3��6P   $��18(�6]   $��Fecha Nacimiento: ��6k   $��Center=�6�   $
��Left=�5B   $   $��Cascadia Code SemiBold)�6?   
���   �$��50,0,0,0q�6v   �� 5C      -   5        $ 
txtRanking��5C   $   $��1��68   $��4��6H   $��18(�6U   $��	Ranking: ��6c   $��Center=�6t   $
��Left=�5D   $   $��Cascadia Code SemiBold)�6?   
���   �$��50,0,0,0q�6v   �� 5E      -	   5         ��Name$ canvasEstrellas��5E   !   $��1��6:   $��5��6J   $��Center=�6W   $
��Left=�5F   !   $��50,0,0,0q�6<   6P   5H      5I      ]� 5L      $��0��6   $��2��6,   $	��100��69   .+�5M      �   5N      �� 5O      $��* �6/    5Q      �� 5R      $��32 �6,   �� 5S      $��* �6,   .+�5V      �� $��0��6!   $��0��61   ���#2c2c2d$��0q�6S   ._�5W      �   5X   "   �� 5Y   &   $��* �67   �� 5Z   &   $��* �67   �� 5[   &   $��* �67   �� 5\   &   $��* �67   �� 5]   &   $��* �67    5_   "   �� 5`   &   $��* �64   .+�5c   "   �� -
   5        $ txtPartidosJugados��5c   ,   $��0��6H   $��0��6X   $��16(�6e   $��JUGADOS��6s   $��Center=�6�   $��Center=�5d   .   $��Cascadia Code SemiBold)�6K   
�������6o   �� 5e   "   -   5        $ txtPartidosGanados��5e   ,   $��1��6H   $��0��6X   $��16(�6e   $��	VICTORIAS��6s   $��Center=�6�   $��Center=�5f   .   $��Cascadia Code SemiBold)�6K   
�������6o   �� 5g   "   -   5        $ txtPartidosEmpatados��5g   ,   $��2��6J   $��0��6Z   $��16(�6g   $��EMPATES��6u   $��Center=�6�   $��Center=�5h   ,   $��Cascadia Code SemiBold)�6I   
�������6m   �� 5i   "   -   5        $ txtPartidosPerdidos��5i   ,   $��3��6I   $��0��6Y   $��16(�6f   $��DERROTAS��6t   $��Center=�6�   $��Center=�5j   .   $��Cascadia Code SemiBold)�6K   
�������6o   �� 5k   "   -   5        $ txtPorcentaje��5k   ,   $��4��6C   $��0��6S   $��16(�6`   $��% VICTORIAS��6n   $��Center=�6�   $��Center=�5l   .   $��Cascadia Code SemiBold)�6K   
�������6o   5m      5n      �� 5p      $��0��6!   $��1��61   
�������$��0q�6V   ���#1d6a7d._�5q      �   5r   "   �� 5s   &   $��* �67   �� 5t   &   $��* �67   �� 5u   &   $��* �67   �� 5v   &   $��* �67   �� 5w   &   $��* �67    5y   "   �� 5z   &   $��* �64   .+�5}   "   �� -   5        $ txtPartidosJugadosValor��5}   ,   $��0��6M   $��0��6]   $��18(�6j   $��0��6x   $��Center=�6�   $��Center=�5~   ,   $��Cascadia Code SemiBold)�6I   
���   �6m   �� 5   "   -   5        $ txtPartidosGanadosValor��5   ,   $��1��6M   $��0��6]   $��18(�6j   $��0��6x   $��Center=�6�   $��Center=�5�   ,   $��Cascadia Code SemiBold)�6I   
���   �6m   �� 5�   "   -   5        $ txtPartidosEmpatadosValor��5�   ,   $��2��6O   $��0��6_   $��18(�6l   $��0��6z   $��Center=�6�   $��Center=�5�   ,   $��Cascadia Code SemiBold)�6I   
���   �6m   �� 5�   "   -   5        $ txtPartidosPerdidosValor��5�   ,   $��3��6N   $��0��6^   $��18(�6k   $��0��6y   $��Center=�6�   $��Center=�5�   ,   $��Cascadia Code SemiBold)�6I   
���   �6m   �� 5�   "   -   5        $ txtPorcentajeValor��5�   ,   $��4��6H   $��0��6X   $��18(�6e   $��0.00 %��6s   $��Center=�6�   $��Center=�5�   ,   $��Cascadia Code SemiBold)�6I   
���   �6m   5�      5�      5�      5�      5�      5�      �� 5�   
   
�������$��2q�5�      ���#1d6a7d$��5,5,5,5q�5�      $��1��5�      $��0��6!   $	��Top=�6.   $	��455��6F   ._�5�      �   5�      �� 5�      $��* �6'    5�      �� 5�      $��42 �6$   �� 5�      $��* �6$   .+�5�      �� $��0��6   $��0��6)   ���#1d6a7d$��0q�6K   ���#1d6a7d._�5�      �� -   5        $ txtRankingEntrenadores��5�       $��16(�6@   $��RANKING ENTRENADORES��6N   $��Center=�6j   $��Center=�5�       $��Cascadia Code SemiBold)�6=   
�������6a   5�      � 5�      $��0��6   $��1��6,   $��5q�69   .+�5�      &   System.Windows.Controls.DataGrid  -   5          Name$ dgRankingEntrenadores��5�      $
��1110��6>      
IsReadOnly$
 True��6K      AutoGenerateColumns$ False��6]   	   GridLinesVisibility$
	 None=�5�      $��Cascadia Code Light)�6:   $��16(�6[   $	��Top=�6i   $	��399��6~   5�      5�      5�      �� 5�   
   
�������$��2q�5�      ���#1d6a7d$��5,5,5,5q�5�      $��0��5�      $��1��6!   $��2��6.   $	��Top=�6B   $	��455��6Z   ._�5�      �   5�      �� 5�      $��* �6'   �� 5�      $��2* �6'    5�      �� 5�      $��42 �6$   �� 5�      $��* �6$   �� 5�      $��* �6$   �� 5�      $��* �6$   .+�5�      �� $��0��6   $��0��6)   $��2��66   ���#1d6a7d$��0q�6_   ���#1d6a7d._�5�      �� -   5        $ txtRelaciones��5�       $��16(�67   $��
RELACIONES��6E   $��Center=�6W   $��Center=�5�       $��Cascadia Code SemiBold)�6=   
�������6a   5�      �� 5�      $��0��6   $��1��6)   
�������$��0q�6M   
�������$��10q�6y   ._�5�      �   5�      �� 5�      $��2* �6/   �� 5�      $��* �6/    5�      �� 5�      $��* �6,   .+�5�      �� $��0��6$   $��0��64   $��16(�6B   $��Confianza de la Directiva��6P   $��Center=�6q   $
��Left=�6�   $��Cascadia Code SemiBold)�5�   $   
���   �$��50,0,0,0q�6[   A� 5�      -   5        
 A�Name$
 elipseDirectiva��5�   "   $��1��6;   $��0��6K   $��80��6X   $��Center=�6d   $��Center=�5�   "   
���   �$��80��6N   $��10��6Y   �� 5�      -   5        $ txtDirectiva��5�   $   $��1��6:   $��0��6J   $��20(�6W   $��99��6e   $��Center=�6o   $��Center=�5�   $   $��Cascadia Code SemiBold)�6A   
���   �6e   5�      5�      �� 5�      $��0��6   $��2��6)   
�������$��0q�6M   
�������$��10q�6y   ._�5�      �   5�      �� 5�      $��2* �6/   �� 5�      $��* �6/    5�      �� 5�      $��* �6,   .+�5�      �� $��0��6$   $��0��64   $��16(�6B   $��Confianza de los Fans��6P   $��Center=�6m   $
��Left=�6�   $��Cascadia Code SemiBold)�5�   $   
���   �$��50,0,0,0q�6[   A� 5�      -   5        $
 
elipseFans��5�   "   $��1��66   $��0��6F   $��80��6S   $��Center=�6_   $��Center=�5�   "   
���   �$��80��6N   $��10��6Y   �� 5�      -   5        $ txtFans��5�   $   $��1��65   $��0��6E   $��20(�6R   $��99��6`   $��Center=�6j   $��Center=�5�   $   $��Cascadia Code SemiBold)�6A   
���   �6e   5�      5�      �� 5�      $��0��6   $��3��6)   
�������$��0q�6M   
�������$��10q�6y   ._�5�      �   5�      �� 5�      $��2* �6/   �� 5�      $��* �6/    5�      �� 5�      $��* �6,   .+�5�      �� $��0��6$   $��0��64   $��16(�6B   $ ��Confianza de los Jugadores��6P   $��Center=�6r   $
��Left=�6�   $��Cascadia Code SemiBold)�5�   $   
���   �$��50,0,0,0q�6[   A� 5�      -   5        $
 elipseJugadores��5�   "   $��1��6;   $��0��6K   $��80��6X   $��Center=�6d   $��Center=�5�   "   
���   �$��80��6N   $��10��6Y   �� 5�      -   5        $ txtJugadores��5�   $   $��1��6:   $��0��6J   $��20(�6W   $��99��6e   $��Center=�6o   $��Center=�5�   $   $��Cascadia Code SemiBold)�6A   
���   �6e   5�      5�      �� 5�      $��1��6   $��1��6)   
�������$��0q�6M   
�������$��10q�6y   ._�5�      �� -   5        $ txtOpinionDirectiva��5�       $��0��6=   $��0��6M   $��16(�6Z   $��Opinión de la Directiva��6h   $��Center=�6�   $��Center=�5�       $��Cascadia Code Light)�6=   
���   �$
��Wrap=�6q   $
��1148��6�   5�      �� 5�      $��1��6   $��2��6)   
�������$��0q�6M   
�������$��10q�6y   ._�5      �� -   5        $ txtOpinionFans��5       $��0��68   $��0��6H   $��16(�6U   $��Opinión de los Fans��6c   $��Center=�6~   $��Center=�5      $��Cascadia Code Light)�6=   
���   �$
��Wrap=�6q   $
��1148��6�   5     �� 5     $��1��6   $��3��6)   
�������$��0q�6M   
�������$��10q�6y   ._�5     �� -   5        $ txtOpinionJugadores��5      $��0��6=   $��0��6M   $��16(�6Z   $��Opinión de los Jugadores��6h   $��Center=�6�   $��Center=�5      $��Cascadia Code Light)�6=   
���   �$
��Wrap=�6q   $
��1148��6�   5     5     5	     5
     5     