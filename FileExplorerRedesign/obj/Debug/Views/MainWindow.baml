   M S B A M L   `   `   `  ����O  KFileExplorerRedesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:FileExplorerRedesign.Custom_Controls$FileExplorerRedesign.Custom_Controls  5   	   Q-clr-namespace:FileExplorerRedesign.PropertiesFileExplorerRedesign.Properties  5   	   G(clr-namespace:FileExplorerRedesign.ViewsFileExplorerRedesign.Views  5   	   Q-clr-namespace:FileExplorerRedesign.ViewModelsFileExplorerRedesign.ViewModels  5	   	   +    %FileExplorerRedesign.Views.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Acc2clr-namespace:FileExplorerRedesign.Custom_Controls     5   	   >prop-clr-namespace:FileExplorerRedesign.Properties     5   	   :views(clr-namespace:FileExplorerRedesign.Views     5   	   C	viewmodel-clr-namespace:FileExplorerRedesign.ViewModels     5	   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  5
   	   -   5          �Name$  
mainWindow��5   	    � Title$ File Explorer��5   	   $	��782��5   	   $
��1235��6   $	��760��5   	   $	��864��6    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency . #��[B�5   	   ���#1010104  .System.Windows.Automation.AutomationProperties   IsOffscreenBehavior$ FromClip=�5       � 	Resources 5         	checkanim �� TargetProperty �� 
TargetName<  6System.Windows.Media.Animation.EasingThicknessKeyFrame	 p� KeyTime
 p� Value  uncheckanim��%  &	        &	 a    �� 5   
   .��5      r� $ (FrameworkElement.Margin)�6/   $ ListViewControl��6e   .	�5        $	 0:0:0.2��6*   $
 0,80,7,5q�6<   5      r� 5      $ (FrameworkElement.Margin)�6/   $ navigatepagehome��6e   .	�5        $	 0��6*   $
 0q�66     5      $	 0:0:0.2��6*   $
 0,80,0,0q�6<   5      5      �� 5    
   .��5!      r� $ (FrameworkElement.Margin)�6/   $ ListViewControl��6e   .	�5"        $	 0��6*   $
 0,80,7,5q�66     5#      $	 0:0:0.4��6*   $
 0,10,7,5q�6<   5$      r� 5%      $ (FrameworkElement.Margin)�6/   $ navigatepagehome��6e   .	�5&        $	 0��6*   $
 0,80,0,0q�66     5'      $	 0:0:0.4��6*   $
 0q�6<   5(      5)      5*       � Triggers 5+      :� 5,   
    :� RoutedEvent$ ToggleButton.Checked��6    :� 
SourceName$! ToggleExpandCollapseSubMenu��6:   .[�5-      �� -   5         ��Name$! uncheckanim_BeginStoryboard��5-      #��[  6C   5.      :� 5/   
   $ ToggleButton.Unchecked��6   $! ToggleExpandCollapseSubMenu��6<   .[�50      �� -   5        $" uncheckanim_BeginStoryboard1��50      #��[ 6D   51       � DataContext 53      /   )FileExplorerRedesign.ViewModels.ViewModel  54   
   .��57      �  � RowDefinitions 58   
   �� 59      $
��111* �6   �� 5:      $
��671* �6   .+�5<   
   � $��0��6   .+�5=      � $	��Top=�6   $��46��6+    � ColumnDefinitions 5>      �� 5?      �� 5@      $
��Auto �6'   .+�5C      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�5D      $��Center=�5E      .+�5F      	� -   5         	�Name$ HomeTabButton��5F   "   $
��Home��5G   "    d� 	IsChecked$
 TrueY�5H   "     MenuHeaderRadioButtonStyle#��[ 5I   "   	� 5J      -   5        $ ComputerTabButton��5K      $��Computer��5L      #��[ 5M      	� 5N      -   5        $ ViewTabButton��5N   "   $
��View��5O   "   #��[ 5P   "   d� 5R      -   5         d�Name$! ToggleExpandCollapseSubMenu��5S      $ falseY�5T      $��10(�5U       # ExpandCollapseToggleButtonStyle#��[ 5V       � Cursor$
 Hand��6R   5W      �� 5Z      $��1��5[      $ 
Horizontal=�5\      $��Right=�5]      $��Center=�5^      .+�5_      �� -   5         ��Name$ 	SearchBox��5`      $
��12,0q�5_        SearchBoxStyle#��[ 5a       �� 
CaretBrush
 ����� �� SelectionBrush
 �����.��6p    J� InputBindings 5b      �� 5c      
 �� Key$ Escape��5d   !    �� Command 5e   !   �� *!CancelSearchFileOrFolderCommand+.��5f   1   5g      5u      5v      � 5w      $��2��6   $��Bottom=�5x      $��3,0,0,0q�5y      $��60��5z      ���	#33121212 5|      �� 5}      $��50 �6'   �� 5~      $��50 �6'   �� 5      $��50 �6'   �� 5�      $��* �6'   .+�5�      ��   Back#��[ 6   $��16(�5�      $��Center=�5�      ��5�      �� *GoToPreviousDirectoryCommand+��5�      �� 5�       �� 
