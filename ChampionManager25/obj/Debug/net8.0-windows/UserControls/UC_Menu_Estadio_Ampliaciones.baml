   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      A    ;ChampionManager25.UserControls.UC_Menu_Estadio_Ampliaciones   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      -   5          >�Name$  UCAmpliaciones��5      .��5      � ��5	   
   �� 5
       �� ImageSource$C =pack://application:,,,/Recursos/img/backgroundEstadio_img.png��6    j� Stretch$ UniformToFill=�6e    j� TileMode$
 None=�6}    � ColumnDefinitions 5   
   �� 5      $	��745 �6   �� 5      $
��1100 �6    � RowDefinitions 5   
   �� 5      $	��930 �6   .+�5   
   �� 
�������$��5q�5      ���#1d6a7d$��
37,100,5,0q�5      $��0��5      $��0��6!   $	��Top=�6.   $	��764��6F   ._�5      �  5      �� 5      $	��700 �6'    5      �� 5      $��70 �6$   �� 5       $��32 �6$   �� 5!      $��* �6$   �� 5"      $	��350 �6$   .+�5%      �� $��0��6   $��0��6)   ���#23282d._�5&      �� -   5         ��Name$ txtNombreEstadio��5&       $��2��6:   $
��Left=�6J   $��Nombre Pabellon��6e   $��Center=�6|   $��Cascadia Code SemiBold)�5'      $��20(�6<   
�������$��20,0,0,0q�6b   5(      �� 5*      -   5         ��Name$ borderBarraTitulo��5*      $��0��64   $��1��6D   ���#1d6a7d._�5+      �� -   5        $ 	txtTitulo��5+       $��2��63   $
��Left=�6C   $��AUMENTAR CAPACIDAD EN...��6^   $��Center=�6~   $��Cascadia Code SemiBold)�5,      $��18(�6<   
�������$��20,0,0,0q�6b   5-      �� 5/      $��0��6   $��2��6)   ._�50      �  51      �� 52      $	��694 �6/    54      �� 55      $��* �6,   �� 56      $��* �6,   �� 57      $��* �6,   �� 58      $��50 �6,   .+�5;      �� $��0��6!   $��0��61   
���   �$��1q�6R   $��5q�6f   ._�5<      �  5=   "   �� 5>   &   $��72 �67   �� 5?   &   $	��484 �67   �� 5@   &   $	��128 �67    5B   "   �� 5C   &   $��72 �64   .+�5F   "   �� -   5         ��Name$ imgSeleccionObra1��5F   (   $��0��6C   $��0��6S   $��Center=�6`   $��Center=�6{   	 � Cursor$
	 Hand��6�   $��64��5G   (   $��64��63   $��Uniform=�6?   $1��+/Recursos/img/icons/noSeleccionado_icon.png��6Q   6�   �� 5I   "   $��1��6)   $��0��69   $	��484��5J   )   $��72��65   $��Center=�6A   ._�5K   &   �  5L   *   �� 5M   .   $��* �6?    5O   *   �� 5P   .   $��* �6<   �� 5Q   .   $��* �6<   �� 5R   .   $��* �6<   .+�5T   *   �� -   5        $ txt1Remodelacion1��5T   4   $��0��6O   $��0��6_   $
��Left=�6l   $��50,0,0,0q�6�   $��500 asientos��5U   4   $��Center=�6H   
 �� TextAlignment$
 Center=�6c   $��Cascadia Code SemiBold)�5V   4   $��18(�6X   
���   �6f   �� 5W   *   -   5        $ txt2Remodelacion1��5W   4   $��0��6O   $��1��6_   $
��Left=�6l   $��50,0,0,0q�6�   $��5.000.000 €��5X   4   $��Center=�6G   $
 Center=�6b   $��Cascadia Code SemiBold)�5Y   4   $��18(�6X   
���  ��6f   �� 5Z   *   -   5        $ txt3Remodelacion1��5Z   4   $��0��6O   $��2��6_   $
��Left=�6l   $��50,0,0,0q�6�   $��
20 semanas��5[   4   $��Center=�6F   $
 Center=�6a   $��Cascadia Code SemiBold)�5\   4   $��18(�6X   ���	#FF0E2B826f   5]   '   5^   #   �� 5`   "   -	   5        $ imgExcavadora1��5`   (   $��2��6@   $��0��6P   $��Center=�6]   $��Center=�6x   $��64��5a   (   $��64��63   $��Uniform=�6?   $-��'/Recursos/img/icons/excavadora_icon.png��6Q   $y�Hidden=�6�   5b      5c      �� 5e      $��0��6!   $��1��61   
���   �$��1q�6R   $��5q�6f   ._�5f      �  5g   "   �� 5h   &   $��72 �67   �� 5i   &   $	��484 �67   �� 5j   &   $	��128 �67    5l   "   �� 5m   &   $��72 �64   .+�5p   "   �� -
   5        $ imgSeleccionObra2��5p   (   $��0��6C   $��0��6S   $��Center=�6`   $��Center=�6{   $
	 Hand��6�   $��64��5q   (   $��64��63   $��Uniform=�6?   $1��+/Recursos/img/icons/noSeleccionado_icon.png��6Q   6�   �� 5s   "   $��1��6)   $��0��69   $	��484��5t   )   $��72��65   $��Center=�6A   ._�5u   &   �  5v   *   �� 5w   .   $��* �6?    5y   *   �� 5z   .   $��* �6<   �� 5{   .   $��* �6<   �� 5|   .   $��* �6<   .+�5~   *   �� -   5        $ txt1Remodelacion2��5~   4   $��0��6O   $��0��6_   $
��Left=�6l   $��50,0,0,0q�6�   $��1000 asientos��5   4   $��Center=�6I   $
 Center=�6d   $��Cascadia Code SemiBold)�5�   4   $��18(�6X   
���   �6f   �� 5�   *   -   5        $ txt2Remodelacion2��5�   4   $��0��6O   $��1��6_   $
��Left=�6l   $��50,0,0,0q�6�   $��15.000.000 €��5�   4   $��Center=�6H   $
 Center=�6c   $��Cascadia Code SemiBold)�5�   4   $��18(�6X   
���  ��6f   �� 5�   *   -   5        $ txt3Remodelacion2��5�   4   $��0��6O   $��2��6_   $
��Left=�6l   $��50,0,0,0q�6�   $��
35 semanas��5�   4   $��Center=�6F   $
 Center=�6a   $��Cascadia Code SemiBold)�5�   4   $��18(�6X   ���	#FF0E2B826f   5�   '   5�   #   �� 5�   "   -   5        $ imgExcavadora2��5�   (   $��2��6@   $��0��6P   $��Center=�6]   $��Center=�6x   $��64��5�   (   $��64��63   $��Uniform=�6?   $-��'/Recursos/img/icons/excavadora_icon.png��6Q   $y�Hidden=�6�   5�      5�      �� 5�      $��0��6!   $��2��61   
���   �$��1q�6R   $��5q�6f   ._�5�      �  5�   "   �� 5�   &   $��72 �67   �� 5�   &   $	��484 �67   �� 5�   &   $	��128 �67    5�   "   �� 5�   &   $��72 �64   .+�5�   "   �� -   5        $ imgSeleccionObra3��5�   (   $��0��6C   $��0��6S   $��Center=�6`   $��Center=�6{   $
	 Hand��6�   $��64��5�   (   $��64��63   $��Uniform=�6?   $1��+/Recursos/img/icons/noSeleccionado_icon.png��6Q   6�   �� 5�   "   $��1��6)   $��0��69   $	��484��5�   )   $��72��65   $��Center=�6A   ._�5�   &   �  5�   *   �� 5�   .   $��* �6?    5�   *   �� 5�   .   $��* �6<   �� 5�   .   $��* �6<   �� 5�   .   $��* �6<   .+�5�   *   �� -   5        $ txt1Remodelacion3��5�   4   $��0��6O   $��0��6_   $
��Left=�6l   $��50,0,0,0q�6�   $��1500 asientos��5�   4   $��Center=�6I   $
 Center=�6d   $��Cascadia Code SemiBold)�5�   4   $��18(�6X   
���   �6f   �� 5�   *   -   5        $ txt2Remodelacion3��5�   4   $��0��6O   $��1��6_   $
��Left=�6l   $��50,0,0,0q�6�   $��30.000.000 €��5�   4   $��Center=�6H   $
 Center=�6c   $��Cascadia Code SemiBold)�5�   4   $��18(�6X   
���  ��6f   �� 5�   *   -   5        $ txt3Remodelacion3��5�   4   $��0��6O   $��2��6_   $
��Left=�6l   $��50,0,0,0q�6�   $��
50 semanas��5�   4   $��Center=�6F   $
 Center=�6a   $��Cascadia Code SemiBold)�5�   4   $��18(�6X   ���	#FF0E2B826f   5�   '   5�   #   �� 5�   "   -   5        $ imgExcavadora3��5�   (   $��2��6@   $��0��6P   $��Center=�6]   $��Center=�6x   $��64��5�   (   $��64��63   $��Uniform=�6?   $-��'/Recursos/img/icons/excavadora_icon.png��6Q   $y�Hidden=�6�   5�      5�      �� 5�      -   5         ��Name$ btnEmpezarObras��5�   !   $��3��6:   $��5,6,5,7q�6G   $��Empezar Obras��5�   !   $��Cascadia Code SemiBold)�5�   !   $��18(�6E   $
	 Hand��6S   $y�Hidden=�6a   ��5�      �� 5�   "    �� 
TargetType" ��6(   .�5�   &   ��  �� Property � �� �� Value �#8b00006C   �� 5�   &    � ��
 �����6C   �� 5�   &    � ��$ 24(�6A   �� 5�   &    �@J�SnapsToDevicePixels$
 True��6L   �� 5�   &    � �� 5�   *   �� 5�   .    �� 
TargetType" ��6>   .R�5�   2   ��  �� CornerRadius$ 4��69   #��z��6J   ._�5�   6   � .+�5�   :   �� $ imgBoton��6@   $(��"/Recursos/img/icons/obras_icon.png��6R   $��32��6~   $��32��6�   $��Center=�6�   $
��Left=�6�   $��Uniform=�6�   $��17,0,0,0q�6�   �� 5�   :    ��Name$ MyContentPresenter��6K   #��z��6g   $��Center=�6�   $��Center=�6�   $��0,0,0,0q�6�   �� 5�   :   $ 	imgBoton2��6@   $(��"/Recursos/img/icons/obras_icon.png��6S   $��32��6   $��32��6�   $��Center=�6�   $��Right=�6�   $��Uniform=�6�   $��0,0,17,0q�6�   5�   7   5�   3    �� Triggers 5�   2   P� 5�   6    P� Property �@J�IsMouseOver P� Value$
 True��6U   . �5�   :   ��  � �� �#a80b0b6W   �� 5�   :    � ��
 �����6W   5�   7   5�   /   5�   '   5�   #   5�      5�      5�      �� 5�      -   5        $ 
imgStadium��5�      $��3��6,   $	��Top=�69   $
��Left=�6Q   $	��700��5�      $	��350��6   $��UniformToFill=�6)   5�      5�      �� 5�   
   -   5        $ imgLogo��5�      $��1��6!   $
��Left=�61   $	��200��6L   $��132,322,0,0q�6Y   $	��Top=�6n   $	��200��6�   5�      5�      