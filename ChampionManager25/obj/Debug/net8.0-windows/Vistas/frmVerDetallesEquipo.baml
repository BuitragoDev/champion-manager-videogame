   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ChampionManager25.VistasChampionManager25.Vistas  5   	   3    -ChampionManager25.Vistas.frmVerDetallesEquipo   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:ChampionManager25.Vistas     5   	   -   5          �Name$  VerDetallesWindow��5      $
��1080��65   $
��1920��6C    � Title$ frmVerDetallesEquipo��5   	    � 
ResizeMode$ NoResize=�6&    � WindowStyle$
 None=�6<    � WindowState$ 	Maximized=�6O   .��5	      � ��5   
   �� 5       �� ImageSource$4 ./Recursos/img/backgroundPrincipal1920x1080.png��6    j� Stretch$ UniformToFill=�6V    j� TileMode$
 None=�6n    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��6* �6   �� 5      $��* �6   	 � RowDefinitions	 5   
   �� 5      $��80 �6   �� 5      $��50 �6   �� 5      $��* �6   �� 5      $��80 �6   .+�5   
   �� -   5        
 ��Name$
 borderBarraSuperior��5      $��0��6.   $��0��6>   $��3��6K   ���#23282d._�5      �  5      �� 5      $��* �6'   �� 5       $��3* �6'   �� 5!      $��* �6'   	 5#      �� 5$      $��* �6$   .+�5'      �� $��0��6   $��0��6)   ���#23282d._�5(      �  5)      �� 5*      $��80 �6/   �� 5+      $��* �6/   	 5-      �� 5.      $��* �6,   .+�51      �� $��0��6!   $��0��61   
�������._�52      �� -   5         ��Name$ imgEscudoEquipo��52   $   $��64��6=   $��64��6H   $��Uniform=�6T   $��Center=�6f   $��Center=�6�   53      54      55      �� 57      -   5         ��Name$ lblTituloVentana��57      $��1��66   $��0��6F   $��Center=�6S   $��Center=�6n   $��Equipo��58      
�������$��28(�6B   $��Cascadia Code SemiBold)�6P   �� 5:      $��2��6   $��0��6)   ���#23282d._�5;      �  5<      �� 5=      $��* �6/   �� 5>      $��80 �6/   	 5@      �� 5A      $��* �6,   .+�5D      �� $��1��6!   $��0��61   
�������._�5E      �� -   5        $ imgBotonCerrar��5E   $   $��64��6<   $��64��6G   $��Center=�6S   $��Right=�6n   $��0,0,8,0q�6�   $(��"/Recursos/img/icons/cerrar64px.png��5F   $    � Cursor$
 Hand��6P   6^   5G      5H      5I      5J      5K      �� 5N   
   -   5        $
 panelMenuOpciones��5N      $��0��6,   $��1��6<   $��3��6I   $��50��6]   $��Center=�6i   
�������._�5O      �  5P      �� 5Q      $��* �6'   �� 5R      $��* �6'   �� 5S      $��* �6'   	 5U      �� 5V      $��* �6$   .+�5X      �� -   5        $ lblInformacion��5X      $��Información Equipo��64   $��0��6N   $��0��6^   
���   �$��Cascadia Code SemiBold)�5Y      $��16(�6@   $��Center=�6N   $��Center=�6i   $
 Hand��6�   6�   �� 5[      -   5        $ lblPlantilla��5[      $��	Plantilla��62   $��1��6C   $��0��6S   
���   �$��Cascadia Code SemiBold)�5\      $��16(�6@   $��Center=�6N   $��Center=�6i   $
 Hand��6�   6�   �� 5^      -	   5        $ lblPalmares��5^      $��	Palmarés��61   $��2��6A   $��0��6Q   
���   �$��Cascadia Code SemiBold)�5_      $��16(�6@   $��Center=�6N   $��Center=�6i   $
 Hand��6�   6�   5`      5a      �� 5d   
   $��1��6   $��2��6!   $
��1440��6.   $	��800��6;   $��Center=�6H   
������� J� Effect 5e      3  -System.Windows.Media.Effects.DropShadowEffect  5f         Color$ Black��6#      
BlurRadius$ 10Q�5g         ShadowDepth$ 5Q�5h         	Direction$	 320Q�5i         Opacity$	 0.5Q�5j      ._�5l      ]� -
   5         ]�Name$ panelContenido��5l      62   5m      5n      5o      