TargetType" ��6      ButtonWithIconStyle �� BasedOn# [ 5�        �� Triggers 5�      �� 5�   "     �� Binding  6.   �� *	CanGoBack+! �� UpdateSourceTrigger$! PropertyChanged=�" �� Value	" False5�   .   .a�5�   &   �� # �� Property# � {�$ �� Value$ .  6B   5�   #   5�      5�      �� 5�      $��1��6     Forward#��[ 5�      $��16(�5�      $��Center=�5�      ��5�      �� *GoToForwardDirectoryCommand+��5�      �� 5�      " ��6    # [ 5�        5�      �� 5�   "     6.   �� *CanGoForward+$! PropertyChanged=�	" False5�   .   .a�5�   &   �� # � {�$ .  5�   -   5�   #   5�      5�      �� 5�      $��2��6     Up#��[ 5�      $��20(�5�      $��Center=�5�      ��5�      �� *GoToParentDirectoryCommand+��5�      �� 5�      " ��6    # [ 5�        5�      �� 5�   "     6.   �� *IsAtRootDirectory+$! PropertyChanged=�" True5�   .   .a�5�   &   �� # � {�$ .  5�   -   5�   #   5�      5�      �� 5�      -	   5        $��
AddressBar��5�      $��3��5�      $��12,11,274,11q�5�       	 AddressBarTextBoxStyle#��[	 5�      
 �����
 �������5�      �� *CurrentDirectory+$! PropertyChanged=�.��5�   t    5�      �� 5�      $ Enter��5�       5�      �� *NavigateToPathCommand+% �� CommandParameter% 5�      �� *Text+& �� ElementName$& 
AddressBar��.��5�   -   5�      5�      5�      � 5�   
   $��1��6    5�      �� 5�      $	��280 �6#   �� 5�      .+�5�      � $��0��6   .+�5�      � $��0,10,0,0q�6    5�      �� 5�      $��* �6(   �� 5�      $��95 �6(   .+�5�      �� $��0��5�      $
