   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      ;    5ChampionManager25.UserControls.UC_ResumenPresentacion   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      -   5          >�Name$  resumenRuedaPrensa��5      $
��1080��5      $
��1920��6-   .��5      � ��5
   
   �� 5       �� ImageSource$4 ./Recursos/img/backgroundPrincipal1920x1080.png��6    j� Stretch$ UniformToFill=�6V    j� TileMode$
 None=�6n    � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5      $��* �6   �� 5      $	��150 �6    � RowDefinitions 5   
   �� 5      $��80 �6   �� 5      $	��550 �6   �� 5      $��50 �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ borderBarraSuperior��5      $��0��6.   $��0��6>   $��3��6K   ���#23282d._�5      �  5      �� 5      $��* �6'   �� 5      $��3* �6'   �� 5       $��* �6'    5"      �� 5#      $��* �6$   .+�5&      � -   5         �Name$ progressBar��5&      $y�	Collapsed=�63   $��0��6J   $��0��6Z    � IsIndeterminate$
 True��6g   ���#1d6a7d$��1q�5'      $��20��62   $	��250��6>   $��Center=�6J   ���#1d6a7d
�������6z   �� 5)      -   5        	 ��Name$	 lblSeleccionEquipo��5)      $��1��68   $��0��6H   $��Center=�6U   $��Center=�6p   $&�� Rueda de Prensa de Presentación��5*      
�������$��28(�6S   $��Cascadia Code SemiBold)�6a   �� 5,      -   5        
 ��Name$
 
