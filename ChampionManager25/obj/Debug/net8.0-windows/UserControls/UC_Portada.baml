   M S B A M L   `   `   `  ����L  HChampionManager25, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ChampionManager25.UserControlsChampionManager25.UserControls  5      /    )ChampionManager25.UserControls.UC_Portada   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ChampionManager25.UserControls     5      .��5      � ��5	   
   �� 5
        �� ImageSource$4  ./Recursos/img/backgroundPrincipal1920x1080.png��6    j� Stretch$ UniformToFill=�6V    j� TileMode$
 None=�6n    � ColumnDefinitions 5   
   �� 5      $��50 �6   �� 5      $	��100 �6   �� 5      $	��650 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $	��400 �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��2��6   $��1��6!   ._�5       �� $	��300��6   $	��300��6    $��Center=�6-   $
��Left=�6H   $#��/Recursos/img/logo300x300.png��5!      $��20,0,0,0q�6;   5"      �� 5%   
   -   5         ��Name$ borderLigaManagerLogo��5%      $��1��60   $��2��6@   ._�5&      �� -   5         ��Name$ imgPartidaManagerLogo��5&      $��64��63   $��64��6>   $��Center=�6J   $��Center=�6e   $#��/Recursos/img/logoManager.png��5'      5(      �� 5)   
   -   5        $ borderLigaManager��5)      $��2��6,   $��2��6<    � Cursor$
 Hand��6I   ._�5*      �� -   5        $ imgPartidaManager��5*      $	��500��6/   $��65��6;   $��Center=�6G   $
��Left=�6b   $��20,0,0,0q�6}   $&�� /Recursos/img/partidaManager.png��5+      6�   5,      �� 5/   
   -   5        $ borderLigaPromanagerLogo��5/      $��1��63   $��3��6C   ._�50      �� -   5        $ imgPartidaPromanagerLogo��50      $��64��66   $��64��6A   $��Center=�6M   $��Center=�6h   $&�� /Recursos/img/logoPromanager.png��51      52      �� 53   
   -   5        $ borderLigaProManager��53      $��2��6/   $��3��6?   $
 Hand��6L   ._�54      �� -   5        $ imgPartidaPromanager��54      $	��604��62   $��65��6>   $��Center=�6J   $
��Left=�6e   $��20,0,0,0q�6�   $)��#/Recursos/img/partidaPromanager.png��55      6�   56      �� 59   
   -	   5        $ borderCargarPartidaLogo��59      $��1��62   $��4��6B   ._�5:      �� -
   5        $ imgCargarPartidaLogo��5:      $��64��62   $��64��6=   $��Center=�6I   $��Center=�6d   $)��#/Recursos/img/logoCargarPartida.png��5;      5<      �� 5=   
   -   5        $ borderCargarPartida��5=      $��2��6.   $��4��6>   $
 Hand��6K   ._�5>      �� -   5        $ imgCargarPartida��5>      $	��506��6.   $��63��6:   $��Center=�6F   $
��Left=�6a   $��20,0,0,0q�6|   $%��/Recursos/img/cargarPartida.png��5?      6�   5@      �� 5C   
   -   5        $ borderEditorJuegoLogo��5C      $��1��60   $��5��6@   ._�5D      �� -   5        $ imgEditorJuegoLogo��5D      $��64��60   $��64��6;   $��Center=�6G   $��Center=�6b   $'��!/Recursos/img/logoEditorJuego.png��5E      5F      �� 5G   
   -   5        $ borderEditorJuego��5G      $��2��6,   $��5��6<   $
 Hand��6I   ._�5H      �� -   5        $ imgEditorJuego��5H      $	��506��6,   $��63��68   $��Center=�6D   $
��Left=�6_   $��20,0,0,0q�6z   $#��/Recursos/img/editorJuego.png��5I      6�   5J      �� 5M   
   -   5        $ borderSalirJuegoLogo��5M      $��1��6/   $��6��6?   ._�5N      �� -   5        $ imgSalirJuegoLogo��5N      $��64��6/   $��64��6:   $��Center=�6F   $��Center=�6a   $&�� /Recursos/img/logoSalirJuego.png��5O      5P      �� 5Q   
   -   5        $ borderSalirJuego��5Q      $��2��6+   $��6��6;   $
 Hand��6H   ._�5R      �� -   5        $ imgSalirJuego��5R      $	��506��6+   $��63��67   $��Center=�6C   $
��Left=�6^   $��20,0,0,0q�6y   $"��/Recursos/img/salirJuego.png��5S      6�   5T      �� 5W   
   $��3��6   $��0��6$   $��Center=�61   $��Right=�6L   $��v. 1.0.0��6h   
�������$��22(�5X   $   $��Cascadia Code SemiBold)�62   $��	0,0,150,0q�6V   �� 5[   
   -   5        $ imgReportarBug��5[      $��3��6(   $��7��68   $	��214��6E   $��33��6Q   $��Center=�6]   $��Right=�6x   $��	0,0,100,0q�6�   $
 Hand��6�   $'��!/Recursos/img/reportarErrores.png��5\      6�   5]      5^      