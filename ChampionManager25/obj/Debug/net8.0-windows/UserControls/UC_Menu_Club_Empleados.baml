   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      ;    5ChampionManager25.UserControls.UC_Menu_Club_Empleados   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      -   5        .��5      � 
�������  � ColumnDefinitions  5	   
   �� 5
      $
��1320 �6   �� 5      $	��520 �6    � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ bordeEmpleados��5      
�������$��1q�6A   ���#1d6a7d$��10q�6k   $��0��5      $��0��6!   ._�5      � 
�������  5      �� 5      $��* �6'    5      �� 5      $��30 �6$   �� 5      $��* �6$   .+�5       �� $��0��6   $��0��6)   ���#1d6a7d66   �� 5!      $��0��6   $��0��6,   $��	EMPLEADOS��69   $��Center=�6J   $��Center=�6g   $��Cascadia Code SemiBold)�5"      
�������$��20(�6X   �� 5$      $��0��6   $��1��6)   
�������$��1q�6N   
�������$��10q�6z   ._�5%      � 
�������  5&      �� 5'      $��2* �6/   �� 5(      $��* �6/   �� 5)      $��* �6/   �� 5*      $��* �6/   �� 5+      $��* �6/    5-      �� 5.      $��30 �6,   �� 5/      $��* �6,   �� 50      $��* �6,   �� 51      $��* �6,   �� 52      $��* �6,   �� 53      $��* �6,   �� 54      $��* �6,   �� 55      $��* �6,   �� 56      $��* �6,   .+�5:      �� $��0��6!   $��0��61   $��5��6>   ���#1d6a7d6R   �� 5;      $��0��6$   $��0��64   $��EMPLEADO��6A   $��Center=�6Q   $��Center=�6n   $��Cascadia Code SemiBold)�5<   $   
�������$��16(�6`   �� 5=      $��1��6$   $��0��64   $��PUESTO��6A   $��Center=�6O   $��Center=�6l   $��Cascadia Code SemiBold)�5>   $   
�������$��16(�6`   �� 5?      $��2��6$   $��0��64   $��
CATEGORÍA��6A   $��Center=�6R   $��Center=�6o   $��Cascadia Code SemiBold)�5@   $   
�������$��16(�6`   �� 5A      $��3��6$   $��0��64   $��SALARIO��6A   $��Center=�6P   $��Center=�6m   $��Cascadia Code SemiBold)�5B   $   
�������$��16(�6`   �� 5E      $��0��6!   $��1��61   $��5��6>   
�������6R   �� 5F      -   5         ��Name$  txtNombreSegundoEntrenador��5F   $   $��0��6H   $��1��6X   $��-��6e   $��Center=�6n   $��Center=�6�   $��Cascadia Code Light)�5G   $   
���   �$��14(�6X    � Cursor$
 Hand��6f   6t   �� 5H      $��1��6$   $��1��64   $��Segundo Entrenador��6A   $��Center=�6[   $��Center=�6x   $��Cascadia Code Light)�5I   $   
���   �$��14(�6X   �� 5J      -   5         ��Name$ canvasSegundoEntrenador��5J   !   $��2��6B   $��1��6R   $��Center=�6_   $��Center=�6|   $��30��5K   !   $	��155��6-   �� 5L      -   5        $! txtSalarioSegundoEntrenador��5L   $   $��3��6I   $��1��6Y   $��-��6f   $��Center=�6o   $��Center=�6�   $��Cascadia Code Light)�5M   $   
���   �$��14(�6X   �� 5N      -   5         ��Name$# btnContratarSegundoEntrenador��5N   !   $��4��6H   $��1��6X   $��30��6e   $	��200��6q   $��CONTRATAR EMPLEADO��6}   $��Center=�5O   !   $��Center=�6>   ���#23282d
�������$��Cascadia Code SemiBold)�5P   !   $��16(�6E   $
 Hand��6S   ��5Q      �� 5R   "    �� 
TargetType" ��6(   .�5T   &   ��  �� Property � ��	 �� Value	 �#23282d6C   �� 5U   &    � ��
	 ���� 6D   �� 5V   &    � ��
	 �����6C   �� 5W   &    � ��$	 0q�6H   �� 5X   &    � ��	 5Y   *   �� 5Z   .   
 �� 
TargetType"
 ��6>   .R�5[   2   �� #��z��69   #��z��5\   9   #��z��5]   9   ._�5^   6   �� $��Center=�6G   $��Center=�6d   5_   3   5`   /   5b   '   5c   #   5e      �� 5g      -   5        $" btnDespedirSegundoEntrenador��5g   !   $��4��6G   $��1��6W   $��30��6d   $	��200��6p   $��DESPEDIR EMPLEADO��6|   $��Center=�5h   !   $��Center=�6>   ���#941f13
�������$��Cascadia Code SemiBold)�5i   !   $��16(�6E   $
 Hand��6S   $y�Hidden=�6�   ��5j      �� 5k   "   " ��6(   .�5m   &   ��  � ��	 �#941f136C   �� 5n   &    � ��
	 ���� 6D   �� 5o   &    � ��
	 �����6C   �� 5p   &    � ��$	 0q�6H   �� 5q   &    � ��	 5r   *   �� 5s   .   "
 ��6>   .R�5t   2   �� #��z��69   #��z��5u   9   #��z��5v   9   ._�5w   6   �� $��Center=�6G   $��Center=�6d   5x   3   5y   /   5{   '   5|   #   5~      �� 5�      $��0��6!   $��2��61   $��5��6>   
�������6R   �� 5�      -   5        $ txtNombreDelegado��5�   $   $��0��6?   $��2��6O   $��-��6\   $��Center=�6e   $��Center=�6�   $��Cascadia Code Light)�5�   $   
���   �$��14(�6X   $
 Hand��6f   6t   �� 5�      $��1��6$   $��2��64   $��Delegado��6A   $��Center=�6Q   $��Center=�6n   $��Cascadia Code Light)�5�   $   
���   �$��14(�6X   �� 5�      -	   5        $ canvasDelegado��5�   !   $��2��69   $��2��6I   $��Center=�6V   $��Center=�6s   $��30��5�   !   $	��155��6-   �� 5�      -
   5        $ txtSalarioDelegado��5�   $   $��3��6@   $��2��6P   $��-��6]   $��Center=�6f   $��Center=�6�   $��Cascadia Code Light)�5�   $   
���   �$��14(�6X   �� 5�      -   5        $ btnContratarDelegado��5�   !   $��4��6?   $��2��6O   $��30��6\   $	��200��6h   $��CONTRATAR EMPLEADO��6t   $��Center=�5�   !   $��Center=�6>   ���#23282d
�������$��Cascadia Code SemiBold)�5�   !   $��16(�6E   $
 Hand��6S   ��5�      �� 5�   "   " ��6(   .�5�   &   ��  � ��	 �#23282d6C   �� 5�   &    � ��
	 ���� 6D   �� 5�   &    � ��
	 �����6C   �� 5�   &    � ��$	 0q�6H   �� 5�   &    � ��	 5�   *   �� 5�   .   "
 ��6>   .R�5�   2   �� #��z��69   #��z��5�   9   #��z��5�   9   ._�5�   6   �� $��Center=�6G   $��Center=�6d   5�   3   5�   /   5�   '   5�   #   5�      �� 5�      -   5        $ btnDespedirDelegado��5�   !   $��4��6>   $��2��6N   $��30��6[   $	��200��6g   $��DESPEDIR EMPLEADO��6s   $��Center=�5�   !   $��Center=�6>   ���#941f13
�������$��Cascadia Code SemiBold)�5�   !   $��16(�6E   $
 Hand��6S   $y�Hidden=�6�   ��5�      �� 5�   "   " ��6(   .�5�   &   ��  � ��	 �#941f136C   �� 5�   &    � ��
	 ���� 6D   �� 5�   &    � ��
	 �����6C   �� 5�   &    � ��$	 0q�6H   �� 5�   &    � ��	 5�   *   �� 5�   .   "
 ��6>   .R�5�   2   �� #��z��69   #��z��5�   9   #��z��5�   9   ._�5�   6   �� $��Center=�6G   $��Center=�6d   5�   3   5�   /   5�   '   5�   #   5�      �� 5�      $��0��6!   $��3��61   $��5��6>   
�������6R   �� 5�      -   5        $ txtNombreDirectorTecnico��5�   $   $��0��6F   $��3��6V   $��-��6c   $��Center=�6l   $��Center=�6�   $��Cascadia Code Light)�5�   $   
���   �$��14(�6X   $
 Hand��6f   6t   �� 5�      $��1��6$   $��3��64   $��Director Técnico��6A   $��Center=�6Y   $��Center=�6v   $��Cascadia Code Light)�5�   $   
���   �$��14(�6X   �� 5�      -   5        $ canvasDirectorTecnico��5�   !   $��2��6@   $��3��6P   $��Center=�6]   $��Center=�6z   $��30��5�   !   $	��155��6-   �� 5�      -   5        $ txtSalarioDirectorTecnico��5�   $   $��3��6G   $��3��6W   $��-��6d   $��Center=�6m   $��Center=�6�   $��Cascadia Code Light)�5�   $   
���   �$��14(�6X   �� 5�      -   5        $! btnContratarDirectorTecnico��5�   !   $��4��6F   $��3��6V   $��30��6c   $	��200��6o   $��CONTRATAR EMPLEADO��6{   $��Center=�5�   !   $��Center=�6>   ���#23282d
�������$��Cascadia Code SemiBold)�5�   !   $��16(�6E   $
 Hand��6S   ��5�      �� 5�   "   " ��6(   .�5�   &   ��  � ��	 �#23282d6C   �� 5�   &    � ��
	 ���� 6D   �� 5�   &    � ��
	 �����6C   �� 5�   &    � ��$	 0q�6H   �� 5�   &    � ��	 5�   *   �� 5�   .   "
 ��6>   .R�5�   2   �� #��z��69   #��z��5�   9   #��z��5�   9   ._�5�   6   �� $��Center=�6G   $��Center=�6d   5�   3   5�   /   5�   '   5�   #   5�      �� 5�      -   5        $  btnDespedirDirectorTecnico��5�   !   $��4��6E   $��3��6U   $��30��6b   $	��200��6n   $��DESPEDIR EMPLEADO��6z   $��Center=�5�   !   $��Center=�6>   ���#941f13
�������$��Cascadia Code SemiBold)�5�   !   $��16(�6E   $
 Hand��6S   $y�Hidden=�6�   ��5�      �� 5�   "   " ��6(   .�5�   &   ��  � ��	 �#941f136C   �� 5�   &    � ��
	 ���� 6D   �� 5�   &    � ��
	 �����6C   �� 5�   &    � ��$	 0q�6H   �� 5�   &    � ��	 5�   *   �� 5�   .   "
 ��6>   .R�5�   2   �� #��z��69   #��z��5�   9   #��z��5�   9   ._�5�   6   �� $��Center=�6G   $��Center=�6d   5�   3   5�   /   5�   '   5�   #   5�      �� 5�      $��0��6!   $��4��61   $��5��6>   
�������6R   �� 5�      -   5        $ txtNombrePreparadorFisico��5�   $   $��0��6G   $��4��6W   $��-��6d   $��Center=�6m   $��Center=�6�   $��Cascadia Code Light)�5�   $   
���   �$��14(�6X   $
 Hand��6f   6t   �� 5�      $��1��6$   $��4��64   $��Preparador Físico��6A   $��Center=�6Z   $��Center=�6w   $��Cascadia Code Light)�5�   $   
���   �$��14(�6X   �� 5�      -   5        $ canvasPreparadorFisico��5�   !   $��2��6A   $��4��6Q   $��Center=�6^   $��Center=�6{   $��30��5�   !   $	��155��6-   �� 5      -   5        $  txtSalarioPreparadorFisico��5   $   $��3��6H   $��4��6X   $��-��6e   $��Center=�6n   $��Center=�6�   $��Cascadia Code Light)�5  $   
���   �$��14(�6X   �� 5     -   5        $" btnContratarPreparadorFisico��5  !   $��4��6G   $��4��6W   $��30��6d   $	��200��6p   $��CONTRATAR EMPLEADO��6|   $��Center=�5  !   $��Center=�6>   ���#23282d
�������$��Cascadia Code SemiBold)�5  !   $��16(�6E   $
 Hand��6S   ��5     �� 5  "   " ��6(   .�5  &   ��  � ��	 �#23282d6C   �� 5	  &    � ��
	 ���� 6D   �� 5
  &    � ��
	 �����6C   �� 5  &    � ��$	 0q�6H   �� 5  &    � ��	 5  *   �� 5  .   "
 ��6>   .R�5  2   �� #��z��69   #��z��5  9   #��z��5  9   ._�5  6   �� $��Center=�6G   $��Center=�6d   5  3   5  /   5  '   5  #   5     �� 5     -   5        $! btnDespedirPreparadorFisico��5  !   $��4��6F   $��4��6V   $��30��6c   $	��200��6o   $��DESPEDIR EMPLEADO��6{   $��Center=�5  !   $��Center=�6>   ���#941f13
�������$��Cascadia Code SemiBold)�5  !   $��16(�6E   $
 Hand��6S   $y�Hidden=�6a   ��5     �� 5  "   " ��6(   .�5!  &   ��  � ��	 �#941f136C   �� 5"  &    � ��
	 ���� 6D   �� 5#  &    � ��
	 �����6C   �� 5$  &    � ��$	 0q�6H   �� 5%  &    � ��	 5&  *   �� 5'  .   "
 ��6>   .R�5(  2   �� #��z��69   #��z��5)  9   #��z��5*  9   ._�5+  6   �� $��Center=�6G   $��Center=�6d   5,  3   5-  /   5/  '   50  #   52     �� 55     $��0��6!   $��5��61   $��5��6>   
�������6R   �� 56     -   5        $ txtNombrePsicologo��56  $   $��0��6@   $��5��6P   $��-��6]   $��Center=�6f   $��Center=�6�   $��Cascadia Code Light)�57  $   
���   �$��14(�6X   $
 Hand��6f   6t   �� 58     $��1��6$   $��5��64   $��
Psicólogo��6A   $��Center=�6R   $��Center=�6o   $��Cascadia Code Light)�59  $   
���   �$��14(�6X   �� 5:     -   5        $ canvasPsicologo��5:  !   $��2��6:   $��5��6J   $��Center=�6W   $��Center=�6t   $��30��5;  !   $	��155��6-   �� 5<     -   5        $ txtSalarioPsicologo��5<  $   $��3��6A   $��5��6Q   $��-��6^   $��Center=�6g   $��Center=�6�   $��Cascadia Code Light)�5=  $   
���   �$��14(�6X   �� 5>     -   5        $ btnContratarPsicologo��5>  !   $��4��6@   $��5��6P   $��30��6]   $	��200��6i   $��CONTRATAR EMPLEADO��6u   $��Center=�5?  !   $��Center=�6>   ���#23282d
�������$��Cascadia Code SemiBold)�5@  !   $��16(�6E   $
 Hand��6S   ��5A     �� 5B  "   " ��6(   .�5D  &   ��  � ��	 �#23282d6C   �� 5E  &    � ��
	 ���� 6D   �� 5F  &    � ��
	 �����6C   �� 5G  &    � ��$	 0q�6H   �� 5H  &    � ��	 5I  *   �� 5J  .   "
 ��6>   .R�5K  2   �� #��z��69   #��z��5L  9   #��z��5M  9   ._�5N  6   �� $��Center=�6G   $��Center=�6d   5O  3   5P  /   5R  '   5S  #   5U     �� 5W     -   5        $ btnDespedirPsicologo��5W  !   $��4��6?   $��5��6O   $��30��6\   $	��200��6h   $��DESPEDIR EMPLEADO��6t   $��Center=�5X  !   $��Center=�6>   ���#941f13
�������$��Cascadia Code SemiBold)�5Y  !   $��16(�6E   $
 Hand��6S   $y�Hidden=�6�   ��5Z     �� 5[  "   " ��6(   .�5]  &   ��  � ��	 �#941f136C   �� 5^  &    � ��
	 ���� 6D   �� 5_  &    � ��
	 �����6C   �� 5`  &    � ��$	 0q�6H   �� 5a  &    � ��	 5b  *   �� 5c  .   "
 ��6>   .R�5d  2   �� #��z��69   #��z��5e  9   #��z��5f  9   ._�5g  6   �� $��Center=�6G   $��Center=�6d   5h  3   5i  /   5k  '   5l  #   5n     �� 5q     $��0��6!   $��6��61   $��5��6>   
�������6R   �� 5r     -   5        $ txtNombreFinanciero��5r  $   $��0��6A   $��6��6Q   $��-��6^   $��Center=�6g   $��Center=�6�   $��Cascadia Code Light)�5s  $   
���   �$��14(�6X   $
 Hand��6f   6t   �� 5t     $��1��6$   $��6��64   $��
Financiero��6A   $��Center=�6S   $��Center=�6p   $��Cascadia Code Light)�5u  $   
���   �$��14(�6X   �� 5v     -   5        $ canvasFinanciero��5v  !   $��2��6;   $��6��6K   $��Center=�6X   $��Center=�6u   $��30��5w  !   $	��155��6-   �� 5x     -   5        $ txtSalarioFinanciero��5x  $   $��3��6B   $��6��6R   $��-��6_   $��Center=�6h   $��Center=�6�   $��Cascadia Code Light)�5y  $   
���   �$��14(�6X   �� 5z     -   5        $ btnContratarFinanciero��5z  !   $��4��6A   $��6��6Q   $��30��6^   $	��200��6j   $��CONTRATAR EMPLEADO��6v   $��Center=�5{  !   $��Center=�6>   ���#23282d
�������$��Cascadia Code SemiBold)�5|  !   $��16(�6E   $
 Hand��6S   ��5}     �� 5~  "   " ��6(   .�5�  &   ��  � ��	 �#23282d6C   �� 5�  &    � ��
	 ���� 6D   �� 5�  &    � ��
	 �����6C   �� 5�  &    � ��$	 0q�6H   �� 5�  &    � ��	 5�  *   �� 5�  .   "
 ��6>   .R�5�  2   �� #��z��69   #��z��5�  9   #��z��5�  9   ._�5�  6   �� $��Center=�6G   $��Center=�6d   5�  3   5�  /   5�  '   5�  #   5�     �� 5�     -    5        $ btnDespedirFinanciero��5�  !   $��4��6@   $��6��6P   $��30��6]   $	��200��6i   $��DESPEDIR EMPLEADO��6u   $��Center=�5�  !   $��Center=�6>   ���#941f13
�������$��Cascadia Code SemiBold)�5�  !   $��16(�6E   $
 Hand��6S   $y�Hidden=�6a   ��5�     �� 5�  "   " ��6(   .�5�  &   ��  � ��	 �#941f136C   �� 5�  &    � ��
	 ���� 6D   �� 5�  &    � ��
	 �����6C   �� 5�  &    � ��$	 0q�6H   �� 5�  &    � ��	 5�  *   �� 5�  .   "
 ��6>   .R�5�  2   �� #��z��69   #��z��5�  9   #��z��5�  9   ._�5�  6   �� $��Center=�6G   $��Center=�6d   5�  3   5�  /   5�  '   5�  #   5�     �� 5�     $��0��6!   $��7��61   $��5��6>   
�������6R   �� 5�     -!   5        $ txtNombreEquipoMedico��5�  $   $��0��6C   $��7��6S   $��-��6`   $��Center=�6i   $��Center=�6�   $��Cascadia Code Light)�5�  $   
���   �$��14(�6X   $
 Hand��6f   6t   �� 5�     $��1��6$   $��7��64   $��Equipo Médico��6A   $��Center=�6V   $��Center=�6s   $��Cascadia Code Light)�5�  $   
���   �$��14(�6X   �� 5�     -"   5        $ canvasEquipoMedico��5�  !   $��2��6=   $��7��6M   $��Center=�6Z   $��Center=�6w   $��30��5�  !   $	��155��6-   �� 5�     -#   5        $ txtSalarioEquipoMedico��5�  $   $��3��6D   $��7��6T   $��-��6a   $��Center=�6j   $��Center=�6�   $��Cascadia Code Light)�5�  $   
���   �$��14(�6X   �� 5�     -$   5        $ btnContratarEquipoMedico��5�  !   $��4��6C   $��7��6S   $��30��6`   $	��200��6l   $��CONTRATAR EMPLEADO��6x   $��Center=�5�  !   $��Center=�6>   ���#23282d
�������$��Cascadia Code SemiBold)�5�  !   $��16(�6E   $
 Hand��6S   ��5�     �� 5�  "   " ��6(   .�5�  &   ��  � ��	 �#23282d6C   �� 5�  &    � ��
	 ���� 6D   �� 5�  &    � ��
	 �����6C   �� 5�  &    � ��$	 0q�6H   �� 5�  &    � ��	 5�  *   �� 5�  .   "
 ��6>   .R�5�  2   �� #��z��69   #��z��5�  9   #��z��5�  9   ._�5�  6   �� $��Center=�6G   $��Center=�6d   5�  3   5�  /   5�  '   5�  #   5�     �� 5�     -%   5        $ btnDespedirEquipoMedico��5�  !   $��4��6B   $��7��6R   $��30��6_   $	��200��6k   $��DESPEDIR EMPLEADO��6w   $��Center=�5�  !   $��Center=�6>   ���#941f13
�������$��Cascadia Code SemiBold)�5�  !   $��16(�6E   $
 Hand��6S   $y�Hidden=�6a   ��5�     �� 5�  "   " ��6(   .�5�  &   ��  � ��	 �#941f136C   �� 5�  &    � ��
	 ���� 6D   �� 5�  &    � ��
	 �����6C   �� 5�  &    � ��$	 0q�6H   �� 5�  &    � ��	 5�  *   �� 5�  .   "
 ��6>   .R�5�  2   �� #��z��69   #��z��5�  9   #��z��5�  9   ._�5�  6   �� $��Center=�6G   $��Center=�6d   5�  3   5�  /   5�  '   5�  #   5�     �� 5�     $��0��6!   $��8��61   $��5��6>   
�������6R   �� 5�     -&   5        $  txtNombreEncargadoPabellon��5�  $   $��0��6H   $��8��6X   $��-��6e   $��Center=�6n   $��Center=�6�   $��Cascadia Code Light)�5�  $   
���   �$��14(�6X   $
 Hand��6f   6t   �� 5�     $��1��6$   $��8��64   $��Encargado Campo��6A   $��Center=�6X   $��Center=�6u   $��Cascadia Code Light)�5�  $   
���   �$��14(�6X   �� 5�     -'   5        $ canvasEncargadoPabellon��5�  !   $��2��6B   $��8��6R   $��Center=�6_   $��Center=�6|   $��30��5�  !   $	��155��6-   �� 5�     -(   5        $! txtSalarioEncargadoPabellon��5�  $   $��3��6I   $��8��6Y   $��-��6f   $��Center=�6o   $��Center=�6�   $��Cascadia Code Light)�5�  $   
���   �$��14(�6X   �� 5�     -)   5        $# btnContratarEncargadoPabellon��5�  !   $��4��6H   $��8��6X   $��30��6e   $	��200��6q   $��CONTRATAR EMPLEADO��6}   $��Center=�5�  !   $��Center=�6>   ���#23282d
�������$��Cascadia Code SemiBold)�5�  !   $��16(�6E   $
 Hand��6S   ��5�     �� 5�  "   " ��6(   .�5�  &   ��  � ��	 �#23282d6C   �� 5�  &    � ��
	 ���� 6D   �� 5�  &    � ��
	 �����6C   �� 5�  &    � ��$	 0q�6H   �� 5�  &    � ��	 5�  *   �� 5�  .   "
 ��6>   .R�5�  2   �� #��z��69   #��z��5   9   #��z��5  9   ._�5  6   �� $��Center=�6G   $��Center=�6d   5  3   5  /   5  '   5  #   5	     �� 5     -*   5        $" btnDespedirEncargadoPabellon��5  !   $��4��6G   $��8��6W   $��30��6d   $	��200��6p   $��DESPEDIR EMPLEADO��6|   $��Center=�5  !   $��Center=�6>   ���#941f13
�������$��Cascadia Code SemiBold)�5  !   $��16(�6E   $
 Hand��6S   $y�Hidden=�6a   ��5     �� 5  "   " ��6(   .�5  &   ��  � ��	 �#941f136C   �� 5  &    � ��
	 ���� 6D   �� 5  &    � ��
	 �����6C   �� 5  &    � ��$	 0q�6H   �� 5  &    � ��	 5  *   �� 5  .   "
 ��6>   .R�5  2   �� #��z��69   #��z��5  9   #��z��5  9   ._�5  6   �� $��Center=�6G   $��Center=�6d   5  3   5  /   5  '   5   #   5"     5$     5%     5&     5'     �� 5*  
   -+   5        $ bordeEmpleadosDisponibles��5*     
�������$��1q�6L   ���#1d6a7d$��15q�6v   $��0��5+     $��1��6!   ._�5,     � 
�������  5-     �� 5.     $��* �6'    50     �� 51     $��30 �6$   �� 52     $��* �6$   .+�55     �� $��0��6   $��0��6)   ���#1d6a7d66   �� 56     $��0��6   $��0��6,   $��EMPLEADOS DISPONIBLES��69   $��Center=�6V   $��Center=�6s   $��Cascadia Code SemiBold)�57     
�������$��20(�6X   �� 59     $��0��6   $��1��6)   
�������$��1q�6N   
�������$��10q�6z   ._�5:     � 
�������  5;     �� 5<     $��* �6/   �� 5=     $��* �6/   �� 5>     $��* �6/   �� 5?     $��* �6/   �� 5@     $��* �6/    5B     �� 5C     $��30 �6,   �� 5D     $��* �6,   �� 5E     $��* �6,   �� 5F     $��* �6,   �� 5G     $��* �6,   �� 5H     $��* �6,   .+�5L     �� $��0��6!   $��0��61   $��5��6>   ���#1d6a7d6R   �� 5M     $��0��6$   $��0��64   $��EMPLEADO��6A   $��Center=�6Q   $��Center=�6n   $��Cascadia Code SemiBold)�5N  $   
�������$��16(�6`   �� 5O     $��1��6$   $��0��64   $��PUESTO��6A   $��Center=�6O   $��Center=�6l   $��Cascadia Code SemiBold)�5P  $   
�������$��16(�6`   �� 5Q     $��2��6$   $��0��64   $��
CATEGORÍA��6A   $��Center=�6R   $��Center=�6o   $��Cascadia Code SemiBold)�5R  $   
�������$��16(�6`   �� 5S     $��3��6$   $��0��64   $��SALARIO��6A   $��Center=�6P   $��Center=�6m   $��Cascadia Code SemiBold)�5T  $   
�������$��16(�6`   �� 5W     $��0��6!   $��1��61   $��5��6>   
�������6R   �� 5X     -,   5        $" txtNombreEmpleadoDisponible1��5X  $   $��0��6J   $��1��6Z   $��-��6g   $��Center=�6p   $��Center=�6�   $��Cascadia Code Light)�5Y  $   
���   �$��14(�6X   �� 5Z     --   5        $" txtPuestoEmpleadoDisponible1��5Z  $   $��1��6J   $��1��6Z   $��-��6g   $��Center=�6p   $��Center=�6�   $��Cascadia Code Light)�5[  $   
���   �$��14(�6X   �� 5\     -.   5        $ canvasEmpleadoDisponible1��5\  !   $��2��6D   $��1��6T   $��Center=�6a   $��Center=�6~   $��30��5]  !   $	��155��6-   �� 5^     -/   5        $# txtSalarioEmpleadoDisponible1��5^  $   $��3��6K   $��1��6[   $��-��6h   $��Center=�6q   $��Center=�6�   $��Cascadia Code Light)�5_  $   
���   �$��14(�6X   �� 5`     -0   5        $ 
btnFirmar1��5`  !   $��4��65   $��1��6E   $��30��6R   $	��200��6^   $��FIRMAR��6j   $��Center=�5a  !   $��Center=�6>   ���#23282d
�������$��Cascadia Code SemiBold)�5b  !   $��16(�6E   $
 Hand��6S   ��5c     �� 5d  "   " ��6(   .�5f  &   ��  � ��	 �#23282d6C   �� 5g  &    � ��
	 ���� 6D   �� 5h  &    � ��
	 �����6C   �� 5i  &    � ��$	 0q�6H   �� 5j  &    � ��	 5k  *   �� 5l  .   "
 ��6>   .R�5m  2   �� #��z��69   #��z��5n  9   #��z��5o  9   ._�5p  6   �� $��Center=�6G   $��Center=�6d   5q  3   5r  /   5t  '   5u  #   5w     �� 5z     $��0��6!   $��2��61   $��5��6>   
�������6R   �� 5{     -1   5        $" txtNombreEmpleadoDisponible2��5{  $   $��0��6J   $��2��6Z   $��-��6g   $��Center=�6p   $��Center=�6�   $��Cascadia Code Light)�5|  #   
���   �$��14(�6W   �� 5}     -2   5        $" txtPuestoEmpleadoDisponible2��5}  $   $��1��6J   $��2��6Z   $��-��6g   $��Center=�6p   $��Center=�6�   $��Cascadia Code Light)�5~  $   
���   �$��14(�6X   �� 5     -3   5        $ canvasEmpleadoDisponible2��5  !   $��2��6D   $��2��6T   $��Center=�6a   $��Center=�6~   $��30��5�  !   $	��155��6-   �� 5�     -4   5        $# txtSalarioEmpleadoDisponible2��5�  $   $��3��6K   $��2��6[   $��-��6h   $��Center=�6q   $��Center=�6�   $��Cascadia Code Light)�5�  $   
���   �$��14(�6X   �� 5�     -5   5        $ 
btnFirmar2��5�  !   $��4��65   $��2��6E   $��30��6R   $	��200��6^   $��FIRMAR��6j   $��Center=�5�  !   $��Center=�6>   ���#23282d
�������$��Cascadia Code SemiBold)�5�  !   $��16(�6E   $
 Hand��6S   ��5�     �� 5�  "   " ��6(   .�5�  &   ��  � ��	 �#23282d6C   �� 5�  &    � ��
	 ���� 6D   �� 5�  &    � ��
	 �����6C   �� 5�  &    � ��$	 0q�6H   �� 5�  &    � ��	 5�  *   �� 5�  .   "
 ��6>   .R�5�  2   �� #��z��69   #��z��5�  9   #��z��5�  9   ._�5�  6   �� $��Center=�6G   $��Center=�6d   5�  3   5�  /   5�  '   5�  #   5�     �� 5�     $��0��6!   $��3��61   $��5��6>   
�������6R   �� 5�     -6   5        $" txtNombreEmpleadoDisponible3��5�  $   $��0��6J   $��3��6Z   $��-��6g   $��Center=�6p   $��Center=�6�   $��Cascadia Code Light)�5�  $   
���   �$��14(�6X   �� 5�     -7   5        $" txtPuestoEmpleadoDisponible3��5�  $   $��1��6J   $��3��6Z   $��-��6g   $��Center=�6p   $��Center=�6�   $��Cascadia Code Light)�5�  $   
���   �$��14(�6X   �� 5�     -8   5        $ canvasEmpleadoDisponible3��5�  !   $��2��6D   $��3��6T   $��Center=�6a   $��Center=�6~   $��30��5�  !   $	��155��6-   �� 5�     -9   5        $# txtSalarioEmpleadoDisponible3��5�  $   $��3��6K   $��3��6[   $��-��6h   $��Center=�6q   $��Center=�6�   $��Cascadia Code Light)�5�  $   
���   �$��14(�6X   �� 5�     -:   5        $ 
btnFirmar3��5�  !   $��4��65   $��3��6E   $��30��6R   $	��200��6^   $��FIRMAR��6j   $��Center=�5�  !   $��Center=�6>   ���#23282d
�������$��Cascadia Code SemiBold)�5�  !   $��16(�6E   $
 Hand��6S   ��5�     �� 5�  "   " ��6(   .�5�  &   ��  � ��	 �#23282d6C   �� 5�  &    � ��
	 ���� 6D   �� 5�  &    � ��
	 �����6C   �� 5�  &    � ��$	 0q�6H   �� 5�  &    � ��	 5�  *   �� 5�  .   "
 ��6>   .R�5�  2   �� #��z��69   #��z��5�  9   #��z��5�  9   ._�5�  6   �� $��Center=�6G   $��Center=�6d   5�  3   5�  /   5�  '   5�  #   5�     �� 5�     $��0��6!   $��4��61   $��5��6>   
�������6R   �� 5�     -;   5        $" txtNombreEmpleadoDisponible4��5�  $   $��0��6J   $��4��6Z   $��-��6g   $��Center=�6p   $��Center=�6�   $��Cascadia Code Light)�5�  $   
���   �$��14(�6X   �� 5�     -<   5        $" txtPuestoEmpleadoDisponible4��5�  $   $��1��6J   $��4��6Z   $��-��6g   $��Center=�6p   $��Center=�6�   $��Cascadia Code Light)�5�  $   
���   �$��14(�6X   �� 5�     -=   5        $ canvasEmpleadoDisponible4��5�  !   $��2��6D   $��4��6T   $��Center=�6a   $��Center=�6~   $��30��5�  !   $	��155��6-   �� 5�     ->   5        $# txtSalarioEmpleadoDisponible4��5�  $   $��3��6K   $��4��6[   $��-��6h   $��Center=�6q   $��Center=�6�   $��Cascadia Code Light)�5�  $   
���   �$��14(�6X   �� 5�     -?   5        $ 
btnFirmar4��5�  !   $��4��65   $��4��6E   $��30��6R   $	��200��6^   $��FIRMAR��6j   $��Center=�5�  !   $��Center=�6>   ���#23282d
�������$��Cascadia Code SemiBold)�5�  !   $��16(�6E   $
 Hand��6S   ��5�     �� 5�  "   " ��6(   .�5�  &   ��  � ��	 �#23282d6C   �� 5�  &    � ��
	 ���� 6D   �� 5�  &    � ��
	 �����6C   �� 5�  &    � ��$	 0q�6H   �� 5�  &    � ��	 5�  *   �� 5�  .   "
 ��6>   .R�5�  2   �� #��z��69   #��z��5�  9   #��z��5�  9   ._�5�  6   �� $��Center=�6G   $��Center=�6d   5�  3   5�  /   5�  '   5�  #   5�     �� 5�     $��0��6!   $��5��61   $��5��6>   
�������6R   �� 5�     -@   5        $" txtNombreEmpleadoDisponible5��5�  $   $��0��6J   $��5��6Z   $��-��6g   $��Center=�6p   $��Center=�6�   $��Cascadia Code Light)�5�  #   
���   �$��14(�6W   �� 5�     -A   5        $" txtPuestoEmpleadoDisponible5��5�  $   $��1��6J   $��5��6Z   $��-��6g   $��Center=�6p   $��Center=�6�   $��Cascadia Code Light)�5�  $   
���   �$��14(�6X   �� 5�     -B   5        $ canvasEmpleadoDisponible5��5�  !   $��2��6D   $��5��6T   $��Center=�6a   $��Center=�6~   $��30��5�  !   $	��155��6-   �� 5�     -C   5        $# txtSalarioEmpleadoDisponible5��5�  $   $��3��6K   $��5��6[   $��-��6h   $��Center=�6q   $��Center=�6�   $��Cascadia Code Light)�5�  $   
���   �$��14(�6X   �� 5�     -D   5        $ 
btnFirmar5��5�  !   $��4��65   $��5��6E   $��30��6R   $	��200��6^   $��FIRMAR��6j   $��Center=�5�  !   $��Center=�6>   ���#23282d
�������$��Cascadia Code SemiBold)�5�  !   $��16(�6E   $
 Hand��6S   ��5�     �� 5�  "   " ��6(   .�5�  &   ��  � ��	 �#23282d6C   �� 5�  &    � ��
	 ���� 6D   �� 5�  &    � ��
	 �����6C   �� 5�  &    � ��$	 0q�6H   �� 5�  &    � ��	 5�  *   �� 5�  .   "
 ��6>   .R�5�  2   �� #��z��69   #��z��5�  9   #��z��5�  9   ._�5�  6   �� $��Center=�6G   $��Center=�6d   5�  3   5�  /   5   '   5  #   5     5     5     5     5     �� 5
  
   -E   5        $ bordeFichaEmpleado��5
     
�������$��1q�6E   ���#1d6a7d$��15q�6o   $��1��5     $��0��6!   $��2��6.   ._�5     � 
�������  5     �� 5     $��* �6'    5     �� 5     $��30 �6$   �� 5     $��* �6$   .+�5     �� $��0��6   $��0��6)   ���#1d6a7d66   �� 5     $��0��6   $��0��6,   $��FICHA��69   $��Center=�6F   $��Center=�6c   $��Cascadia Code SemiBold)�5     
�������$��20(�6X   �� 5     $��0��6   $��1��6)   
�������$��1q�6N   
�������$��10q�6z   ._�5     � 
�������  5     �� 5     $��* �6/    5     �� 5     $��30 �6,   �� 5      $��50 �6,   �� 5!     $��50 �6,   �� 5"     $��50 �6,   �� 5#     $��30 �6,   �� 5$     $��* �6,   .+�5'     �� $��0��6!   $��0��61   ���#23282d6>   �� 5(     -F   5        $ txtFichaEmpleado��5(  $   $��0��6?   $��0��6O   $��FICHA EMPLEADO��6\   $��Center=�6r   $��Center=�6�   $��Cascadia Code SemiBold)�5)  $   
�������$��16(�6`   �� 5+     $��0��6!   $��1��61   
�������6>   �� 5,     -G   5        $ txtFichaNombreEmpleado��5,  $   $��0��6D   $��1��6T   $��-��6a   $��Center=�6j   $��Center=�6�   $��Cascadia Code SemiBold)�5-  $   ���#23282d$��16(�6]   �� 5/     $��0��6!   $��2��61   
�������6>   �� 50     -H   5        $ canvasFichaEmpleado��50  !   $��0��6>   $��2��6N   $��Center=�6[   $��Center=�6x   $��30��51  !   $	��155��6-   �� 53     $��0��6!   $��3��61   
�������6>   �� 54     -I   5        $ txtFichaSalarioEmpleado��54  $   $��0��6E   $��3��6U   $��-��6b   $��Center=�6k   $��Center=�6�   $��Cascadia Code SemiBold)�55  $   ���#23282d$��16(�6]   �� 57     $��0��6!   $��4��61   ���#23282d6>   �� 58     $��0��6$   $��4��64   $
��INFO��6A   $��Center=�6M   $��Center=�6j   $��Cascadia Code SemiBold)�59  $   
�������$��16(�6`   �� 5;     $��0��6!   $��5��61   
�������6>   �� 5<     -J   5        $ txtFichaInfo��5<  $   $��5��6:   $��-��6G   $��Center=�6P   $	��Top=�6m   $��0,20,0,0q�6�   $��Cascadia Code Light)�5=  $   ���#23282d$��16(�6Z   $	��426��6h   $
��Wrap=�6t    �� 
LineHeight$ 22��6�   5>     5?     5@     5A     5B     5C     