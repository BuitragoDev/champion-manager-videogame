   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      B    <ChampionManager25.UserControls.UC_Menu_Entrenador_Alineacion   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      -   5          >�Name$  
alineacion��5      .��5      � 
������� � ColumnDefinitions 5	   
   �� 5
      $
��1845 �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   ���#1d6a7d$��1q�6D   $��10q�6X   ._�5      � 
������� 5      �� 5      $��* �6'    5      �� 5      $��50 �6$   �� 5      $��* �6$   .+�5      �� ���#1d6a7d$��0��6.   $��0��6>   ._�5      �� $��ALINEACIÓN��6    $��Center=�62   $��Center=�6O   $��Cascadia Code SemiBold)�5      $��22(�68   
������� �� TextAlignment$ Center=�6^   5      �� 5      $��0��6   $��1��6)   ._�5      �  5       �� 5!      $��2* �6/   �� 5"      $��* �6/    5$      �� 5%      $��* �6,   .+�5)      �� $��0��6!   $��0��61   $��10,10,10,10q�6>   ���#1d6a7d$��1q�6i   ._�5*      �  5+   "   �� 5,   &   $��* �67    5.   "   �� 5/   &   $��30 �64   �� 50   &   $	��420 �64   �� 51   &   $��30 �64   �� 52   &   $��* �64   .+�55   "   �� ���#1d6a7d$��0��6>   $��0��6N   ._�56   &   �� $��	TITULARES��60   $��Center=�6A   $��Center=�6^   $��Cascadia Code SemiBold)�57   0   $��22(�6T   
�������$ Center=�6z   58   #   ]� 5:   "   $��0��6,   $��1��6<   $	��Top=�6I   .+�5;   &   &   System.Windows.Controls.DataGrid  -   5          Name$ dgTitulares��5;   /      
IsReadOnly$
 True��6D      AutoGenerateColumns$ False��6V      CanUserSortColumns$ False��6r      GridLinesVisibility$
 None=�5<   /   $��Cascadia Code Light)�6J   $��16(�6k   $	��Top=�6y   	 � Cursor$
	 Hand��5=   /   $	��420��6=   $
��1192��6J   5>   /   5?   #   �� 5A   "   ���#1d6a7d$��0��6>   $��2��6N   ._�5B   &   �� $��RESERVAS��60   $��Center=�6@   $��Center=�6]   $��Cascadia Code SemiBold)�5C   0   $��22(�6T   
�������$ Center=�6z   5D   #   � 5F   "   $��0��6,   $��3��6<   $��Center=�6I   $	��360��6d   .+�5G   &     -   5        $ 
dgReservas��5G   /   $
 True��6C   $ False��6U   $
 None=�5H   /   $��Cascadia Code Light)�6J   $��16(�6k   $	��Top=�6y   $
	 Hand��5I   /   $	��350��6=   $
��1193��6J   5J   /   5K   #   5L      5M      �� 5P      $��1��6!   $��0��61   $��10q�6>   ���#1d6a7d$��1q�6`   ._�5Q      �  5R   "   �� 5S   &   $��* �67    5U   "   �� 5V   &   $��80 �64   �� 5W   &   $��30 �64   �� 5X   &   $��* �64   �� 5Y   &   $	��150 �64   �� 5Z   &   $��30 �64   �� 5[   &   $	��110 �64   �� 5\   &   $��40 �64   �� 5]   &   $	��110 �64   .+�5`   "   �� 
�������$��0��6@   $��0��6P   $��4q�6]   ._�5a   &   �  5b   *   �� 5c   .   $��* �6?   �� 5d   .   $��* �6?   �� 5e   .   $��* �6?   �� 5f   .   $��* �6?   �� 5g   .   $��* �6?   �� 5h   .   $��* �6?    5j   *   �� 5k   .   $��30 �6<   �� 5l   .   $��* �6<   .+�5o   *   �� $��0��61   $��0��6A   $��7��6N   
���  ��._�5p   .   �� $��TÁCTICA PREDEFINIDA��68   $��Center=�6S   $��Center=�6p   $��Cascadia Code SemiBold)�5q   8   $��18(�6\   
�������$ Center=�6�   5r   +   �� 5u   *   -   5        
 ��Name$
 btn541��5u   1   $��0��6A   $��1��6Q   $��Center=�6^   $��Center=�6y   $��35��5v   1   $��80��6=   $��Cascadia Code SemiBold)�6H   $��18(�6l   $
	 Hand��6z   ��5w   .   �� 5x   2    �� 
TargetType" ��68   .�5z   6   ��  �� Property � �� �� Value �#1d6a7d6S   �� 5{   6    � ��
 �����6S   �� 5|   6    � ��
 ���� 6T   �� 5}   6    � ��$ 0q�6X   �� 5~   6    �@�Cursor$
 Hand��6O   �� 5   6    � �� 5�   :   �� 5�   >    �� 
