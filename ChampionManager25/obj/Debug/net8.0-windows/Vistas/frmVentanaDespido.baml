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
 Left=�6b   $	��200��6w   5$      �� 5&      ���	#FF23282D$��0��60   $��2��6@   ._�5'      �  5(      �� 5)      $��* �6/    5+      �� 5,      $��* �6,   .+�5/      �� -   5        	 ��Name$	 btnSalir��5/   !   $��0��63   $��0��6C   $��SALIR��6P   $��Center=�6`   $��Center=�6{   $��40��50      $	��250��6)   $��Cascadia Code SemiBold)�65   $��24(�6Y   
 � Cursor$

 Hand��6g   ��51      �� 52   "    �� 
TargetType" ��6(   .�53   &   ��  �� Property � �� �� Value �#8b00006C   �� 54   &    � ��
 �����6C   �� 55   &    � ��$ 24(�6A   �� 56   &    �@J�SnapsToDevicePixels$
 True��6L   �� 58   &    � �� 59   *   �� 5:   .    �� 
TargetType" ��6>   .R�5;   2   ��  �� CornerRadius$ 4��69   #��z��6J   ._�5<   6   � .+�5=   :   ��  ��Name$ imgBoton��6@   $(��"/Recursos/img/icons/close_icon.png��6R   $��24��6~   $��24��6�   $��Center=�6�   $
��Left=�6�   $��Uniform=�6�   $��17,0,0,0q�6�   �� 5>   :    ��Name$ MyContentPresenter��6K   #��z��6g   $��Center=�6�   $��Center=�6�   $��25,0,0,0q�6�   5?   7   5@   3    �� Triggers 5B   2   P� 5C   6    P� Property �@J�IsMouseOver P� Value$
 True��6U   . �5D   :   ��  � �� �#a80b0b6W   �� 5E   :    � ��
 �����6W   5F   7   5H   /   5J   '   5K   #   5M      5N      5O      5P      5Q      5R      5S      