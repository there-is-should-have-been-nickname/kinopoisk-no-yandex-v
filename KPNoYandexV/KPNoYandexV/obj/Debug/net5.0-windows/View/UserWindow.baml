   M S B A M L   `   `   `  ����F  BKPNoYandexV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:KPNoYandexVKPNoYandexV  5   	   !    KPNoYandexV.View.UserWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:KPNoYandexV     5   	     � Title$  
UserWindow��5   	   $	��600��6   $	��900��6)   .��5	      �  � ShowGridLines$ False��6    � RowDefinitions 5
   
   �� 5      $��90 �6   6*   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $��* �6   6+   .+�5   
   � $ False��6   $��0��6%   $��0��62   #��� ��6B    5      �� 5      $��* �6    6-    5      �� 5      $
��1.5* �6#   62   �� 5      $��* �6#   6/   �� 5      $��* �6#   6/   �� 5      $��* �6#   6/   .+�5      �� $��0��6   $��0��6!   $��Uniform=�61    �� StretchDirection$
 Both=�6C   ${�False��6[    � Cursor$ Arrow��6m   $��../Images/logo.png��6|   �� 5       $��0��6   $��1��6"   $��35��62   $��Center=�6>   $��Consolas)�6Y   $��18(�6o   $	��140��6�   ���	#FFCDCDCD
���   ����	#FF9C9C9C$��Center=�6�   $��Italic$�6  $
 Hand��6  ��6}   �� *
FilmsClick+.��6(  Films6/  �� 5!      $��0��6   $��2��6"   $��35��62   $��Center=�6>   $��Consolas)�6Y   $��18(�6o   $	��140��6�   ���	#FFCDCDCD
���   ����	#FF9C9C9C$��Center=�6�   $��Italic$�6  $
 Hand��6  ��6}   �� *ActorsClick+.��6)  Actors61  �� 5"      $��0��6   $��3��6"   $��35��62   $��Center=�6>   $��Consolas)�6Y   $��18(�6o   $	��140��6�   ���	#FFCDCDCD
���   ����	#FF9C9C9C$��Center=�6�   $��Italic$�6  $
 Hand��6  ��6}   �� *	BackClick+.��6'  Back6-  5$       � 5'   
   $��1��6     � NavigationUIVisibility$ Hidden=�6   ��6=   �� *CurrentPage+5(      5)      