btnAvanzar��5,      $��2��6-   $��0��6=   $��	CONTINUAR��6J   $��Center=�6^   $��Right=�6y   $��50��5-      $	��280��6%   $��0,0,20,0q�61   $��Cascadia Code SemiBold)�6C   $��18(�6g    � Cursor$
 Hand��6u   ��5.      �� 5/       �� 
TargetType" ��6    .�50      ��  �� Property � �� �� Value �#1d6a7d6;   �� 51       � ��
 �����6;   �� 52       � ��$ 24(�69   �� 53       �@J�SnapsToDevicePixels$
 True��6D   �� 55       � �� 56   "   �� 57   &    �� 
TargetType" ��66   .R�58   *   ��  �� CornerRadius$ 4��61   #��z��6B   ._�59   .   � .+�5:   2   ��  ��Name$ imgBoton��68   $:��4/Recursos/img/icons/flechaDerechaBlanca64px_icon.png��6J   $��32��6�   $��32��6�   $��Center=�6�   $
��Left=�6�   $��Uniform=�6�   $��17,0,0,0q�6�   �� 5;   2    ��Name$ MyContentPresenter��6C   #��z��6_   $��Center=�6�   $��Center=�6�   $��20,0,0,0q�6�   5<   /   5=   +    �� Triggers 5?   *   P� 5@   .    P� Property �@J�IsMouseOver P� Value$
 True��6M   . �5A   2   ��  � �� �#22798e6O   �� 5B   2    � ��
 �����6O   5C   /   5E   '   5G      5H      5J      5K      5L      �� 5O   
   $��0��6   $��1��6!   $��3��6.   ._�5P      � ��5R      �� 5S      $. (/Recursos/img/microfonos_ruedaPrensa.png��6!   $ UniformToFill=�6X   $
 None=�6p   5U      5V      �� 5Y   
   $��0��6   $��2��6!   $��3��6.   ���#23282d._�5Z      �� $��Center=�6   $��Center=�63   $#��RESUMEN DE LA RUEDA DE PRENSA��5[      
�������$��24(�6U   $��Cascadia Code SemiBold)�6c   5\      �� 5_   
   $��0��6   $��3��6!   $��3��6.   
�������._�5`      �  5a      �� 5b      $	��350 �6'   �� 5c      $	��350 �6'   �� 5d      $	��350 �6'   �� 5e      $��* �6'    5g      �� 5h      $��40 �6$   �� 5i      $��* �6$   .+�5m      �� $��0��6   $��0��6)   $��4��66   ���#1d6a7d._�5n      �� $��Center=�6    $��Center=�6;   $��
REACCIONES��5o       
�������$��18(�6J   $��Cascadia Code SemiBold)�6X   5p      �� 5s      $��0��6   $��1��6)   
�������$��10q�6M   ._�5t      �  5u      �� 5v      $��* �6/    5x      �� 5y      $��70 �6,   �� 5z      $��* �6,   .+�5}      �� $��0��6$   $��0��64   $��Center=�6A   $��Center=�6\   $
��Wrap=�6y    �� TextAlignment$ Center=�5~   $   $��CONFIANZA DE LA DIRECTIVA��6;   ���#23282d$��18(�6q   $��Cascadia Code SemiBold)�5   $   $	��192��6H   A� 5�      -   5         A�Name$ elipseDirectiva��5�   "   $��0��6;   $��1��6K   $��Center=�6X   $	��115��6u   
���   �$��Center=�5�   "   $	��115��6=   $��10��6I   �� 5�      -   5        $	 lblPuntosDirectiva��5�   $   $��0��6@   $��1��6P   $��Center=�6]   $
��Wrap=�6z   $��Center=�5�   $   $ Center=�6?   $��Cascadia Code SemiBold)�6V   $��40(�6z   $��51��6�   $��00��5�   $   $��69��6.   5�      5�      �� 5�      $��1��6   $��1��6)   
�������$��10q�6M   ._�5�      �  5�      �� 5�      $��* �6/    5�      �� 5�      $��70 �6,   �� 5�      $��* �6,   .+�5�      �� $��0��6$   $��0��64   $��Center=�6A   $��Center=�6\   $
��Wrap=�6y   $ Center=�5�   $   $��CONFIANZA DE LOS FANS��6;   ���#23282d$��18(�6m   $��Cascadia Code SemiBold)�5�   $   $	��192��6H   A� 5�      -   5        $ 
elipseFans��5�   "   $��0��66   $��1��6F   $��Center=�6S   $	��115��6p   
���   �$��Center=�5�   "   $	��115��6=   $��10��6I   �� 5�      -	   5        $	 lblPuntosFans��5�   $   $��0��6;   $��1��6K   $��Center=�6X   $
��Wrap=�6u   $��Center=�5�   $   $ Center=�6?   $��Cascadia Code SemiBold)�6V   $��40(�6z   $��51��6�   $��00��5�   $   $��69��6.   5�      5�      �� 5�      $��2��6   $��1��6)   
�������$��10q�6M   ._�5�      �  5�      �� 5�      $��* �6/    5�      �� 5�      $��70 �6,   �� 5�      $��* �6,   .+�5�      �� $��0��6$   $��0��64   $��Center=�6A   $��Center=�6\   $
��Wrap=�6y   $ Center=�5�   $   $ ��CONFIANZA DE LOS JUGADORES��6;   ���#23282d$��18(�6r   $��Cascadia Code SemiBold)�5�   $   $	��192��6H   A� 5�      -
   5        $ elipseJugadores��5�   "   $��0��6;   $��1��6K   $��Center=�6X   $	��115��6u   
���   �$��Center=�5�   "   $	��115��6=   $��10��6I   �� 5�      -   5        $	 lblPuntosJugadores��5�   $   $��0��6@   $��1��6P   $��Center=�6]   $
��Wrap=�6z   $��Center=�5�   $   $ Center=�6?   $��Cascadia Code SemiBold)�6V   $��40(�6z   $��51��6�   $��00��5�   $   $��69��6.   5�      5�      �� 5�      $��3��6   $��1��6)   
�������$��10q�6M   ._�5�      �  5�      �� 5�      $��* �6/    5�      �� 5�      $��* �6,   �� 5�      $��* �6,   �� 5�      $��* �6,   .+�5�      �� -   5        $	 	lblFrase1��5�   $   $��0��67   $
��Left=�6G   $
��Wrap=�6b   $��Center=�6v   $��Cascadia Code Light)�5�   $   $��16(�6E   $��70��6S   $	��722��6_   $��Frase de la Directiva��6k   $��50,0,0,0q�6�   �� 5�      -   5        $	 	lblFrase2��5�   $   $��0��67   $��1��6G   $
��Left=�6T   $
��Wrap=�6o   $��Center=�6�   $��Cascadia Code Light)�5�   $   $��16(�6E   $��70��6S   $	��722��6_   $��Frase de los Fans��6k   $��50,0,0,0q�6�   �� 5�      -   5        $	 	lblFrase3��5�   $   $��0��67   $��2��6G   $
��Left=�6T   $
��Wrap=�6o   $��Center=�6�   $��Cascadia Code Light)�5�   $   $��16(�6E   $��70��6S   $	��722��6_   $��Frase de los Jugadores��6k   $��50,0,0,0q�6�   5�      5�      5�      5�      5�      5�      