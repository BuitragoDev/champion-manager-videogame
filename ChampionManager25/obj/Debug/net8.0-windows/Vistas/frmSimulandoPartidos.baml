   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ChampionManager25.VistasChampionManager25.Vistas  5   	   3    -ChampionManager25.Vistas.frmSimulandoPartidos   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:ChampionManager25.Vistas     5   	   -   5          �Name$  simulacionPartidos��5   	   $
��1080��5      $
��1920��6(    � 
ResizeMode$ NoResize=�65    � WindowStyle$
 None=�6K    � WindowState$ 	Maximized=�6^   .��5      � ��5
   
   �� 5       �� ImageSource$4 ./Recursos/img/backgroundPrincipal1920x1080.png��6    j� Stretch$ UniformToFill=�6V    j� TileMode$
 None=�6n    � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��* �6   �� 5      $	��100 �6    � RowDefinitions 5   
   �� 5      $��80 �6   �� 5      $��* �6   .+�5   
   �� -   5        	 ��Name$	 borderBarraSuperior��5      $��0��6.   $��0��6>   $��3��6K   ���#23282d._�5      �  5      �� 5      $��* �6'   �� 5      $��3* �6'   �� 5      $��* �6'    5       �� 5!      $��* �6$   .+�5$      �� $��0��6   $��0��6)   ���#23282d._�5%      �  5&      �� 5'      $��80 �6/   �� 5(      $��* �6/    5*      �� 5+      $��* �6,   .+�5.      �� $��0��6!   $��0��61   
�������._�5/      �� -   5        
 ��Name$
 imgCompeticion��5/   $   $��64��6<   $��64��6G   $��Uniform=�6S   $��Center=�6e   $��Center=�50   $   51      52      53      �� 55      -   5         ��Name$ lblTituloVentana��55      $��1��66   $��0��6F   $��Center=�6S   $��Center=�6n   $��Resultados del día��56      
�������$��28(�6J   $��Cascadia Code SemiBold)�6X   �� 58      $��2��6   $��0��6)   ���#23282d._�59      �  5:      �� 5;      $��* �6/   �� 5<      $��80 �6/    5>      �� 5?      $��* �6,   .+�5B      �� $��1��6!   $��0��61   
�������._�5C      �� -   5        $
 imgBotonCerrar��5C   $   $��64��6<   $��64��6G   $��Center=�6S   $��Right=�6n   $��0,0,8,0q�6�   $(��"/Recursos/img/icons/cerrar64px.png��5D        � Cursor$
 Hand��6L   ${�False��6�   5E      5F      5G      5H      5I      �� 5L   
   $��1��6   $��1��6!   $
��auto��6.   $	��Top=�6<   $��Center=�6T   
�������$
��1550��6�    �� Padding$ 20q�6�   $��20q�6�    J� Effect 5M      3  -System.Windows.Media.Effects.DropShadowEffect  5N         Color$ Black��6#      
BlurRadius$ 10Q�5O         ShadowDepth$ 5Q�5P         	Direction$	 320Q�5Q         Opacity$	 0.5Q�5R      ._�5T      ]� -   5         ]�Name$ panelContenido��5T      .+�5U      �� -   5        $	 progressBar��5U      $y�	Collapsed=�6.   $
��1550��6E   $	��200��6R   ._�5V      �  5W      �� 5X      $��* �6/    5Z      �� 5[      $��* �6,   �� 5\      $��* �6,   .+�5_      �� -   5        $ txtSimulando��5_   $   $��0��6:   $��0��6J   $��Bottom=�6W   $��Center=�6r   $��0,0,0,20q�6�   $��Simulando partidos...��5`   $   ���	#FF23282D$��16(�6X   $��Cascadia Code Light)�6f   � 5b      $��0��6&   $��1��66    � IsIndeterminate$
 True��6C   ���#1d6a7d$��1q�6p   $��15��5c   &   $	��470��62   $��0,0,0,0q�6>   $	��Top=�6O   ���#1d6a7d
�������6|   5d      5e      �� 5h      -	   5        $	 areaPartidos��5h      $��1��6/   $
��1500��6<   $
��auto��6I   ���#1d6a7d$��1q�5i   /   $y�Visible=�6C   ._�5j      � -
   5         �Name$ gridPartidos��5j      5l      5m      5n      5o      5p      5q      