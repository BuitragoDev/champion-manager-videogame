   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ChampionManager25.VistasChampionManager25.Vistas  5   	   0    *ChampionManager25.Vistas.frmVentanaDespido   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:ChampionManager25.Vistas     5   	   -   5          �Name$  ventanaDespido��5   	    � Title$ frmVentanaDespido��5      $	��400��64   $	��800��6A    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�6    � WindowStartupLocation$ CenterScreen=�62   .��5	      � ���	#FF23282D � ColumnDefinitions 5
   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� 
�������$��0��6)   $��0��69   $��5q�6F   ._�5      �  5      �� 5      $��* �6'    5      �� 5      $��40 �6$   �� 5      $��* �6$   �� 5      $��80 �6$   .+�5      �� 
���  ��$��0��6.   $��0��6>   ._�5      �� -   5         ��Name$ txtTituloVentana��5       $�� ��6:   $��Center=�6B   $��Center=�6_   $��Cascadia Code SemiBold)�5      $��20(�6@   
������� �� TextAlignment$ Center=�6f   5      �� 5!      $��0��6   $��1��6)   ._�5"      �� -   5        $ 
txtMensaje��5"       $�� ��64   $��Center=�6<   $��Center=�6Y   $	��738��6t   $
��Wrap=�6�   $��Cascadia Code Light)�5#      $��18(�6=   ���	#FF23282D$
 Left=�6b   $	��200��6w   5$      �� 5&      ���	#FF23282D$��0��60   $��2��6@   ._�5'      �  5(      �� 5)      $��* �6/    5+      �� 5,      $��* �6,   .+�5/      �� -   5        	 ��Name$	 btnSalir��5/   !   $��0��63   $��0��6C   $��Center=�6P   $��Center=�6k   $��40��50      $	��250��6)   $��Cascadia Code SemiBold)�65   $��18(�6Y   
 � Cursor$

 Hand��6g   ��51      �� 52   "    �� 
TargetType" ��6(   .�54   &   ��  �� Property � �� �� Value
 �  ��6C   �� 55   &    � ��
 �����6C   �� 56   &    � ��
 ���� 6D   �� 57   &    � ��$ 0q�6H   �� 58   &    �@�Cursor$
 Hand��6?   �� 59   &    � �� 5:   *   �� 5;   .    �� 
TargetType" ��6>   .R�5<   2   �� #��z��69   #��z��5=   5   #��z��5>   5   ._�5?   6   ��  �� Orientation$ 
Horizontal=�6A   $��Center=�6Z   $��Center=�6w   .+�5A   :   �� $��SALIR��6D   #��z��5B   @   $��Center=�5C   @   $��20,0,5,0q�5D   @   $��22(�6R   5E   7   5F   3   5G   /   5I   '    �� Triggers 5L   &   P� 5M   *    P� Property �@J�IsMouseOver P� Value$
 True��6I   . �5N   .   ��  � ��
 �����6L   �� 5O   .    � ��$ 1q�6P   5P   +   5R   #   5T      5U      5V      5W      5X      5Y      5Z      