TargetType" ��6N   .R�5�   B   �� #��z��6I   #��z��5�   I   #��z��5�   I   ._�5�   F   ��  �� Orientation$ 
Horizontal=�6Q   $��Center=�6j   $��Center=�6�   .+�5�   J   �� $��5-4-1��6T   
�������$��Center=�5�   T   $��18(�5�   T   5�   G   5�   C   5�   ?   5�   7    �� Triggers 5�   6   P� 5�   :    P� Property �@J�IsMouseOver P� Value$
 True��6Y   . �5�   >   ��  � ��
 �����6\   �� 5�   >    � ��$ 1q�6`   5�   ;   5�   3   5�   +   �� 5�   *   -   5        $
 btn532��5�   1   $��1��6A   $��1��6Q   $��Center=�6^   $��Center=�6y   $��35��5�   1   $��80��6=   $��Cascadia Code SemiBold)�6H   $��18(�6l   $
	 Hand��6z   ��5�   .   �� 5�   2   " ��68   .�5�   6   ��  � �� �#1d6a7d6S   �� 5�   6    � ��
 �����6S   �� 5�   6    � ��
 ���� 6T   �� 5�   6    � ��$ 0q�6X   �� 5�   6    �@�Cursor$
 Hand��6O   �� 5�   6    � �� 5�   :   �� 5�   >   " ��6N   .R�5�   B   �� #��z��6I   #��z��5�   I   #��z��5�   I   ._�5�   F   �� $ 
Horizontal=�6Q   $��Center=�6j   $��Center=�6�   .+�5�   J   �� $��5-3-2��6T   
�������$��Center=�5�   T   $��18(�5�   T   5�   G   5�   C   5�   ?   5�   7    5�   6   P� 5�   :    �@J�IsMouseOver$
 True��6Y   . �5�   >   ��  � ��
 �����6\   �� 5�   >    � ��$ 1q�6`   5�   ;   5�   3   5�   +   �� 5�   *   -   5        $
 btn451��5�   1   $��2��6A   $��1��6Q   $��Center=�6^   $��Center=�6y   $��35��5�   1   $��80��6=   $��Cascadia Code SemiBold)�6H   $��18(�6l   $
	 Hand��6z   ��5�   .   �� 5�   2   " ��68   .�5�   6   ��  � �� �#1d6a7d6S   �� 5�   6    � ��
 �����6S   �� 5�   6    � ��
 ���� 6T   �� 5�   6    � ��$ 0q�6X   �� 5�   6    �@�Cursor$
 Hand��6O   �� 5�   6    � �� 5�   :   �� 5�   >   " ��6N   .R�5�   B   �� #��z��6I   #��z��5�   I   #��z��5�   I   ._�5�   F   �� $ 
Horizontal=�6Q   $��Center=�6j   $��Center=�6�   .+�5�   J   �� $��4-5-1��6T   
�������$��Center=�5�   T   $��18(�5�   T   5�   G   5�   C   5�   ?   5�   7    5�   6   P� 5�   :    �@J�IsMouseOver$
 True��6Y   . �5�   >   ��  � ��
 �����6\   �� 5�   >    � ��$ 1q�6`   5�   ;   5�   3   5�   +   �� 5�   *   -   5        $
 btn442��5�   1   $��3��6A   $��1��6Q   $��Center=�6^   $��Center=�6y   $��35��5�   1   $��80��6=   $��Cascadia Code SemiBold)�6H   $��18(�6l   $
	 Hand��6z   ${�false��6�   ��5�   .   �� 5�   2   " ��68   .�5�   6   ��  � �� �	#FF23282D6S   �� 5�   6    � ��
 �����6S   �� 5�   6    � ��
 ���� 6T   �� 5�   6    � ��$ 0q�6X   �� 5�   6    �@�Cursor$
 Hand��6O   �� 5�   6    � �� 5�   :   �� 5�   >   " ��6N   .R�5�   B   �� #��z��6I   #��z��5�   I   #��z��5�   I   ._�5�   F   �� $ 
Horizontal=�6Q   $��Center=�6j   $��Center=�6�   .+�5�   J   �� $��4-4-2��6T   
�������$��Center=�5�   T   $��18(�5�   T   $	  ��6b   5�   G   5   C   5  ?   5  7    5  6   P� 5  :    �@J�IsMouseOver$
 True��6Y   . �5  >   ��  � ��
 �����6\   �� 5	  >    � ��$ 1q�6`   5
  ;   5  3   5  +   �� 5  *   -   5        $
 btn433��5  1   $��4��6A   $��1��6Q   $��Center=�6^   $��Center=�6y   $��35��5  1   $��80��6=   $��Cascadia Code SemiBold)�6H   $��18(�6l   $
	 Hand��6z   ��5  .   �� 5  2   " ��68   .�5  6   ��  � �� �#1d6a7d6S   �� 5  6    � ��
 �����6S   �� 5  6    � ��
 ���� 6T   �� 5  6    � ��$ 0q�6X   �� 5  6    �@�Cursor$
 Hand��6O   �� 5  6    � �� 5  :   �� 5  >   " ��6N   .R�5  B   �� #��z��6I   #��z��5  I   #��z��5   I   ._�5!  F   �� $ 
Horizontal=�6Q   $��Center=�6j   $��Center=�6�   .+�5"  J   �� $��4-3-3��6T   
�������$��Center=�5$  T   $��18(�5%  T   5&  G   5'  C   5(  ?   5*  7    5-  6   P� 5.  :    �@J�IsMouseOver$
 True��6Y   . �5/  >   ��  � ��
 �����6\   �� 50  >    � ��$ 1q�6`   51  ;   53  3   55  +   �� 58  *   -	   5        $
 btn352��58  1   $��5��6A   $��1��6Q   $��Center=�6^   $��Center=�6y   $��35��59  1   $��80��6=   $��Cascadia Code SemiBold)�6H   $��18(�6l   $
	 Hand��6z   ��5:  .   �� 5;  2   " ��68   .�5=  6   ��  � �� �#1d6a7d6S   �� 5>  6    � ��
 �����6S   �� 5?  6    � ��
 ���� 6T   �� 5@  6    � ��$ 0q�6X   �� 5A  6    �@�Cursor$
 Hand��6O   �� 5B  6    � �� 5C  :   �� 5D  >   " ��6N   .R�5E  B   �� #��z��6I   #��z��5F  I   #��z��5G  I   ._�5H  F   �� $ 
Horizontal=�6Q   $��Center=�6j   $��Center=�6�   .+�5I  J   �� $��3-5-2��6T   
�������$��Center=�5K  T   $��18(�5L  T   5M  G   5N  C   5O  ?   5Q  7    5T  6   P� 5U  :    �@J�IsMouseOver$
 True��6Y   . �5V  >   ��  � ��
 �����6\   �� 5W  >    � ��$ 1q�6`   5X  ;   5Z  3   5\  +   5]  '   5^  #   �� 5`  "   ���#1d6a7d$��0��6>   $��1��6N   ._�5a  &   �� $��ATRIBUTOS DEL JUGADOR��60   $��Center=�6M   $��Center=�6j   $��Cascadia Code SemiBold)�5b  0   $��22(�6T   
�������$ Center=�6z   5c  #   �� 5e  "   $��0��6)   $��2��69   $��10q�6F   ���#1d6a7d$��1q�6h   ._�5f  &   �  5g  *   �� 5h  .   $	��150 �6?   �� 5i  .   $��* �6?    5k  *   �� 5l  .   $��55 �6<   �� 5m  .   $��55 �6<   �� 5n  .   $��* �6<   .+�5q  *   �� $��0��61   $��0��6A   $��2��6N   ._�5r  .   �� -
   5         ��Name$ imgFotoJugador��5r  4   $	��100��6L   $	��100��6Y   $��Center=�5s  4   $��Center=�6Q   $
	 Hand��6l   5t  +   �� 5v  *   -   5         ��Name$ txtNombreJugador��5v  4   $��1��6N   $��0��6^   $�� ��6k   $
��Left=�6s   $��Center=�5w  4   $��Cascadia Code SemiBold)�6O   $��22(�6s   ���	#FF23282D$ Center=�5x  K   �� 5z  *   -   5        $ txtDemarcacionJugador��5z  4   $��1��6S   $��1��6c   $�� ��6p   $
��Left=�6x   $��Center=�5{  4   $��Cascadia Code Light)�6O   $��18(�6p   ���	#FF23282D$ Center=�5|  K   �� 5~  *   $��0��61   $��2��6A   $��2��6N   ._�5  .   �  5�  2   �� 5�  6   $��* �6G   �� 5�  6   $��* �6G   �� 5�  6   $��* �6G    5�  2   �� 5�  6   $��24 �6D   �� 5�  6   $��* �6D   �� 5�  6   $��24 �6D   �� 5�  6   $��* �6D   .+�5�  2   �� $��0��6<   $��0��6L   $��PORTERO��6Y   ���#1d6a7d$��Center=�6}   $��Center=�6�   $��Cascadia Code SemiBold)�5�  <   $��18(�6`   
�������$ Center=�6�   $	��168��6�   �� 5�  2   $��1��6<   $��0��6L   $
��PASE��6Y   ���#1d6a7d$��Center=�6z   $��Center=�6�   $��Cascadia Code SemiBold)�5�  <   $��18(�6`   
�������$ Center=�6�   $	��168��6�   �� 5�  2   $��2��6<   $��0��6L   $��REGATE��6Y   ���#1d6a7d$��Center=�6|   $��Center=�6�   $��Cascadia Code SemiBold)�5�  <   $��18(�6`   
�������$ Center=�6�   $	��168��6�   �� 5�  2   $��0��6<   $��2��6L   $��REMATE��6Y   ���#1d6a7d$��Center=�6|   $��Center=�6�   $��Cascadia Code SemiBold)�5�  <   $��18(�6`   
�������$ Center=�6�   $	��168��6�   �� 5�  2   $��1��6<   $��2��6L   $��ENTRADAS��6Y   ���#1d6a7d$��Center=�6~   $��Center=�6�   $��Cascadia Code SemiBold)�5�  <   $��18(�6`   
�������$ Center=�6�   $	��168��6�   �� 5�  2   $��2��6<   $��2��6L   $
��TIRO��6Y   ���#1d6a7d$��Center=�6z   $��Center=�6�   $��Cascadia Code SemiBold)�5�  <   $��18(�6`   
�������$ Center=�6�   $	��168��6�   �� 5�  2   -   5        $ 
txtPortero��5�  <   $��0��6P   $��1��6`   $�� ��6m   ���	#FF23282D$��Center=�6�   $��Center=�6�   $��Cascadia Code SemiBold)�5�  <   $��28(�6`   
�������$ Center=�6�   $	��168��6�   �� 5�  2   -   5        $ txtPase��5�  <   $��1��6M   $��1��6]   $�� ��6j   ���	#FF23282D$��Center=�6�   $��Center=�6�   $��Cascadia Code SemiBold)�5�  <   $��28(�6`   
�������$ Center=�6�   $	��168��6�   �� 5�  2   -   5        $ 	txtRegate��5�  <   $��2��6O   $��1��6_   $�� ��6l   ���	#FF23282D$��Center=�6�   $��Center=�6�   $��Cascadia Code SemiBold)�5�  <   $��28(�6`   
�������$ Center=�6�   $	��168��6�   �� 5�  2   -   5        $ 	txtRemate��5�  <   $��0��6O   $��3��6_   $�� ��6l   ���	#FF23282D$��Center=�6�   $��Center=�6�   $��Cascadia Code SemiBold)�5�  <   $��28(�6`   
�������$ Center=�6�   $	��168��6�   �� 5�  2   -   5        $ txtEntradas��5�  <   $��1��6Q   $��3��6a   $�� ��6n   ���	#FF23282D$��Center=�6�   $��Center=�6�   $��Cascadia Code SemiBold)�5�  <   $��28(�6`   
�������$ Center=�6�   $	��168��6�   �� 5�  2   -   5        $ txtTiro��5�  <   $��2��6M   $��3��6]   $�� ��6j   ���	#FF23282D$��Center=�6�   $��Center=�6�   $��Cascadia Code SemiBold)�5�  <   $��28(�6`   
�������$ Center=�6�   $	��168��6�   5�  /   5�  +   5�  '   5�  #   �� 5�  "   $��0��6)   $��3��69   $��
10,10,10,0q�6F   ._�5�  &   �  5�  *   �� 5�  .   $��* �6?   �� 5�  .   $��* �6?    5�  *   �� 5�  .   $��20 �6<   �� 5�  .   $��* �6<   .+�5�  *   �� ���#1d6a7d$��0��6F   $��0��6V   ._�5�  .   �� $��POSICIÓN IDEAL��68   $��Center=�6N   $��Center=�6k   $��Cascadia Code SemiBold)�5�  8   $��18(�6\   
�������$ Center=�6�   5�  +   �� 5�  *   -   5        $ imgDemarcacionIdeal��5�  0   $��0��6M   $��1��6]   $��Center=�6j   $��Center=�5�  0   $��Uniform=�6K   $��5q�6]   �� 5�  *   ���#1d6a7d$��1��6F   $��0��6V   ._�5�  .   �� $��POSICIÓN ACTUAL��68   $��Center=�6O   $��Center=�6l   $��Cascadia Code SemiBold)�5�  8   $��18(�6\   
�������$ Center=�6�   5�  +   �� 5�  *   -   5        $ imgDemarcacionActual��5�  0   $��1��6N   $��1��6^   $��Center=�6k   $��Center=�5�  0   $��Uniform=�6K   $��5q�6]   5�  '   5�  #   �� 5�  "   ���#1d6a7d$��0��6>   $��4��6N   ._�5�  &   �� $��CAMBIOS��60   $��Center=�6?   $��Center=�6\   $��Cascadia Code SemiBold)�5�  0   $��22(�6T   
�������$ Center=�6z   5�  #   �� 5�  "   -   5         ��Name$ bordeCambio1��5�  )   
�������$��0��6V   $��5��6f   ._�5�  &   �  5�  *   �� 5�  .   $	��120 �6?   �� 5�  .   $��* �6?    5�  *   �� 5�  .   $��* �6<   .+�5�  *   �� -   5        $ imgFotoCambio1��5�  0   $��0��6H   $��0��6X   $	��100��6e   $	��100��6r   $��Center=�5�  0   $��Center=�6M   $
	 Hand��6h   �� 5�  *   -   5        $ 