��25,0q�5�      $
��Auto=�5�      $��Disabled=�5�      .��5�      ��  5�       
 NavFontBrush  DropShadowEffect  NavFolderDataTemplate?   9FileExplorerRedesign.Custom_Controls.DriveAndFolderButton'  Name?   9FileExplorerRedesign.Custom_Controls.DriveAndFolderButton(   Checked)   IconFill*   Icon+   UnPinCommand��%q  '��       7 
 7  &	 �     �� 5�   "   " ��6(   .�5�   &   �� # � ��$$ Center=�6J   �� 5�   &   # � ��$
$ Bold"�6C   �� 5�   &   # � ��8$   6C   �� 5�   &   # �@J�Effect8$  6?   5�   #   �� 5�   "   .R�5�   &     $' drivefolder��5�   !   ( drivefolder_Checked5�   !   
) �����$��0,5,0,0q�5�   !   ��5�   !   �� *Name+$! PropertyChanged=�* 5�   !   �� *
FileIcon+$! PropertyChanged=���5�   !   �� *!DataContext.GetFilesListCommand+$& 
mainWindow����5�   !   �� + 5�   !   �� *(&DataContext.UnPinFavoriteFolderCommand+$& 
mainWindow��5�   !   5�   #   5�      .+�5�      �� ��5�   "   �� 5�   &   " ��6,    5�   *   �� 5�   .     5�   )   �� *FavoriteFolders.Count+$! PropertyChanged=�" 05�   )   .a�5�   2   �� # � y�$$ 	Collapsed=�5�   1   5�   /   �� 5�   .     5�   )   �� *FavoriteFolders.Count+$! PropertyChanged=�" 5�   )   Z� .a�5�   2   �� # � y�$$ 	Collapsed=�5�   1   5   /   5  '   .+�5  "   �� $��	Favorites��6,   �� 5  "   #��[ 5     $��0,0,0,17q�5     ��5     �� *FavoriteFolders+$! PropertyChanged=�5     5	     �� 5     $��Remote��6(     5     -
   5        $' btnhome��5  6   $( btnhome_Checked��5  .   $
��Home��5  -     Home#* [ 5  .   
) �����$��0,5,20,0q�5  .   ��5  .   �� + 5  .   �� *(&DataContext.UnPinFavoriteFolderCommand+$& 
mainWindow��5  .   �� 5     #��[ 6+   $��0 0 20 0q�5  +   ��5  #   �� *RemoteFolders+$! PropertyChanged=�5  +   �� 5     $��	Libraries��6(   $��0,17,0,0q�5      �� 5     #��[ 6+   $��0 0 20 0q�5  +   ��5  #   �� *LibraryFolders+$! PropertyChanged=�5  +   �� 5      $��Devices��6(   $��0,17,0,0q�5!      �� 5"     #��[ 6+   
�������$��0 0 20 0q�5$  +   ��5#  #   �� *ConnectedDevices+$! PropertyChanged=�5$  +   5%     5&     �� 5(     $��1��5)     $��0��5*     .+�5+       -   5        $' userpriv��5,     $��zKXDEX��5-       User#* [ 5.     $��	10 5 20 5q�5/     
) �����
���������52     �� * openUserProfileSettingsCommand+  53     $��Settings��54       Settings#* [ 55     $��	10 5 20 5q�56     
) �������58     �� *openSettingsCommand+59     5:     5;     � 5<     $��1��6    5=     �� 5>     $��* �6$   �� 5?     $��30 �6$   .+�5B     � -   5        , �Name$, selectionpanel��5D     $��0��5C     .+�5J     �� .+�5K     �� -   5        - ��Name$- 	SelectBox��5K  $   ���	#19FFFFFF
������ $��2��6]   . �� RadiusX$. 5��6q   5L     �� 5M     -   5        / ��Name$/ ListViewControl��5N     
������ $��0,10,7,5q�5P     $��Disabled=�5Q     $
��Auto=�5R     $��0q�5S     0 �� SelectionMode$0 Extended=�5U     1 �� SelectedItem1 5T     �� 2 �� Mode$2 OneWay=���5V     �� *NavigatedFolderFiles+$! PropertyChanged=�3 � ContextMenu3 5W     �� 5X     .@�5Y  "   m� $	��Add��5Z  %   4 � ToolTip$4 Create Folder��5[  %   5 m� Command5 5\  %   �� *)'DataContext.SubMenuFileOperationCommand+$& 
mainWindow��6 m� CommandParameter6 5]  %   �� m� 5^  "   -   5        $��Rename��5_  )   $4 Rename File��5`  )   $��Rename��5a  )   5 5b  )   �� *NavigatedFolderFiles+$! PropertyChanged=�6 5c  )   �� $2 OneWay=�m� 5d  "   $��Remove��5e  %   $4 Remove File��5f  %   5 5g  %   �� *)'DataContext.SubMenuFileOperationCommand+$& 
mainWindow��6 5h  %   �� m� 5i  "   $��
Properties��5j  %   $4 Open properties��5k  %   5 5l  %   �� *)'DataContext.SubMenuFileOperationCommand+$& 
mainWindow��6 5m  %   �� 5o     *  $Microsoft.Xaml.Behaviors.Interaction7   Triggers7 5q     +  %Microsoft.Xaml.Behaviors.EventTrigger  5r     8   	EventName$8 MouseDoubleClick��6-   *  $Microsoft.Xaml.Behaviors.TriggerBase9   Actions.9 5s  "   2	  ,Microsoft.Xaml.Behaviors.InvokeCommandAction	  : 	  Command: 5t     �� *GetFilesListCommand+; 	  CommandParameter; 5u     �� *SelectedItem+$& ListViewControl��5v       5w     $8 SelectionChanged��6-   .9 5x  "   	  : 68   �� *GetFilesSizeCommand+; 5y  0   �� *SelectedItem+$& ListViewControl��5z     5}      � 5~     -   5        <  �Name$< navigatepagehome��5~     =  � NavigationUIVisibility$= Hidden=�66   $y�Visible=�6V   >  � Source$> /Pages/pgHome.xaml?�6k   � 5     .+�5�     � $��1��6   $
��Left=�5�     $	��Top=�5�      5�     :
   4FileExplorerRedesign.Views.BoolToVisibilityConverter  BoolToVisibilityConverter��%   &	       
  5�  "   6B   5�     .+�5�     �� -   5        ? ��Name$? FileOperationsPanel��5�  +   $
