   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      @    :ChampionManager25.UserControls.UC_Menu_Estadio_Informacion   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      -   5        .��5      � ��5	   
   �� 5
      -   5        imgFondo��  �� ImageSource$C  =pack://application:,,,/Recursos/img/backgroundEstadio_img.png��5
   +    j� Stretch$ UniformToFill=�6w    j� TileMode$
 None=�6�    � ColumnDefinitions 5   
   �� 5      $	��745 �6   �� 5      $
��1100 �6    � RowDefinitions 5   
   �� 5      $	��930 �6   .+�5   
   �� 
�������$��5q�5      ���#1d6a7d$��
37,100,5,0q�5      $��0��5      $��0��6!   $	��Top=�6.   $	��764��6F   ._�5      �  5      �� 5      $	��700 �6'    5      �� 5       $��70 �6$   �� 5!      $��50 �6$   �� 5"      $��64 �6$   �� 5#      $��50 �6$   �� 5$      $	��540 �6$   .+�5'      �� $��0��6   $��0��6)   ���#23282d._�5(      �� -   5         ��Name$ txtNombreEstadio��5(       $��2��6:   $
��Left=�6J   $��Nombre Estadio��6e   $��Center=�6{   $��Cascadia Code SemiBold)�5)       $��24(�6D   
�������$��20,0,0,0q�6j   5*      �� 5,      $��0��6   $��1��6)   ���#1d6a7d._�5-      �� -   5        $ 	txtCancha��5-       $��2��63   $
��Left=�6C   $��	CAPACIDAD��6^   $��Center=�6o   $��Cascadia Code SemiBold)�5.       $��18(�6D   
�������$��20,0,0,0q�6j   5/      �� 51      $��0��6   $��2��6)   ._�52      �  53      �� 54      $��84 �6/   �� 55      $��* �6/    57      �� 58      $��* �6,   .+�5;      �� -   5         ��Name$ imgPabellonIcono��5;       $��0��6:   $��0��6J   $��Center=�6W   $��Center=�6r   $��50��5<       $��50��6+   $��Uniform=�67   $/��)/Recursos/img/icons/estadioNegro_icon.png��6I   $��20,0,0,0q�6|   �� 5=      -   5        $ txtCapacidad��5=   $   $��1��6:   $��0��6J   $
��Left=�6W   $��espectadores��6r   $��Center=�6�   $��Cascadia Code SemiBold)�5>   $   $��18(�6H   
���   �$��20,0,0,0q�6i   5?      5@      �� 5B      $��0��6   $��3��6)   ���#1d6a7d._�5C      �� -   5        $ txtEquipamiento��5C       $��2��69   $
��Left=�6I   $��INTERIOR DEL ESTADIO��6d   $��Center=�6�   $��Cascadia Code SemiBold)�5D       $��18(�6D   
�������$��20,0,0,0q�6j   5E      �� 5F      -   5        $ 
imgEstadio��5F      $��4��6,   $	��Top=�69   $
��Left=�6Q   $	��700��5G      $	��540��6$   $��UniformToFill=�61   5I      5J      �� 5K   
   -	   5        $ imgLogo��5K      $��1��6!   $
��Left=�61   $	��200��6L   $��132,322,0,0q�6Y   $	��Top=�6n   $	��200��6�   5L      5M      