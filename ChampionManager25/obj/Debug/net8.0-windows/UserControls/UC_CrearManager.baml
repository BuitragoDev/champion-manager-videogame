   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      4    .ChampionManager25.UserControls.UC_CrearManager   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      -   5          >�Name$  crearManager��5       � 	Resources 5         ComboBoxToggleButton �� 
TargetType � ColumnDefinitions ��Name �� CornerRadius F�Name  ComboBoxTextBox �� 
TargetType �� Property	 �� Value
 �� 	ClickMode d� 	IsChecked �� Path �� Mode �� RelativeSource J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate ��Name �� 
IsReadOnly �� 
IsReadOnly �� IsDropDownOpen � AllowsTransparency J� SnapsToDevicePixels � ActualWidth �� MaxDropDownHeight K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation �� Triggers P� Property P� Value �� 
TargetName  P� 
SourceName! �� Triggers" �� Padding��%  &	        &	     '�� m    7   7  '�� �    �� 5	   
   " d�67   .R�5
      �  5      �� 5      �� 5      $��20 �6'   .+�5      �� $ Border��6   $��2��5      $ 0��5      
����������	#FF97A0A5$��1q�5      �� 5      $��0��6   $ 0��5      $��1q�5      
����������	#FF97A0A5$��0,0,1,0q�5      F� 5      $ Arrow��6   $��1��5      
���   �$��Center=�5      $��Center=�5      =����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	5       5"      51      �� 53   
   " ��62   .R�54      �� $ PART_ContentHost��6   $|�False��6/   #��z��6A   55      �� 57   
   " ��6*   .�58      ��  �@J�SnapsToDevicePixels$
	 true��64   �� 59       �@�OverridesDefaultStyle$
	 true��66   �� 5:       � ��$
	 Auto=�6K   �� 5;       � ��$
	 Auto=�6I   �� 5<       � ��$
	 true��6>   �� 5=       � ��$	 70��6)   �� 5>       � ��$	 20��6*   �� 5?       � ��	 �	#FF23282D6+   �� 5@       � ��	 5A      �� 5B      " ��6&   .R�5C      � .+�5D      d� $��ToggleButton��5E      8��  $��2��5G      $|�false��5H      $
 Press=�5J       5I      �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+5K      �� 5L      $��ContentSite��6/   $ False��6B   #��z 6\   #��z 5M      #��z��5N      $��	15,3,23,3q�5O      $��Center=�5P      $
��Left=�5Q      �� 5R      $ PART_EditableTextBox��6&   8�� $
��Left=�5U      $��Center=�5V      $��3,3,23,3q�5W      $
|�True��5X      ���	#FF3F3F3F
��� � �$y�Hidden=�5[      # z 5\      ��5S      Z� 5\      � 5]      $��Popup��5^      $��Bottom=�5_      #��z 5`      $
 True��5a      $|�False��5b      $��Slide=�5c      .��5e   "   � $��DropDown��6'   $
 True��5f      #��z 5g      #��z 5h      .+�5i   &   �� $ DropDownBorder��5j   !   ���	#FF3F3F3F$��1q�5m   !   ���#8888885n   !   �� 5o   &   $��4,6,4,6q�63   $
 True��6D   .��5p   *   �� $
 True��65   $ 	Contained=�6H   5q   '   5r   #   5s      5t       5u      P� 5v       �@��HasItems$ false��6:   . �5w   "   �� $ DropDownBorder��6)    � ��$	 95��6Z   5x      P� 5y       � {�$ false��6;   . �5z   "   ��  � ��	 �#8888886?   5{      P� 5|       �@��
IsGrouping$
 true��6<   . �5}   "   ��  � ��$	 false��6R   5~      P� 5      $  Popup��6&    �@�AllowsTransparency$
 true��6]   . �5�   "   �� $ DropDownBorder��6)    �@��CornerRadius$	 0��6]   �� 5�   "   $ DropDownBorder��6)    � ��$	 0,2,0,0q�6W   5�      P� 5�       �@��
