   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      E    ?ChampionManager25.UserControls.UC_Menu_Entrenador_Entrenamiento   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      -   5          >�Name$  entrenamiento��5      .��5      � 
������� � ColumnDefinitions 5	   
   �� 5
      $
��1845 �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   ���#1d6a7d$��1q�6D   $��10q�6X   ._�5      � 
������� 5      �� 5      $��* �6'    5      �� 5      $��50 �6$   �� 5      $��* �6$   .+�5      �� ���#1d6a7d$��0��6.   $��0��6>   ._�5      �� $��ENTRENAMIENTO��6    $��Center=�65   $��Center=�6R   $��Cascadia Code SemiBold)�5       $��22(�6D   
������� �� TextAlignment$ Center=�6j   5      �� 5      $��0��6   $��1��6)   ._�5      �  5       �� 5!      $	��560 �6/   �� 5"      $��* �6/    5$      �� 5%      $��* �6,   .+�5)      �� $��0��6!   $��0��61   $��10,10,10,10q�6>   ���#1d6a7d$��1q�6i   ._�5*      �  5+   "   �� 5,   &   $��* �67    5.   "   �� 5/   &   $��30 �64   �� 50   &   $��35 �64   �� 51   &   $	��775 �64   .+�54   "   �� ���#1d6a7d$��0��6>   $��0��6N   ._�55   &   �� $��LISTA DE JUGADORES��60   $��Center=�6J   $��Center=�6g   $��Cascadia Code SemiBold)�56   ,   $��22(�6P   
�������$ Center=�6v   57   #   �� 59   "   ���#1d6a7d$��0��6>   $��1��6N   $��0,10,0,0q�6[   ._�5:   &   �  5;   *   �� 5<   .   $��80 �6?   �� 5=   .   $	��250 �6?   �� 5>   .   $��80 �6?   �� 5?   .   $��80 �6?    5A   *   �� 5B   .   $��* �6<   .+�5E   *   �� $��0��64   $��0��6D   $	��NAC��6Q   $��Center=�6\   $��Center=�6y   $��Cascadia Code SemiBold)�5F   4   $��16(�6X   
�������$ Center=�6~   $��5,0,0,0q�6�   �� 5G   *   $��1��64   $��0��6D   $��Nombre��6Q   $
��Left=�6_   $��Center=�6z   $��Cascadia Code SemiBold)�5H   4   $��16(�6X   
�������$ Center=�6~   $��5,0,0,0q�6�   �� 5I   *   $��2��64   $��0��6D   $	��DEM��6Q   $��Center=�6\   $��Center=�6y   $��Cascadia Code SemiBold)�5J   4   $��16(�6X   
�������$ Center=�6~   $��10,0,0,0q�6�   �� 5K   *   $��3��64   $��0��6D   $	��MED��6Q   $��Center=�6\   $��Center=�6y   $��Cascadia Code SemiBold)�5L   4   $��16(�6X   
�������$ Center=�6~   $��10,0,0,0q�6�   5M   '   5N   #   �� 5P   "   $
��Auto=�6/   $��0��6R   $��2��6b   $��Center=�6o   $	��757��6�   .��5Q   &   � -   5         �Name$ wrapJugadores��5Q   0   $	��Top=�6G   5R   #   5S      5T      �� 5W      $��1��6!   $��0��61   $��10,10,10,10q�6>   ���#1d6a7d$��1q�6i   ._�5X      �  5Y   "   �� 5Z   &   $��* �67    5\   "   �� 5]   &   $	��120 �64   �� 5^   &   $	��370 �64   �� 5_   &   $	��100 �64   �� 5`   &   $	��100 �64   �� 5a   &   $��* �64   .+�5e   "   �� 
�������$��0��6@   $��0��6P   ._�5f   &   �  5g   *   �� 5h   .   $	��200 �6?   �� 5i   .   $��* �6?    5k   *   �� 5l   .   $��* �6<   .+�5o   *   �� -   5         ��Name$ imgFotoJugador��5o   0   $��0��6H   $��0��6X   $	��100��6e   $	��100��6r   $��Center=�5p   0   $��Center=�6M    � Cursor$
 Hand��6h   �� 5q   *   -   5         ��Name$ txtNombreJugador��5q   4   $��1��6N   $��0��6^   $�� ��6k   $
��Left=�6s   $��Center=�6�   $��Cascadia Code SemiBold)�5r   4   $��32(�6X   
���   �$ Center=�6y   $��20,0,0,0q�6�   5s   '   5t   #   �� 5w   "   $��0��6)   $��1��69   ���#1d6a7d._�5x   &   �  5y   *   �� 5z   .   $	��500 �6?   �� 5{   .   $��* �6?    5}   *   �� 5~   .   $	��370 �6<   .+�5�   *   �� $��0��61   $��0��6A   ._�5�   .   �  5�   2   �� 5�   6   $��* �6G    5�   2   �� 5�   6   $	��100 �6D   �� 5�   6   $��* �6D   .+�5�   2   �� $��0��6<   $��0��6L   $��Entrenando...��6Y   $��Center=�6n   $��Center=�6�   $��Cascadia Code SemiBold)�5�   <   $��24(�6`   
�������$ Center=�6�   �� 5�   2   -   5        $ txtTipoEntrenamiento��5�   <   $��0��6Z   $��1��6j   $�� ��6w   $��Center=�6   $��Center=�6�   $��Cascadia Code SemiBold)�5�   <   $��48(�6`   
�������$ Center=�6�   5�   /   5�   +   �� 5�   *   -   5        $ imgEntrenamiento��5�   0   $��1��6J   $��UniformToFill=�6Z   $��Center=�5�   0   $��Center=�6M   $
 Hand��6h   $ ��/Recursos/img/training.png��5�   0   5�   '   5�   #   �� 5�   "   $��0��6)   $��2��69   
�������._�5�   &   �  5�   *   �� 5�   .   $��* �6?   �� 5�   .   $��* �6?   �� 5�   .   $��* �6?    5�   *   �� 5�   .   $��* �6<   .+�5�   *   �� -   5         ��Name$ 
btnPortero��5�   1   $��0��6E   $��0��6U   $��PORTERO��6b   $��Center=�6t   $��Center=�6�   $��50��5�   1   $	��250��6=   $��Cascadia Code SemiBold)�6I   $��22(�6m   $
 Hand��6{   ${�False��6�   ��5�   .   �� 5�   2   	 �� 
TargetType"	 ��68   .�5�   6   �� 
 �� Property
 � �� �� Value �	#FF23282D6S   �� 5�   6   
 � ��
 �����6S   �� 5�   6   
 � ��$ 22(�6Q   �� 5�   6   
 �@J�SnapsToDevicePixels$
 True��6\   �� 5�   6   
 � �� 5�   :   �� 5�   >    �� 
TargetType" ��6N   .R�5�   B   ��  �� CornerRadius$ 4��6I   #��z��6Z   ._�5�   F   � .+�5�   J   �� $ imgBoton��6P   $*��$/Recursos/img/icons/portero_icon.png��6b   $��32��6�   $��32��6�   $��Center=�6�   $
��Left=�6�   $��Uniform=�6�   $��17,0,0,0q�6�   �� 5�   J    ��Name$ MyContentPresenter��6[   #��z��6w   $��Center=�6�   $��Center=�6�   $��30,0,0,0q�6�   5�   G   5�   C    �� Triggers 5�   B   P� 5�   F    P� Property �@J�IsMouseOver P� Value$
 True��6e   . �5�   J   �� 
 � �� �#2d343a6g   �� 5�   J   
 � ��
 �����6g   5�   G   5�   ?   5�   7   5�   3   5�   +   �� 5�   *   -   5        $ btnEntradas��5�   1   $��1��6F   $��0��6V   $��ENTRADAS��6c   $��Center=�6v   $��Center=�6�   $��50��5�   1   $	��250��6=   $��Cascadia Code SemiBold)�6I   $��22(�6m   $
 Hand��6{   ${�False��6�   ��5�   .   �� 5�   2   "	 ��68   .�5�   6   �� 
 � �� �	#FF23282D6S   �� 5�   6   
 � ��
 �����6S   �� 5�   6   
 � ��$ 22(�6Q   �� 5�   6   
 �@J�SnapsToDevicePixels$
 True��6\   �� 5�   6   
 � �� 5�   :   �� 5�   >   " ��6N   .R�5�   B   �� $ 4��6I   #��z��6Z   ._�5�   F   � .+�5�   J   �� $ imgBoton��6P   $*��$/Recursos/img/icons/entrada_icon.png��6b   $��32��6�   $��32��6�   $��Center=�6�   $
��Left=�6�   $��Uniform=�6�   $��17,0,0,0q�6�   �� 5�   J   $ MyContentPresenter��6[   #��z��6w   $��Center=�6�   $��Center=�6�   $��30,0,0,0q�6�   5�   G   5�   C    5�   B   P� 5�   F    �@J�IsMouseOver$
 True��6e   . �5�   J   �� 
 � �� �#2d343a6g   �� 5�   J   
 � ��
 �����6g   5�   G   5�   ?   5�   7   5�   3   5�   +   �� 5�   *   -	   5        $ 	btnRemate��5�   1   $��2��6D   $��0��6T   $��REMATE��6a   $��Center=�6r   $��Center=�6�   $��50��5�   1   $	��250��6=   $��Cascadia Code SemiBold)�6I   $��22(�6m   $
 Hand��6{   ${�False��6�   ��5�   .   �� 5�   2   "	 ��68   .�5�   6   �� 
 � �� �	#FF23282D6S   �� 5�   6   
 � ��
 �����6S   �� 5�   6   
 � ��$ 22(�6Q   �� 5�   6   
 �@J�SnapsToDevicePixels$
 True��6\   �� 5�   6   
 � �� 5�   :   �� 5�   >   " ��6N   .R�5�   B   �� $ 4��6I   #��z��6Z   ._�5�   F   � .+�5�   J   �� $ imgBoton��6P   $)��#/Recursos/img/icons/remate_icon.png��6b   $��32��6�   $��32��6�   $��Center=�6�   $
��Left=�6�   $��Uniform=�6�   $��17,0,0,0q�6�   �� 5�   J   $ MyContentPresenter��6[   #��z��6w   $��Center=�6�   $��Center=�6�   $��30,0,0,0q�6�   5�   G   5�   C    5�   B   P� 5�   F    �@J�IsMouseOver$
 True��6e   . �5�   J   �� 
 � �� �#2d343a6g   �� 5�   J   
 � ��
 �����6g   5�   G   5  ?   5  7   5  3   5  +   5  '   5  #   �� 5  "   $��0��6)   $��3��69   
�������._�5  &   �  5  *   �� 5  .   $��* �6?   �� 5  .   $��* �6?   �� 5  .   $��* �6?    5  *   �� 5  .   $��* �6<   .+�5  *   �� -
   5        $ btnPase��5  1   $��0��6B   $��0��6R   $
��PASE��6_   $��Center=�6n   $��Center=�6�   $��50��5  1   $	��250��6=   $��Cascadia Code SemiBold)�6I   $��22(�6m   $
 Hand��6{   ��5  .   �� 5  2   "	 ��68   .�5  6   �� 
 � �� �	#FF23282D6S   �� 5  6   
 � ��
 �����6S   �� 5  6   
 � ��$ 22(�6Q   �� 5  6   
 �@J�SnapsToDevicePixels$
 True��6\   �� 5   6   
 � �� 5!  :   �� 5"  >   " ��6N   .R�5#  B   �� $ 4��6I   #��z��6Z   ._�5$  F   � .+�5%  J   �� $ imgBoton��6P   $'��!/Recursos/img/icons/pase_icon.png��6b   $��32��6�   $��32��6�   $��Center=�6�   $
��Left=�6�   $��Uniform=�6�   $��17,0,0,0q�6�   �� 5&  J   $ MyContentPresenter��6[   #��z��6w   $��Center=�6�   $��Center=�6�   $��30,0,0,0q�6�   5'  G   5(  C    5*  B   P� 5+  F    �@J�IsMouseOver$
 True��6e   . �5,  J   �� 
 � �� �#2d343a6g   �� 5-  J   
 � ��
 �����6g   5.  G   50  ?   52  7   53  3   55  +   �� 58  *   -   5        $ 	btnRegate��58  1   $��1��6D   $��0��6T   $��REGATE��6a   $��Center=�6r   $��Center=�6�   $��50��59  1   $	��250��6=   $��Cascadia Code SemiBold)�6I   $��22(�6m   $
 Hand��6{   ${�False��6�   ��5:  .   �� 5;  2   "	 ��68   .�5<  6   �� 
 � �� �	#FF23282D6S   �� 5=  6   
 � ��
 �����6S   �� 5>  6   
 � ��$ 22(�6Q   �� 5?  6   
 �@J�SnapsToDevicePixels$
 True��6\   �� 5A  6   
 � �� 5B  :   �� 5C  >   " ��6N   .R�5D  B   �� $ 4��6I   #��z��6Z   ._�5E  F   � .+�5F  J   �� $ imgBoton��6P   $)��#/Recursos/img/icons/regate_icon.png��6b   $��32��6�   $��32��6�   $��Center=�6�   $
��Left=�6�   $��Uniform=�6�   $��17,0,0,0q�6�   �� 5G  J   $ MyContentPresenter��6[   #��z��6w   $��Center=�6�   $��Center=�6�   $��30,0,0,0q�6�   5H  G   5I  C    5K  B   P� 5L  F    �@J�IsMouseOver$
 True��6e   . �5M  J   �� 
 � �� �#2d343a6g   �� 5N  J   
 � ��
 �����6g   5O  G   5Q  ?   5S  7   5T  3   5V  +   �� 5Y  *   -   5        $ btnTiro��5Y  1   $��2��6B   $��0��6R   $
��TIRO��6_   $��Center=�6n   $��Center=�6�   $��50��5Z  1   $	��250��6=   $��Cascadia Code SemiBold)�6I   $��24(�6m   $
 Hand��6{   ${�False��6�   ��5[  .   �� 5\  2   "	 ��68   .�5]  6   �� 
 � �� �	#FF23282D6S   �� 5^  6   
 � ��
 �����6S   �� 5_  6   
 � ��$ 22(�6Q   �� 5`  6   
 �@J�SnapsToDevicePixels$
 True��6\   �� 5b  6   
 � �� 5c  :   �� 5d  >   " ��6N   .R�5e  B   �� $ 4��6I   #��z��6Z   ._�5f  F   � .+�5g  J   �� $ imgBoton��6P   $'��!/Recursos/img/icons/tiro_icon.png��6b   $��32��6�   $��32��6�   $��Center=�6�   $
��Left=�6�   $��Uniform=�6�   $��17,0,0,0q�6�   �� 5h  J   $ MyContentPresenter��6[   #��z��6w   $��Center=�6�   $��Center=�6�   $��30,0,0,0q�6�   5i  G   5j  C    5l  B   P� 5m  F    �@J�IsMouseOver$
 True��6e   . �5n  J   �� 
 � �� �#2d343a6g   �� 5o  J   
 � ��
 �����6g   5p  G   5r  ?   5t  7   5u  3   5w  +   5x  '   5y  #   �� 5|  "   $��0��6)   $��4��69   
�������._�5}  &   �  5~  *   �� 5  .   $��* �6?    5�  *   �� 5�  .   $��* �6<   .+�5�  *   �� -   5        $ btnNoEntrenar��5�  1   $��5��6H   $��SIN ENTRENAMIENTO��6X   $��Center=�6t   $��Center=�6�   $��50��5�  1   $
��1080��6=   $��Cascadia Code SemiBold)�6J   $��24(�6n   $
 Hand��6|   ${�False��6�   ��5�  .   �� 5�  2   "	 ��68   .�5�  6   �� 
 � �� �	#FF23282D6S   �� 5�  6   
 � ��
 �����6S   �� 5�  6   
 � ��$ 24(�6Q   �� 5�  6   
 �@J�SnapsToDevicePixels$
 True��6\   �� 5�  6   
 � �� 5�  :   �� 5�  >   " ��6N   .R�5�  B   �� $ 4��6I   #��z��6Z   ._�5�  F   � .+�5�  J   �� $ imgBoton��6P   $+��%/Recursos/img/icons/cancelar_icon.png��6b   $��32��6�   $��32��6�   $��Center=�6�   $
��Left=�6�   $��Uniform=�6�   $��17,0,0,0q�6�   �� 5�  J   $ MyContentPresenter��6[   #��z��6w   $��Center=�6�   $��Center=�6�   $��0,0,0,0q�6�   5�  G   5�  C    5�  B   P� 5�  F    �@J�IsMouseOver$
 True��6e   . �5�  J   �� 
 � �� �#2d343a6g   �� 5�  J   
 � ��
 �����6g   5�  G   5�  ?   5�  7   5�  3   5�  +   5�  '   5�  #   5�     5�     5�     5�     5�     5�     5�     5�     