txtCambio1��5�  4   $��1��6H   $��0��6X   $�� ��6e   $
��Left=�6m   $��Center=�5�  4   $��Cascadia Code SemiBold)�6O   $��22(�6s   ���	#FF23282D$ Center=�5�  K   5�  '   5�  #   �� 5�  "   ���#1d6a7d$��0��6>   $��6��6N   ._�5�  &   �� $��32��6,   $��32��68   $��Center=�6C   $��Center=�6`   $
	 Hand��6{   $)��#/Recursos/img/icons/intercambio.png��5�  ,   5�  #   �� 5�  "   -   5        $ bordeCambio2��5�  )   
�������$��0��6W   $��7��6g   ._�5�  &   �  5�  *   �� 5�  .   $	��120 �6?   �� 5�  .   $��* �6?    5�  *   �� 5�  .   $��* �6<   .+�5�  *   �� -   5        $ imgFotoCambio2��5�  0   $��0��6H   $��0��6X   $	��100��6e   $	��100��6r   $��Center=�5�  0   $��Center=�6M   $
	 Hand��6h   �� 5�  *   -   5        $ 
txtCambio2��5�  4   $��1��6H   $��0��6X   $�� ��6e   $
��Left=�6m   $��Center=�5�  4   $��Cascadia Code SemiBold)�6O   $��22(�6s   ���	#FF23282D$ Center=�5�  K   5�  '   5�  #   5�     5�     5�     5�     5�     5�     5�     5�     