IsEditable$
 true��6=   . �5�   "   ��  � ��$	 false��6>   �� 5�   "   $ PART_EditableTextBox��6)    � y�$	 Visible=�6a   �� 5�   "   $ ContentSite��6)    � y�$	 Hidden=�6X   5�      5�      5�      ! 5�      5�      �� 5�   
   " ��6.   .�5�      ��  �@J�SnapsToDevicePixels$
	 true��64   �� 5�       � ��
	 �����6+   �� 5�       �@�OverridesDefaultStyle$
	 true��66   �� 5�       � ��	 5�      �� 5�      " ��6&   .R�5�      �� $��Border��6!   $" 2q�5�      $
 true��5�      ._�5�      �� 5�       5�      P� 5�       �@��IsHighlighted$
 true��6?   . �5�   "   �� $ Border��6)    � ��	 �	#FF4F4F4F6S   5�      P� 5�       � {�$ false��6;   . �5�   "   ��  � ��	 �#8888886?   5�      5�      5�      5�      5�      .��5�      � ��5�   
   �� 5�      # �� ImageSource$0# */Recursos/img/backgroundCarga1920x1080.png��6   $ j� Stretch$$ UniformToFill=�6R   % j� TileMode$
% None=�6j    5�   
   �� 5�      $��* �6   �� 5�      $��3* �6   �� 5�      $��* �6   & � RowDefinitions& 5�   
   �� 5�      $��80 �6   �� 5�      $��* �6   .+�5�   
   �� -   5        $ borderBarraSuperior��5�      $��0��6.   $��0��6>   $��3��6K   ���#23282d._�5�      �  5�      �� 5�      $��* �6'   �� 5�      $��3* �6'   �� 5�      $��* �6'   & 5�      �� 5�      $��* �6$   .+�5�      �� $��0��6   $��0��6)   ���#23282d._�5�      �  5�      �� 5�      $��80 �6/   �� 5�      $��* �6/   & 5�      �� 5�      $��* �6,   .+�5�      �� $��0��6!   $��0��61   
