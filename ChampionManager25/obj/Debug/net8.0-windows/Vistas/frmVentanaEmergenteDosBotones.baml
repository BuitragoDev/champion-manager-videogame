   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ChampionManager25.VistasChampionManager25.Vistas  5   	   <    6ChampionManager25.Vistas.frmVentanaEmergenteDosBotones   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:ChampionManager25.Vistas     5   	   -   5          �Name$  VentanaEmergente��5   	    � Title$# frmVentanaEmergenteDosBotones��5      $	��300��6@   $	��800��6M    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�6    � WindowStartupLocation$ CenterScreen=�62   .��5	      � ���	#FF23282D � ColumnDefinitions 5
   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� 
�������$��0��6)   $��0��69   $��5q�6F   ._�5      �  5      �� 5      $��* �6'    5      �� 5      $��40 �6$   �� 5      $��* �6$   �� 5      $��80 �6$   .+�5      �� ���#1d6a7d$��0��6.   $��0��6>   ._�5      �� -   5         ��Name$ txtTituloVentana��5       $�� ��6:   $��Center=�6B   $��Center=�6_   $��Cascadia Code SemiBold)�5       $��20(�6D   
������� �� TextAlignment$ Center=�6j   5      �� 5!      $��0��6   $��1��6)   ._�5"      �� -   5        $ 
txtMensaje��5"       $�� ��64   $��Center=�6<   $��Center=�6Y   $	��737��6t   $
��Wrap=�6�   $��Cascadia Code Light)�5#       $��18(�6A   ���	#FF23282D$ Center=�6f   5$      �� 5&      ���	#FF23282D$��0��60   $��2��6@   ._�5'      �  5(      �� 5)      $��* �6/   �� 5*      $��* �6/    5,      �� 5-      $��* �6,   .+�50      �� -   5        	 ��Name$	 
btnAceptar��50   !   $��0��65   $��0��6E   $��Center=�6R   $��Center=�6m   $��40��51   !   $	��250��6-   $��Cascadia Code SemiBold)�69   $��18(�6]   
 � Cursor$

 Hand��6k   ��52      �� 53   "    �� 
TargetType" ��6(   .�55   &   ��  �� Property � �� �� Value �#1d6a7d6C   �� 56   &    � ��
 �����6C   �� 57   &    � ��
 ���� 6D   �� 58   &    � ��$ 0q�6H   �� 59   &    �@�Cursor$
 Hand��6?   �� 5:   &    � �� 5;   *   �� 5<   .    �� 
TargetType" ��6>   .R�5=   2   �� #��z��69   #��z��5>   9   #��z��5?   9   ._�5@   6   ��  �� Orientation$ 
Horizontal=�6A   $��Center=�6Z   $��Center=�6w   .+�5B   :   �� $��ACEPTAR��6D   #��z��5C   D   $��Center=�5D   D   $��20,0,5,0q�5E   D   $��22(�6V   5F   7   5G   3   5H   /   5J   '    �� Triggers 5M   &   P� 5N   *    P� Property �@J�IsMouseOver P� Value$
 True��6I   . �5O   .   ��  � ��
 �����6L   �� 5P   .    � ��$ 1q�6P   5Q   +   5S   #   5U      �� 5W      -   5        $	 btnCancelar��5W   !   $��1��66   $��0��6F   $��Center=�6S   $��Center=�6n   $��40��5X   !   $	��250��6-   $��Cascadia Code SemiBold)�69   $��18(�6]   $

 Hand��6k   ��5Y      �� 5Z   "   " ��6(   .�5\   &   ��  � �� �#1d6a7d6C   �� 5]   &    � ��
 �����6C   �� 5^   &    � ��
 ���� 6D   �� 5_   &    � ��$ 0q�6H   �� 5`   &    �@�Cursor$
 Hand��6?   �� 5a   &    � �� 5b   *   �� 5c   .   " ��6>   .R�5d   2   �� #��z��69   #��z��5e   9   #��z��5f   9   ._�5g   6   �� $ 
Horizontal=�6A   $��Center=�6Z   $��Center=�6w   .+�5i   :   �� $��SALIR��6D   #��z��5j   D   $��Center=�5k   D   $��20,0,5,0q�5l   D   $��22(�6V   5m   7   5n   3   5o   /   5q   '    5t   &   P� 5u   *    �@J�IsMouseOver$
 True��6I   . �5v   .   ��  � ��
 �����6L   �� 5w   .    � ��$ 1q�6P   5x   +   5z   #   5|      5}      5~      5      5�      5�      5�      