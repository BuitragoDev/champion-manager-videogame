   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ChampionManager25.VistasChampionManager25.Vistas  5   	   .    (ChampionManager25.Vistas.frmMenuOpciones   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:ChampionManager25.Vistas     5   	     � Title$  frmMenuOpciones��5      $	��303��62   $	��600��6?    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�6    � WindowStartupLocation$ CenterScreen=�62   .��5	      � ���	#FF23282D � ColumnDefinitions 5
   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� 
�������$��0��6)   $��0��69   $��5q�6F   ._�5      �  5      �� 5      $��* �6'   �� 5      $	��100 �6'    5      �� 5      $��60 �6$   �� 5      $��* �6$   �� 5      $��* �6$   .+�5      �� $��0��6   $��0��6)   $��2��66   ���#1d6a7d._�5      �  5      �� 5       $��* �6/   �� 5!      $	��100 �6/    5#      �� 5$      $��* �6,   .+�5'      �� $��0��6$   $��0��64   $��MENU OPCIONES��6A   $��Center=�6V   $��Center=�6s   $��Cascadia Code SemiBold)�5(   $   $��20(�6H   
������� �� TextAlignment$ Center=�6n   $��75,0,0,0q�6�   �� 5*      -   5         ��Name$ 	imgCerrar��5*       $��1��63   $��0��6C   $��32��6P   $��32��6[   $��Uniform=�6g   $��Center=�6y   $��Center=�5+        � Cursor$
 Hand��6=   $(��"/Recursos/img/icons/cerrar32px.png��6K   6w   5,      5-      �� 5/      $��0��6   $��1��6)   $��2��66   ._�50      �� -   5        	 ��Name$	 
btnGuardar��50      $��Center=�61   $��Center=�6L   $��70��51      $	��350��6)   $��Cascadia Code SemiBold)�65   $��18(�6Y   $
 Hand��6g   ��52      �� 53      
 �� 
TargetType"
 ��6$   .�55   "   ��  �� Property � �� �� Value �#1d6a7d6?   �� 56   "    � ��
 �����6?   �� 57   "    � �� �	#FF23282D6@   �� 58   "    � ��$ 2q�6D   �� 59   "    �@�Cursor$
 Hand��6;   �� 5:   "    � �� 5;   &   �� 5<   *    �� 
TargetType" ��6:   .R�5=   .   �� #��z��65   #��z��5>   5   #��z��5?   5   ._�5@   2   ��  �� Orientation$ 
Horizontal=�6=   $��Center=�6V   $��Center=�6s   .+�5B   6   �� $��VOLVER AL MENU PRINCIPAL��6@   #��z��5C   @   $��Center=�5D   @   $��20,0,5,0q�5E   @   $��22(�6R   5F   3   5G   /   5H   +   5J   #    �� Triggers 5M   "   P� 5N   &    P� Property �@J�IsMouseOver P� Value$
 True��6E   . �5O   *   ��  � ��
 �����6H   �� 5P   *    � ��$ 1q�6L   5Q   '   5S      5U      5V      �� 5X      $��0��6   $��2��6)   $��2��66   ._�5Y      �� -   5        $	 btnSalir��5Y      $��Center=�6/   $��Center=�6J   $��70��5Z      $	��350��6)   $��Cascadia Code SemiBold)�65   $��18(�6Y   $
 Hand��6g   ��5[      �� 5\      "
 ��6$   .�5^   "   ��  � �� �#1d6a7d6?   �� 5_   "    � ��
 �����6?   �� 5`   "    � �� �	#FF23282D6@   �� 5a   "    � ��$ 2q�6D   �� 5b   "    �@�Cursor$
 Hand��6;   �� 5c   "    � �� 5d   &   �� 5e   *   " ��6:   .R�5f   .   �� #��z��65   #��z��5g   5   #��z��5h   5   ._�5i   2   �� $ 
Horizontal=�6=   $��Center=�6V   $��Center=�6s   .+�5k   6   �� $��SALIR DEL JUEGO��6@   #��z��5l   @   $��Center=�5m   @   $��20,0,5,0q�5n   @   $��22(�6R   5o   3   5p   /   5q   +   5s   #    5v   "   P� 5w   &    �@J�IsMouseOver$
 True��6E   . �5x   *   ��  � ��
 �����6H   �� 5y   *    � ��$ 1q�6L   5z   '   5|      5~      5      5�      5�      5�      5�      