   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      @    :ChampionManager25.UserControls.UC_Menu_Finanzas_DerechosTV   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      -   5        .��5      � ��5	   
   �� 5
        �� ImageSource$@  :pack://application:,,,/Recursos/img/background_oficina.png��6    j� Stretch$ Uniform=�6b    j� TileMode$
 None=�6t    � ColumnDefinitions 5   
   �� 5      $	��920 �6   �� 5      $	��920 �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��70 �6   .+�5   
   �� -   5         ��Name$ bordeLadoIzquierdo��5      
�������$��1q�6E   ���#23282d$��15q�6o   $��0��5      $��0��6!   ._�5      �  5      �� 5      $��* �6'   �� 5      $��* �6'    5      �� 5      $��40 �6$   �� 5       $��* �6$   .+�5#      �� $��0��6   $��0��6)   $��2��66   ���#1d6a7d._�5$      �� $��DERECHOS TELEVISIVOS��6    $��Center=�6<   $��Center=�6Y   $��Cascadia Code SemiBold)�5%   !   $��20(�6E   
������� �� TextAlignment$ Center=�6k   5&      �� 5(      $��0��6   $��1��6)   
�������._�5)      �� -   5         ��Name$ imgLogo��5)      $	��200��6-   $	��200��69   $��Uniform=�6F   $��Center=�6X   $��Center=�6u   5*      �� 5,      $��1��6   $��2��6)   $��10q�66   $��1q�6B   ���#23282d._�5-      �  5.      �� 5/      $��* �6/    51      �� 52      $��30 �6,   �� 53      $��* �6,   �� 54      $��30 �6,   �� 55      $	��120 �6,   �� 56      $��30 �6,   �� 57      $	��120 �6,   �� 58      $��30 �6,   �� 59      $	��120 �6,   .+�5<      �� $��0��6!   $��0��61   ���#23282d._�5=      �� -   5         ��Name$ txtTelevision1��5=   (   $��CADENA DE TV��6@   $��Center=�6T   $��Center=�6q   $��Cascadia Code SemiBold)�5>   (   $��20(�6L   
�������$ Center=�6r   5?      �� 5A      -   5        $ imgTelevision1��5A       $��0��68   $��1��6H   $	��200��6U   $	��200��6a   $��Uniform=�6n   $��Center=�5B       $��Center=�6=   �� 5D      $��0��6!   $��2��61   ���#23282d._�5E      �� $��PAGO INICIAL��6(   $��Center=�6<   $��Center=�6Y   $��Cascadia Code SemiBold)�5F   (   $��20(�6L   
�������$ Center=�6r   5G      �� 5I      -   5        $ txtCantidadTelevision1��5I   $   $��0��6D   $��3��6T   $�� ��6a   $��Center=�6i   $��Center=�6�   $��Cascadia Code SemiBold)�5J   $   $��20(�6H   
���   �$ Center=�6i   �� 5L      $��0��6!   $��4��61   ���#23282d._�5M      �� $��PAGO MENSUAL��6(   $��Center=�6<   $��Center=�6Y   $��Cascadia Code SemiBold)�5N   (   $��20(�6L   
�������$ Center=�6r   5O      �� 5Q      -   5        $ txtMensualidadTelevision1��5Q   $   $��0��6G   $��5��6W   $�� ��6d   $��Center=�6l   $��Center=�6�   $��Cascadia Code SemiBold)�5R   $   $��20(�6H   
���   �$ Center=�6i   �� 5T      $��0��6!   $��6��61   ���#23282d._�5U      �� $��DURACIÓN DEL CONTRATO��6(   $��Center=�6E   $��Center=�6b   $��Cascadia Code SemiBold)�5V   (   $��20(�6L   
�������$ Center=�6r   5W      �� 5Y      -   5        $ txtDuracionTelevision1��5Y   $   $��0��6D   $��7��6T   $�� ��6a   $��Center=�6i   $��Center=�6�   $��Cascadia Code SemiBold)�5Z   $   $��20(�6H   
���   �$ Center=�6i   5[      5\      5]      5^      �� 5a   
   $��0��6   $��1��6!   $��2��6.   ._�5b      �� -	   5        	 ��Name$	 btnOfertasTelevisiones��5b      $��50��65   $	��350��6A   $��
OFERTAS TV��6M   $
��Left=�5c      $	��Top=�60   ���#23282d
�������$��15,0,0,0q�6u   $��Cascadia Code SemiBold)�5d      $��20(�69   
 � Cursor$

 Hand��6G   ��5e      �� 5f       �� 
TargetType" ��6   .�5g      ��  �� Property � �� �� Value �	#FF23282D67   �� 5h       � ��
 �����67   �� 5i       � ��$ 24(�65   �� 5j       �@J�SnapsToDevicePixels$
 True��6@   �� 5l       � �� 5m      �� 5n   "    �� 
TargetType" ��62   .R�5o   &   ��  �� CornerRadius$ 4��6-   #��z��6>   ._�5p   *   � .+�5q   .   �� $ imgBoton��64   $:��4/Recursos/img/icons/flechaDerechaBlanca64px_icon.png��6F   $��32��6�   $��32��6�   $��Center=�6�   $
��Left=�6�   $��Uniform=�6�   $��17,0,0,0q�6�   �� 5r   .    ��Name$ MyContentPresenter��6?   #��z��6[   $��Center=�6   $��Center=�6�   $��20,0,0,0q�6�   5s   +   5t   '    �� Triggers 5v   &   P� 5w   *    P� Property �@J�IsMouseOver P� Value$
 True��6I   . �5x   .   ��  � �� �#2d343a6K   �� 5y   .    � ��
 �����6K   5z   +   5|   #   5~      5      5�      5�      5�      5�      