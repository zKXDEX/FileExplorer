   M S B A M L   `   `   `  ����O  KFileExplorerRedesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:FileExplorerRedesign.ViewsFileExplorerRedesign.Views  5   	   -    'FileExplorerRedesign.Views.RenameDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:FileExplorerRedesign.Views     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  5   	   -   5        $��renameDialog��5   	     � Title$  RenameDialog��5	   	   $��80��6   $	��270��6*    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterOwner=�5   	    � AllowsTransparency . 
������ .��5      �� $��0q�5   	   
���   �._�5   
   �  � RowDefinitions 5      �� 5      $��32 �6    �� 5      .+�5      � ���	#66191919 J� Opacity$ .3Q�5      *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5      +  %Microsoft.Xaml.Behaviors.EventTrigger  5         	EventName$ 	MouseDown��6%   *  $Microsoft.Xaml.Behaviors.TriggerBase	   Actions.	 5      4  .Microsoft.Xaml.Behaviors.Core.CallMethodAction  
   
MethodName$
 DragMove��6-      TargetObject 5   -   ��  �� ElementName$ renameDialog��5      5!      �� 5"      $
��Left=�5$      $��Center=�5%      
�������$
��10,0q�5'      ��5#      �� *OldFolderName+$ renameDialog��5'      �� 5)      -   5         ��Name$ CloseButton��5)      $	����5*         Segoe MDL2 Assets#��[  5+      $��11(�5,      $��Right=�5-       �� IsCancel .   WindowChromeCloseButton#��[ 5/      $��0,0,0,10q�6F    50        51      $ Click��6%   .	 52        $
 Close��53       54      �� $ renameDialog��55      57      �� 59      $��1��5:      ���	#66191919$ .3Q�5<      � 5>      $��1��5?      $��10q�5@       � ColumnDefinitions 5A      �� 5B      �� 5C      $
��Auto �6'   .+�5F      �� -   5         ��Name$ TextBox��5H      
������ $��1q�5I      
����������	#FF2D2D2D$��Center=�5M       �� SelectionBrush
 ����� �� 
CaretBrush
 ����� J� OpacityMask
 �������5L      �� *NewFolderName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5M   a   �� 5O      $��1��6     Forward#��[ 5P      $��25��5Q      $��25��5R       �� 	IsDefault .   ButtonWithIconStyle#��[ 5T      
������� � Cursor$
 Hand��6Y    5U        5V      $ Click��6)   .	 5W        $
 Close��61    5X   1   �� $ renameDialog��5Y      5[      5\      5]      5^      5_      