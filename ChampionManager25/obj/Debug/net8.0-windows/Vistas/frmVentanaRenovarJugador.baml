   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ChampionManager25.VistasChampionManager25.Vistas  5   	   7    1ChampionManager25.Vistas.frmVentanaRenovarJugador   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:ChampionManager25.Vistas     5   	   -   5        $	��400��5      $
��1000��6+     � Title$#  frmVentanaEmergenteDosBotones��5       � 
ResizeMode$ NoResize=�6+    � WindowStyle$
 None=�6A    � WindowStartupLocation$ CenterScreen=�6T   .��5	      � ���	#FF23282D � ColumnDefinitions 5
   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� 
�������$��0��6)   $��0��69   $��5q�6F   ._�5      �  5      �� 5      $��* �6'    5      �� 5      $��40 �6$   �� 5      $��* �6$   �� 5      $��80 �6$   .+�5      �� ���#1d6a7d$��0��6.   $��0��6>   ._�5      �� -   5         ��Name$ txtTituloVentana��5       $�� ��6:   $��Center=�6B   $��Center=�6_   $��Cascadia Code SemiBold)�5      $��20(�6@   
������� �� TextAlignment$ Center=�6f   5      �� 5!      $��0��6   $��1��6)   ._�5"      �� -   5        $ 
txtMensaje��5"       $�� ��64   $��Center=�6<   $��Center=�6Y   $	��600��6t   $
��Wrap=�6�   $��Cascadia Code Light)�5#      $��18(�6=   ���	#FF23282D$ Center=�6b   5$      �� 5&      ���	#FF23282D$��0��60   $��2��6@   ._�5'      �  5(      �� 5)      $��* �6/   �� 5*      $��* �6/    5,      �� 5-      $��* �6,   .+�50      �� -   5         ��Name$ 
btnAceptar��50   !   $��0��65   $��0��6E   $��ACEPTAR��6R   $��Center=�6d   $��Center=�6   $��50��51   	   $	��250��6   $��Cascadia Code SemiBold)�6!   $��24(�6E   	 � Cursor$
	 Hand��6S   ��52      �� 53   "   
 �� 
TargetType"
 ��6(   .�54   &   ��  �� Property � �� �� Value �#1d6a7d6C   �� 55   &    � ��
 �����6C   �� 56   &    � ��$ 24(�6A   �� 57   &    �@J�SnapsToDevicePixels$
 True��6L   �� 58   &    � ��$ 0q�6H   �� 59   &    � �� 5:   *   �� 5;   .    �� 
TargetType" ��6>   .R�5<   2   ��  �� CornerRadius$ 8��69   #��z��6J   ._�5=   6   � .+�5>   :   ��  ��Name$ imgBoton��6@   $,��&/Recursos/img/icons/confirmar_icon.png��6R   $��32��5?   (   $��32��64   $��Center=�6?   $
��Left=�5@   (   $��Uniform=�6C   $��17,0,0,0q�6U   �� 5A   :    ��Name$ MyContentPresenter��6K   #��z��5B   3   $��Center=�5C   3   $��Center=�5D   3   $��35,0,0,0q�5E   3   5F   7   5G   3    �� Triggers 5H   2   P� 5I   6    P� Property �@J�IsMouseOver P� Value$
 True��6U   . �5J   :   ��  � �� �#22798e6W   �� 5K   :    � ��
 �����6W   5L   7   5N   /   5P   '   5Q   #   5S      �� 5U      -   5        $ btnCancelar��5U   !   $��1��66   $��0��6F   $��CANCELAR��6S   $��Center=�6f   $��Center=�6�   $��50��5V   	   $	��250��6   $��Cascadia Code SemiBold)�6!   $��24(�6E   $
	 Hand��6S   ��5W      �� 5X   "   "
 ��6(   .�5Y   &   ��  � �� �#1d6a7d6C   �� 5Z   &    � ��
 �����6C   �� 5[   &    � ��$ 24(�6A   �� 5\   &    �@J�SnapsToDevicePixels$
 True��6L   �� 5]   &    � ��$ 0q�6H   �� 5^   &    � �� 5_   *   �� 5`   .   " ��6>   .R�5a   2   �� $ 8��69   #��z��6J   ._�5b   6   � .+�5c   :   �� $ imgBoton��6@   $+��%/Recursos/img/icons/cancelar_icon.png��6R   $��32��5d   (   $��32��64   $��Center=�6?   $
��Left=�5e   (   $��Uniform=�6C   $��17,0,0,0q�6U   �� 5f   :   $ MyContentPresenter��6K   #��z��5g   3   $��Center=�5h   3   $��Center=�5i   3   $��35,0,0,0q�5j   3   5k   7   5l   3    5m   2   P� 5n   6    �@J�IsMouseOver$
 True��6U   . �5o   :   ��  � �� �#22798e6W   �� 5p   :    � ��
 �����6W   5q   7   5s   /   5u   '   5v   #   5x      5y      5z      5{      5|      5}      5~      