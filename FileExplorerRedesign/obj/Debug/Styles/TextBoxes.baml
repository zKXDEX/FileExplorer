   M S B A M L   `   `   `  ����O  KFileExplorerRedesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:FileExplorerRedesign.PropertiesFileExplorerRedesign.Properties  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   CommonTextBoxStyle  �� 
TargetType �� Property �� Value  SearchBox.Static-   'System.Windows.Input.KeyboardNavigation �� Triggers �� Binding.  (FileExplorerRedesign.Properties.Settings   Default �� Source �� Value  SearchBoxStyle �� BasedOn	 �� 
TargetType
 ��Name �� CornerRadius � Cursor � ColumnDefinitions �� Padding ��Name F�Name  SearchingIcon J� RenderTransformOrigin �� Stretch �� Angle :� RoutedEvent g� RepeatBehavior �� TargetProperty W� From	 W� To 
 Search �� 	IsDefault  ButtonWithIconStyle �� RelativeSource �� Mode �� AncestorType �� Triggers �� UpdateSourceTrigger �� 
TargetName  P� Property! P� Value  SearchBox.Active  AddressBarTextBoxStyle�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      6prop-clr-namespace:FileExplorerRedesign.Properties  5      %u  &	        7  7  &	 G    7   7  7  7  7  &	 �
    7   7  7  7  �� 5      "  ��5      .�5   
   ��  � ��
 ���� 6'   �� 5   
    � ��$ 1q�6,   �� 5	   
    � �� �#2020206(   �� 5
   
    � ��8   6'   �� 5   
    �@  TabNavigation$
 None=�6=   �� 5   
    � ��$ 28��6#   �� 5   
    � ��$
 Left=�67   �� 5   
    � ��$ Center=�65    5   
   �� 5       6   �� *
DarkMode+# Z  True5      .a�5      ��  � ��8  5      �� 5       � �� �#2020205      �� 5       �@��SelectionBrush
 �����5      5      5      �� 5      "  ��5      8   .�5   
   ��  � ��$	 200��6"   �� 5    
    �@�FocusVisualStyle 6-   Z� �� 5!   
    �@��PanningMode$ VerticalFirst=�65   �� 5"   
    � �� 5#      �� 5$      "	 ��6"   .R�5%      �� $
 Border��6   #��z��5&      #��z��5'      
������ $ 5��5)      $ IBeam��5*      ._�5+      �  5,      �� 5-   "   �� 5.   "   $��25 �63   �� 5/   "   $��25 �63   .+�52      �� $��PlaceHolder��6(   #��z��53   (   $��Search��54   (   $	 7,0q�55   (   #��z��56   (   $y�	Collapsed=�57   (   �� 59      $ PART_ContentHost��5;   !   $��0��5:   !   $	��5,0q�5<   !   |�.  $��Hidden=�5>   !   $��Hidden=�5?   !   F� 5A      $ SearchInProgressIndicator��6#   #���  5B   #   #��z��5C   #   $ 0.5,0.5&�5D   #   $��1��5E   #   $ Uniform=�5F   #   $��15��5G   #   $y�	Collapsed=�5H   #   z�5I   "   �� 5J   &   $ 0Q�66   ��5L   "   �� 5M   &   "  F�6,    5N   *   :� 5O   .   $ Loaded��6;   .[�5P   2   �� .��5Q   6   �� $ Forever��6A   .��5R   :   W� $4 .(Path.RenderTransform).(RotateTransform.Angle)�5S   =   $ 0X�5T   =   $	 360X�5U   =   5V   7   5W   3   5X   /   5Z   '   5\      �� 5^      $��2��5_   !   #��z��5`   !   #���  5a   !    . 8�� ��5b   !   �� *'%DataContext.SearchFileOrFolderCommand+ �� $ FindAncestor=�" ���5c   !   �� *Text+ �� *TemplatedParent+5e   !   5f      5g       5h      �� 5i      $
 True��5j   &    5i   &   �� *IsInSearchMode+$ PropertyChanged=�.a�5k      �� $ SearchInProgressIndicator��5l   !    � y�$ Visible=�5m   7   5n      P� 5p        � ��$!  ��5q   "   . �5r      �� $ PlaceHolder��6%    � y�$ Visible=�6T   5s      P� 5t        �@J�IsKeyboardFocused! . . �5v      �� 8   � ��5w   %   5x      P� 5y        � {�! .  . �5{      �� $ Border��5|   !    �@J�Opacity$	 .56Q�5~   !   5      P� 5�        �@J�IsMouseOver! . . �5�      �� 8   � ��6<   �� 5�      8   � ��6;   5�      5�      5�      5�      �� 5�      "  ��5�      8   .�5�   
   ��  � ��$ 15(�6%   �� 5�   
    � ��$ 38��6&   �� 5�   
    � ��$	 200��6%   �� 5�   
    � ��
 ���� 6'   �� 5�   
    �@��SelectionBrush �#2020206+   �� 5�   
    � �� 5�      �� 5�      "	 ��5�      .R�5�      �� #��z��6   #��z��5�      
������ $ 5��5�      ._�5�      �� $ PART_ContentHost��5�      $	��5,0q�5�      |�.  $��Hidden=�5�      $��Hidden=�5�      5�       5�      P� 5�        �@J�	IsFocused! . . �5�      ��  � ��
 �����6<   �� 5�       � ��$ 2q�6@   �� 5�       � �� �	#4C1414146;   �� 5�      8   � ��6;   5�      P� 5�        �@J�IsMouseOver! . . �5�      �� 8   � ��5�   %   �� 5�      8   � ��5�   %   5�      5�      5�      5�      5�      