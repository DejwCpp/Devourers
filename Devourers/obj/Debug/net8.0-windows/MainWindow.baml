   M S B A M L   `   `   `  ����D  @Devourers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Devourers	Devourers  5   	       Devourers.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Devourers     5   	   ���#222831  � WindowState$  	Maximized=�5	   	    � WindowStyle$ 
ToolWindow=�5
   	    � 
ResizeMode$ NoResize=�5   	    � Title$ 	Devourers��5   	   $
��1080��6   $
��1920��6)    � 	Resources 5         NoHoverButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType  NoHoverButtonStyleForColors	 �� CornerRadius��%@  &	        &	     '�� _    �� 5   
   " ��6+   .�5      ��  � ��
 ���� 6+   �� 5       � ��
 ���� 6,   �� 5       � ��$ 0q�60   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��Center=�6+   $��Center=�6H   5      5      5      �� 5   
   " ��64   .�5      ��  � ��
 ���� 6,   �� 5       � ��$ 0q�60   �� 5       � �� 5       �� 5!      " ��6&   .R�5"      �� #��z��6!   #��z��5#      #��z��5$      $	 5��5%      ._�5&      �� $��Center=�6/   $��Center=�5'   '   5(      5)      5+      5,      �� 5/   
   " ��6   .�50      ��  � ��$9 3pack://application:,,,/Assets/Fonts/#Ubuntu Regular)�6+   �� 51       � ��$ 20(�6)   �� 52       � ��
 �����6+   53      54      .��57      � -   5        $��MainGrid��57      .+�59   
   � -   5        $��
MenuScreen��59      $y�Visible=�6!   
 � RowDefinitions
 5:      �� 5;      $��7* �6    �� 5<      $��* �6    �� 5=      $��2* �6     � ColumnDefinitions 5?      �� 5@      $��* �6#   �� 5A      $��* �6#   .+�5D      �� $��0��6   $��0��6&   $��Stretch=�66   $��Center=�6T   $��10,10,10,10q�6o   .+�5E      �� -   5        $��PlayerOneLabel��5E      $��
Player one��6.   $��90(�6C   $��Center=�6Q   
�������6n   �� 5F      $��Select your color:��6   $��20(�65   $��Center=�6C   
�������$��0,40,0,0q�6t   A� 5G      -   5        $��PlayerOneButtonContainer��5G       A� Columns$ 4��6>    A� Rows$ 2��6J   $��Center=�6S   $	��Top=�6p   $��10q�6�   .+�5H      �� $��90��6   $��90��6(   $��10q�64   #��[ 6@   ���#ef233c6u   �� 5I      $��90��6   $��90��6(   $��10q�64   #��[ 6@   ���#00a8e86u   �� 5J      $��90��6   $��90��6(   $��10q�64   #��[ 6@   ���#ffd5006u   �� 5K      $��90��6   $��90��6(   $��10q�64   #��[ 6@   ���#9d4edd6u   �� 5L      $��90��6   $��90��6(   $��10q�64   #��[ 6@   ���#70e0006u   �� 5M      $��90��6   $��90��6(   $��10q�64   #��[ 6@   ���#bc6c256u   �� 5N      $��90��6   $��90��6(   $��10q�64   #��[ 6@   ���#fb6f926u   �� 5O      $��90��6   $��90��6(   $��10q�64   #��[ 6@   ���#fefae06u   5P      5Q      �� 5S      $��0��6   $��1��6&   $��Stretch=�66   $��Center=�6T   $��10,10,10,10q�6o   .+�5T      �� -   5        $��PlayerTwoLabel��5T      $��
Player two��6.   $��90(�6C   $��Center=�6Q   
�������6n   �� 5U      $��Select your color:��6   $��20(�65   $��Center=�6C   
�������$��0,40,0,0q�6t   A� 5V      -   5        $��PlayerTwoButtonContainer��5V      $ 4��6>   $ 2��6J   $��Center=�6S   $	��Top=�6p   $��10q�6�   .+�5W      �� $��90��6   $��90��6(   $��10q�64   #��[ 6@   ���#ef233c6w   �� 5X      $��90��6   $��90��6(   $��10q�64   #��[ 6@   ���#00a8e86u   �� 5Y      $��90��6   $��90��6(   $��10q�64   #��[ 6@   ���#ffd5006u   �� 5Z      $��90��6   $��90��6(   $��10q�64   #��[ 6@   ���#9d4edd6u   �� 5[      $��90��6   $��90��6(   $��10q�64   #��[ 6@   ���#70e0006u   �� 5\      $��90��6   $��90��6(   $��10q�64   #��[ 6@   ���#bc6c256u   �� 5]      $��90��6   $��90��6(   $��10q�64   #��[ 6@   ���#fb6f926u   �� 5^      $��90��6   $��90��6(   $��10q�64   #��[ 6@   ���#fefae06u   5_      5`      �� 5b      -   5        $��SelectBoardSizeSection��5b      $��1��67   $��2��6D   $��Center=�6X   $��Center=�6u   $��20q�6�   $y�	Collapsed=�6�   .+�5c      �� $��Select map size:��6   
�������$��30(�6F   5d      �� 5e      -   5        $��BoardSizeButtonsSection��5e      $��2��68   $��2��6E   $��Center=�6Y   $��Center=�6v    �� Orientation$ 
Horizontal=�6�   $��
20,0,20,40q�6�   $y�	Collapsed=�6�   .+�5f      �� -	   5        
 � Tag$ 3��5f   <    � Cursor$
 Hand��6D   $��0,0,0,0q�6R   $	��250��6c   $��90��6o   $��0q�6{   #��[  6�   ��5g      �� 5h      $6��0pack://application:,,,/Assets/Img/mapSize3x3.png��6    $��UniformToFill=�6Z   5j      �� 5k      -
   5        $ 4��5k   <   $
 Hand��6D   $��0,0,0,0q�6R   $	��250��6c   $��90��6o   $��0q�6{   #��[  6�   ��5l      �� 5m      $6��0pack://application:,,,/Assets/Img/mapSize4x4.png��6    $��UniformToFill=�6Z   5o      �� 5p      -   5        $ 5��5p   <   $
 Hand��6D   $��0,0,0,0q�6R   $	��250��6c   $��90��6o   $��0q�6{   #��[  6�   ��5q      �� 5r      $6��0pack://application:,,,/Assets/Img/mapSize5x5.png��6    $��UniformToFill=�6Z   5t      5u      5v      � 5x   
   -   5        $��
GameScreen��5x      $y�	Collapsed=�6!   
�������
 5y      �� 5z      $��* �6    �� 5{      $��3* �6    �� 5|      $��* �6     5~      �� 5      $��* �6#   �� 5�      $��2* �6#   �� 5�      $��* �6#   5�      5�      5�      