   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ChampionManager25.VistasChampionManager25.Vistas  5   	   0    *ChampionManager25.Vistas.frmPonerEnMercado   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:ChampionManager25.Vistas     5   	   -   5        $	��300��5      $	��800��6'     � Title$  frmPonerEnMercado��5   	    � 
ResizeMode$ NoResize=�6#    � WindowStyle$
 None=�69    � WindowStartupLocation$ CenterScreen=�6L   .��5	      � ���	#FF23282D � ColumnDefinitions 5
   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� 
�������$��0��6)   $��0��69   $��5q�6F   ._�5      �  5      �� 5      $��* �6'    5      �� 5      $��40 �6$   �� 5      $	��100 �6$   �� 5      $��* �6$   .+�5      �� ���#1d6a7d$��0��6.   $��0��6>   ._�5      �� -   5         ��Name$ txtTituloVentana��5       $�� ��6:   $��Center=�6B   $��Center=�6_   $��Cascadia Code SemiBold)�5       $��20(�6D   
������� �� TextAlignment$ Center=�6j   5      �� 5!      $��0��6   $��1��6)   $��10q�66   
�������._�5"      �� -   5        $ 
txtMensaje��5"       $�� ��64   $��Center=�6<   $��Center=�6Y   $	��614��6t   $
��Wrap=�6�   $��Cascadia Code Light)�5#       $��18(�6A   ���	#FF23282D$ Center=�6f   5$      �� 5&      $��0��6   $��2��6)   ._�5'      �  5(      �� 5)      $��* �6/    5+      �� 5,      $��* �6,   �� 5-      $��* �6,   .+�50      �� -   5         ��Name$ btnMercadoTransferibles��50   !   $��0��6B   $��0��6R   $��Center=�6_   $��Center=�6z   $��50��51   !   $	��350��6-   $��Cascadia Code SemiBold)�69   $��18(�6]   	 � Cursor$
	 Hand��6k   ��52      �� 53   "   
 �� 
TargetType"
 ��6(   .�55   &   ��  �� Property � �� �� Value �#1d6a7d6C   �� 56   &    � ��
 �����6C   �� 57   &    � ��
 ���� 6D   �� 58   &    � ��$ 0q�6H   �� 59   &    �@�Cursor$
 Hand��6?   �� 5:   &    � �� 5;   *   �� 5<   .    �� 
TargetType" ��6>   .R�5=   2   �� #��z��69   #��z��5>   9   #��z��5?   9    �� CornerRadius$ 10��5@   9   ._�5A   6   ��  �� Orientation$ 
Horizontal=�6A   $��Center=�6Z   $��Center=�6w   .+�5C   :   �� $��Mercado de Transferibles��6D   #��z��5D   D   $��Center=�5E   D   $��20,0,5,0q�5F   D   $��18(�6V   5G   7   5H   3   5I   /   5K   '    �� Triggers 5N   &   P� 5O   *    P� Property �@J�IsMouseOver P� Value$
 True��6I   . �5P   .   ��  � �� �#22798e6K   �� 5Q   .    � ��
 �����6K   5R   +   5T   #   5V      �� 5X      -   5        $ btnMercadoCesiones��5X   !   $��0��6=   $��1��6M   $��Center=�6Z   $��Center=�6u   $��50��5Y   !   $	��350��6-   $��Cascadia Code SemiBold)�69   $��18(�6]   $
	 Hand��6k   ��5Z      �� 5[   "   "
 ��6(   .�5]   &   ��  � �� �#1d6a7d6C   �� 5^   &    � ��
 �����6C   �� 5_   &    � ��
 ���� 6D   �� 5`   &    � ��$ 0q�6H   �� 5a   &    �@�Cursor$
 Hand��6?   �� 5b   &    � �� 5c   *   �� 5d   .   " ��6>   .R�5e   2   �� #��z��69   #��z��5f   9   #��z��5g   9   $ 10��5h   9   ._�5i   6   �� $ 
Horizontal=�6A   $��Center=�6Z   $��Center=�6w   .+�5k   :   �� $��Mercado de Cesiones��6D   #��z��5l   D   $��Center=�5m   D   $��20,0,5,0q�5n   D   $��18(�6V   5o   7   5p   3   5q   /   5s   '    5v   &   P� 5w   *    �@J�IsMouseOver$
 True��6I   . �5x   .   ��  � �� �#22798e6K   �� 5y   .    � ��
 �����6K   5z   +   5|   #   5~      5      5�      5�      5�      5�      5�      