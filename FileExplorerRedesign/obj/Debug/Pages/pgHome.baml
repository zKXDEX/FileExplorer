   M S B A M L   `   `   `  ����O  KFileExplorerRedesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:FileExplorerRedesign.PagesFileExplorerRedesign.Pages  5      [2clr-namespace:FileExplorerRedesign.Custom_Controls$FileExplorerRedesign.Custom_Controls  5      Q-clr-namespace:FileExplorerRedesign.PropertiesFileExplorerRedesign.Properties  5      G(clr-namespace:FileExplorerRedesign.ViewsFileExplorerRedesign.Views  5	      Q-clr-namespace:FileExplorerRedesign.ViewModelsFileExplorerRedesign.ViewModels  5
      '    !FileExplorerRedesign.Pages.pgHome   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:FileExplorerRedesign.Pages     5      Acc2clr-namespace:FileExplorerRedesign.Custom_Controls     5      >prop-clr-namespace:FileExplorerRedesign.Properties     5      :views(clr-namespace:FileExplorerRedesign.Views     5	      C	viewmodel-clr-namespace:FileExplorerRedesign.ViewModels     5
      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  5        N� Title$  pgHome��5      .��5      �  � RowDefinitions 5   
   �� 5      $	��165 �6   �� 5      $
��427* �6   .+�5   
   � $	��166��6   $��Center=�6    � ColumnDefinitions 5      �� 5      �� 5      �� 5      �� 5      .+�5      � .+�5      �� $��Folders��6   $
��Left=�6+   $	��Top=�6F   $��20 15q�6^   
�������$��15(�6�   $
��Bold"�6�   �� 5      -   5         ��Name$ btndownload��5!      ���	#33121212 �� CornerRadius$ 7��5#       � ToolTip$ 	Downloads��5$      $��
20 0 20 20q�5%      $	��100��5&      $��Bottom=�5'      ._�5(      � $��0,0,4,0q�6    5)      �� 5*      $	��13* �6,   �� 5+      $��7* �6,   .+�5-      � $��0��6   .+�5.      F�    DownloadsFolder#��[  6#   ���	#FF2779FF$��30��6\    �� Stretch$ Uniform=�6h   5/      � 50      $��1��6   .+�51      �� $��0��6(   $��Center=�52   %   $	��Top=�6B   $
��10 0q�53   %   $��	Downloads��63   
�������$
��Bold"�54   %   $��15(�67   $��WordEllipsis=�6E   $
��Wrap=�6a   55      56      58      59      �� 5<      $��1��6   ���	#33121212$ 7��5>      $��
20 0 20 20q�5?      $ Images��5@      $��Bottom=�5A      $	��100��5B      ._�5C      � $��0,0,4,0q�6    5D      �� 5E      $	��13* �6(   �� 5F      $��7* �6(   .+�5H      � $��0��6   .+�5I      ��   	pctimages#��[ 6    $��30��6D   $��Uniform=�6P   5J      � 5K      $��1��6   .+�5L      �� $��0��6$   $��Center=�5M   %   $	��Top=�6B   $
��10 0q�5N   %   $��Images��63   
�������$
��Bold"�5O   %   $��15(�67   $��WordEllipsis=�6E   $
��Wrap=�6a   5P      5Q      5S      �� 5U      $��2��6   ���	#33121212$ 7��5W      $��
20 0 20 20q�5X      $ Music��5Y      $��Bottom=�5Z      $	��100��5[      ._�5\      � $��0,0,4,0q�6    5]      �� 5^      $	��13* �6(   �� 5_      $��7* �6(   .+�5a      � $��0��6   .+�5b      ��   icomusic#��[ 6    $��30��6C   $��Uniform=�6O   5c      � 5d      $��1��6   .+�5e      �� $��0��6$   $��Center=�5f   %   $	��Top=�6B   $
��10 0q�5g   %   $��Music��63   
�������$
��Bold"�5h   %   $��15(�67   $��WordEllipsis=�6E   $
��Wrap=�6a   5i      5j      5l      �� 5n      $��3��6   ���	#33121212$ 7��5p      $��
20 0 20 20q�5q      $ Videos��5r      $��Bottom=�5s      $	��100��5t      ._�5u      � $��0,0,4,0q�6    5v      �� 5w      $	��13* �6(   �� 5x      $��7* �6(   .+�5z      � $��0��6   .+�5{      ��   	pathvideo#��[ 6    $��30��6D   $��Uniform=�6P   5|      � 5}      $��1��6   .+�5~      �� $��0��6$   $��Center=�5   %   $	��Top=�6B   $
��10 0q�5�   %   $��Video��63   
�������$
��Bold"�5�   %   $��15(�67   $��WordEllipsis=�6E   $
��Wrap=�6a   5�      5�      5�      5�      � 5�   
   $��1��6   .+�5�      �� $��Recent Files��6   $
��Left=�6,   $	��Top=�6G   $��20 15q�6_   
�������$��15(�6�   $
��Bold"�6�   � 5�      $��0,40,0,0q�6   .+�5�      �� $��No Recent Files��6   
�������$��Center=�6E   $��Center=�6b   $
��Bold"�6}   $��18(�6�   5�      5�      5�      5�      