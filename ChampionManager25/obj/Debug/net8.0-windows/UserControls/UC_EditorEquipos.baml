   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      5    /ChampionManager25.UserControls.UC_EditorEquipos   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      -   5        $
��1080��5      $
��1920��6-   .��5      � ��5
   
   �� 5        �� ImageSource$4  ./Recursos/img/backgroundPrincipal1920x1080.png��6    j� Stretch$ UniformToFill=�6V    j� TileMode$
 None=�6n    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��6* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��80 �6   �� 5      $	��220 �6   �� 5      $��10 �6   �� 5      $
��auto �6   �� 5      $��40 �6   .+�5   
   �� -   5         ��Name$ borderBarraSuperior��5      $��0��6.   $��0��6>   $��3��6K   ���#23282d._�5      �  5      �� 5      $��* �6'   �� 5       $��3* �6'   �� 5!      $��* �6'    5#      �� 5$      $��* �6$   .+�5'      �� $��0��6   $��0��6)   ���#23282d._�5(      �  5)      �� 5*      $��80 �6/   �� 5+      $��* �6/    5-      �� 5.      $��* �6,   .+�51      �� $��0��6!   $��0��61   
�������._�52      �� -   5         ��Name$ imgBotonAtras��52   $   $��32��6;   $��32��6F   $��Center=�6R   $
��Left=�6m   $��20,0,0,0q�6�   $;��5/Recursos/img/icons/flechaIzquierdaNegra64px_icon.png��53       � Cursor$
 Hand��6[   6i   54      55      56      �� 57      -   5         ��Name$ lblSeleccionEquipo��57      $��1��68   $��0��6H   $��Center=�6U   $��Center=�6p   $��EDITAR EQUIPOS��6�   
�������$��28(�58   4   $��Cascadia Code SemiBold)�6B   59      5:      �� 5=   
   -   5        $ borderCompeticiones��5=      $��1��6.   $��1��6>   
�������$��20q�6c   	 �� CornerRadius$	 10��6o   
 J� Effect
 5>      3  -System.Windows.Media.Effects.DropShadowEffect  5?         Color$ Black��6#      
BlurRadius$ 10Q�5@         ShadowDepth$ 5Q�5A         	Direction$	 320Q�5B         Opacity$	 0.5Q�5C      ._�5F      �  5G      �� 5H      $��* �6'   �� 5I      $��* �6'   �� 5J      $��* �6'   �� 5K      $��* �6'   �� 5L      $��* �6'    5N      �� 5O      $��* �6$   .+�5R      �� -   5        $ imgCompeticion��5R      $��1��60   $��0��6@   $	��150��6M   $	��150��6Y   $��Uniform=�6f   $��Center=�6x   $��Center=�5S      $��10q�65   $
 Hand��6A   6O   �� 5T      -   5        $ imgCompeticion2��5T      $��2��61   $��0��6A   $	��150��6N   $	��150��6Z   $��Uniform=�6g   $��Center=�6y   $��Center=�5U      $��10q�65   $
 Hand��6A   6O   �� 5V      -   5        $ imgCompeticion3��5V      $��3��61   $��0��6A   $	��150��6N   $	��150��6Z   $��Uniform=�6g   $��Center=�6y   $��Center=�5W      $��10q�65   $
 Hand��6A   6O   5Y      5Z      �� 5]   
   -	   5        $ borderEquipos��5]      $��1��6(   $��3��68   
�������$��20q�6]   $	 10��6i   
 5^        5_      $ Black��6#   $ 10Q�5`   #   $ 5Q�5a   #   $	 320Q�5b   #   $	 0.5Q�5c   #   ._�5f      �  5g      �� 5h      $��* �6'    5j      �� 5k      $
��auto �6$   �� 5l      $��70 �6$   .+�5o      � -
   5        $��wrapPanelEquipos��5o      $��0��64   $��Center=�6A   $
��auto��6^    � 	ItemWidth$	 130��6l    � 
ItemHeight$	 130��6|   $��10q�6�   ]� 5q      $��0��6   $��1��6,   $��50��69   $
��1300��6E   $��Center=�6R   $��Bottom=�6o   $��10q�6�   .+�5r      �� -   5         ��Name$ btnEditarEquipo��5r      $��EDITAR EQUIPO��66   $
��1250��6N   $��40��6[   $��Center=�6g   $��Center=�6�   $��Cascadia Code SemiBold)�5s      $��18(�6A   $
 Hand��6O   ${�False��6]   ��5t      �� 5u       �� 
TargetType" ��6$   .�5w   "   ��  �� Property � �� �� Value �#1d6a7d6?   �� 5x   "    � ��
 �����6?   �� 5y   "    � ��
 ���� 6@   �� 5z   "    � ��$ 0q�6D   �� 5{   "    �@�Cursor$
 Hand��6;   �� 5|   "    � �� 5}   &   �� 5~   *    �� 
TargetType" ��6:   .R�5   .   �� #��z��65   #��z��5�   -   #��z��5�   -   $	 5��5�   -   ._�5�   2   ��  �� Orientation$ 
Horizontal=�6=   $��Center=�6V   $��Center=�6s   .+�5�   6   �� #��z��6@   #��z��5�   8   $��Center=�5�   8   $��0,0,5,0q�5�   8   5�   3   5�   /   5�   +   5�   #    �� Triggers 5�   "   P� 5�   &    P� Property �@J�IsMouseOver P� Value$
 True��6E   . �5�   *   ��  � �� �#1d6a7d6H   �� 5�   *    � ��$ 1q�6L   5�   '   5�      5�      5�      5�      5�      5�      5�      