�������._�5�      �� -   5        ' ��Name$' imgBotonAtras��5�   $   $��32��6;   $��32��6F   $��Center=�6R   $
��Left=�6m   $��20,0,0,0q�6�   $;��5/Recursos/img/icons/flechaIzquierdaNegra64px_icon.png��5�   $   ( � Cursor$
( Hand��6c   6q   5�      5�      5�      �� 5�      $��1��6   $��0��6,   $��Center=�69   $��Center=�6T   $��Crear Nuevo Manager��6q   
�������$��28(�5�   4   $��Cascadia Code SemiBold)�6B   �� 5�      -   5        ) ��Name$) 
btnAvanzar��5�      $��2��6-   $��Center=�6=   $
��Left=�6X   $��50��5�      $	��328��6%   $��36,0,0,0q�61   $��SELECCIÓN DE EQUIPO��6C   $��Cascadia Code SemiBold)�5�      $��18(�6=   $
( Hand��6K   ${�False��6Y   ��5�      �� 5�      " ��6    .�5�      ��  � ��	 �#1d6a7d6;   �� 5�       � ��
	 �����6;   �� 5�       � ��$	 24(�69   �� 5�       �@J�SnapsToDevicePixels$
	 True��6D   �� 5�       � ��	 5�   "   �� 5�   &   " ��66   .R�5�   *   �� $ 4��61   #��z��6B   ._�5�   .   � .+�5�   2   �� $' imgBoton��68   $:��4/Recursos/img/icons/flechaDerechaBlanca64px_icon.png��6J   $��32��6�   $��32��6�   $��Center=�6�   $
��Left=�6�   $��Uniform=�6�   $��17,0,0,0q�6�   �� 5�   2   * ��Name$* MyContentPresenter��6C   #��z��6_   $��Center=�6�   $��Center=�6�   $��30,0,0,0q�6�   5�   /   5�   +    5�   *   P� 5�   .    �@J�IsMouseOver$
 True��6M   . �5�   2   ��  � ��	 �#22798e6O   �� 5�   2    � ��
	 �����6O   5�   /   5�   '   5�      5�      5�      5�      5�      �� 5�   
   -   5        $ borderFormularioCreacion��5�      $��1��63   $��1��6C   
����������#23282d$��1q�6~   $��100,100,100,100q�5�      + J� Effect+ 5      3  -System.Windows.Media.Effects.DropShadowEffect  5     ,   Color$, Black��6#   -   
BlurRadius$- 10Q�5     .   ShadowDepth$. 5Q�5     /   	Direction$	/ 320Q�5     0   Opacity$	0 0.5Q�5     ._�5     �  5	     �� 5
     $��* �6'   �� 5     $��* �6'   & 5     �� 5     $��20 �6$   �� 5     $��2* �6$   �� 5     $��20 �6$   �� 5     $��* �6$   �� 5     $��20 �6$   �� 5     $��* �6$   �� 5     $��20 �6$   �� 5     $��* �6$   .+�5     �� $��0��6   $��0��6)   $��2��66   ���#23282d._�5     �� $��Center=�6    $��Center=�6;   $��IMAGEN DEL MANAGER��6X   
�������$��18(�5  8   $��Cascadia Code SemiBold)�6F   5     �� 5     -   5        $' 	imgAvatar��5     $��0��6+   $��1��6;   $��2��6H   $	��256��6\   $	��256��6h   $��Center=�6u   $��Center=�5     $
( Hand��65   6C   �� 5"     $��0��6   $��2��6)   ���#23282d._�5#     �� $��Center=�6    $��Center=�6;   $��NOMBRE��6X   
�������$��18(�5$  8   $��Cascadia Code SemiBold)�6F   5%     �� 5'     -   5        $ 	txtNombre��5'     $��0��6-   $��3��6=   $��Cascadia Code SemiBold)�6J   $��20(�6n   $	��300��5(     $��40��6%   $��Center=�61   1 �� TextAlignment$1 Center=�6S   2 �� 	MaxLength$2 15��5)     $��Center=�6(   $��Center=�6C   6�   �� 5,     $��1��6   $��2��6)   ���#23282d._�5-     �� $��Center=�6    $��Center=�6;   $��APELLIDO��6X   
�������$��18(�5.  8   $��Cascadia Code SemiBold)�6F   5/     �� 51     -   5        $ txtApellido��51     $��1��6/   $��3��6?   $��Cascadia Code SemiBold)�6L   $��20(�6p   $	��300��52     $��40��6&   $��Center=�62   $1 Center=�6T   $2 15��53     $��Center=�6)   $��Center=�6D   6�   �� 56     $��0��6   $��4��6)   $��2��66   ���#23282d._�57     �� $��Center=�6    $��Center=�6;   $��NACIONALIDAD��6X   
�������$��18(�58  8   $��Cascadia Code SemiBold)�6F   59     �� 5;     -	   5        3 ��Name$3 cbNacionalidad��5;     $��0��63   $��5��6C   $��2��6P   $��Center=�6d   $	��350��5<     $��40��6'   $��Center=�63   $��Cascadia Code SemiBold)�6N   $��20(�6r   $��Center=�5=     4 �� SelectedIndex$4 59��6=   $
( Hand��6P   �� 5@     $��0��6   $��6��6)   $��2��66   ���#23282d._�5A     �� $��0��6    $��0��60   $��3��6=   $��Center=�6Q   $��Center=�6l   $��FECHA DE NACIMIENTO��6�   
�������$��18(�5B  8   $��Cascadia Code SemiBold)�6F   5C     �� 5E     $��0��6   $��7��6)   $��2��66   ._�5F     �  5G     �� 5H     $��* �6/   �� 5I     $��* �6/   �� 5J     $��* �6/   & 5L     �� 5M     $��20 �6,   �� 5N     $��* �6,   .+�5R     �� $��0��6!   $��0��61   ���#23282d._�5S     �� $��Center=�6(   $��Center=�6C   $	��DIA��6`   
�������$��18(�5T  @   $��Cascadia Code SemiBold)�6N   5U     �� 5W     -
   5        $ txtDia��5W  "   $��0��62   $��1��6B   $��Cascadia Code SemiBold)�6O   $��20(�6s   $��75��5X  "   $��40��6-   $��Center=�69   $1 Center=�6[   $2 2��5Y  "   $��Center=�60   $��Center=�6K   6�   �� 5\     $��1��6!   $��0��61   ���#23282d._�5]     �� $��Center=�6(   $��Center=�6C   $	��MES��6`   
�������$��18(�5^  @   $��Cascadia Code SemiBold)�6N   5_     �� 5a     -   5        $ txtMes��5a  "   $��1��62   $��1��6B   $��Cascadia Code SemiBold)�6O   $��20(�6s   $��75��5b  "   $��40��6-   $��Center=�69   $1 Center=�6[   $2 2��5c  "   $��Center=�60   $��Center=�6K   6�   �� 5f     $��2��6!   $��0��61   ���#23282d._�5g     �� $��Center=�6(   $��Center=�6C   $
��AÑO��6`   
�������$��18(�5h  @   $��Cascadia Code SemiBold)�6N   5i     �� 5k     -   5        $ txtAnio��5k  "   $��2��63   $��1��6C   $��Cascadia Code SemiBold)�6P   $��20(�6t   $	��100��5l  "   $��40��6.   $��Center=�6:   $1 Center=�6\   $2 4��5m  "   $��Center=�60   $��Center=�6K   6�   5n     5o     5p     5q     5r     5s     