��Left=�5�     $
��10,0q�5�     y�5�     �� *	IsChecked+@ �� 	Converter#@ [ $!& ToggleExpandCollapseSubMenu����5�  "   �� 5�  &   .R�5�  *   �� $ 
Horizontal=�5�  !   5�  '   ��5�  "   �� 5�  &   .R�5�  *   <   6FileExplorerRedesign.Custom_Controls.SubMenuIconButton  ���#d4cdcd  SubMenuIconButtonStyle#��[ 5�  -   $
 Hand��5�  J   A   IconA 5�  -   �� *Icon+��5�  -   �� *Name+��5�  -   �� *)'DataContext.SubMenuFileOperationCommand+$& 
mainWindow����5�  -   �� 6J   5�  '   ��5�  "   �� 5�  &   " ��6,   .�5�  *   �� # � ��$ 5�  %   �� *HomeTabSubMenuCollection+$! PropertyChanged=� 5�  *   �� 5�  .     6:   �� *	IsChecked+$& ViewTabButton��" True6s   .a�5�  2   �� # � ��$ 5�  -   �� *ViewTabSubMenuCollection+$! PropertyChanged=�5�  /   5�  '   5�     5�     5�     5�     ]� 5�     -   5        B ]�Name$B dockitemcount��5�     $��1��5�     $��25��5�     ��.   5�     .  (FileExplorerRedesign.Properties.SettingsC   DefaultD �� Source  MenuHeader.Static��%�   '��       7  �� 5�     " ��6    .�5�     �� # � ��$ �#2020206;    5�     �� 5�  "     6.   �� *
DarkMode+#D ZC " True5�  *   .a�5�  &   �� # � ��8$   5�  )   5�  #   5�     5�     .+�5�     �� $
��Left=�6!   $
��12,0q�5�     $ 
Horizontal=�5�     $��Center=�5�     .+�5�     �� �� 5�     �5�     �� *Items.Count+$& ListViewControl��$2 OneWay=�$! PropertyChanged=� �� 5�     $�items��6    �� 5�     $�|��6    �� 5�     �5�     �� *SelectedItems.Count+$& ListViewControl��$2 OneWay=�$! PropertyChanged=� �� 5�     .�6   ��5�     �� 5�  "   " ��6(   .�5�  &   �� # � �$$ items selected��6=    5�  &   �� 5�  *     5�  -   �� *SelectedItems.Count+$& ListViewControl��$2 OneWay=�$! PropertyChanged=�" 15�  -   .a�5�  .   �� # � �$$ item selected��5�  5   5�  +   �� 5�  *     5�  -   �� *SelectedItems.Count+$& ListViewControl��$2 OneWay=�$! PropertyChanged=�" 05�  -   .a�5�  .   �� # � �$$ item selected��5�  5   5�  +   5�  #   .�5�  )   5�      �� 5�     �6   �� *SelectedFolderDetails+$! PropertyChanged=�5�     5�     �� 5�     $��Right=�6!   $
��12,0q�5�     $��Center=�5�     .+�5�     �� �� 5�     $�Total Disk Space:��6    �� 5�     �6   �� *	DriveSize+$! PropertyChanged=�5�     5�     5�     5�     5�     5�     5�     