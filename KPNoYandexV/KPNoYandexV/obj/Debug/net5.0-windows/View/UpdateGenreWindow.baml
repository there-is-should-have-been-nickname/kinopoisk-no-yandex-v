   M S B A M L   `   `   `  ����F  BKPNoYandexV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:KPNoYandexV.ViewKPNoYandexV.View  5   	   (    "KPNoYandexV.View.UpdateGenreWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:KPNoYandexV.View     5   	     � Title$  UpdateGenreWindow��5   	   $	��450��6#   $	��800��60   .��5	      �  � RowDefinitions 5
   
   �� 5      $��30 �6   6*   �� 5      $
��0.7* �6   6,   �� 5      $��* �6   6)   �� 5      $
��1.6* �6   6,   �� 5      $��* �6   6)   �� 5      $��* �6   6)   �� 5      $
��0.7* �6   6,   �� 5      $��30 �6   6*    � ColumnDefinitions 5   
   �� 5      $��30 �6   6-   �� 5      $
��0.4* �6   6.   �� 5      $��* �6   6+   �� 5      $
��0.4* �6   6.   �� 5      $��30 �6   6,   .+�5   
   �� $��1��6   $��2��6   $#��Изменение жанра��6-   $��Center=�6G   $��Consolas)�6d   $��25(�6z   $��40��6�   $��Center=�6�   $	��411��6�   $��Center=�6�   �� 5   
   $��2��6   $��1��6   $��Название��6-   $
��Left=�6@   $��	164,4,0,0q�6[   $	��Top=�6n   $��25��6�   $	��139��6�   $��Consolas)�6�   $��14(�6�   $��2��6�   �� 5    
   $��2��6   $��1��6   $
��Left=�6/   $��
164,33,0,0q�6J    �� TextWrapping$
 Wrap=�6^   $	��Top=�6r   $	��200��6�   $��2��6�   $��25��6�   $��Consolas)�6�   $��16(�6�   $
��Left=�6�   $��Center=�6�   ��6  �� *	GenreName+ �� Mode$ TwoWay=��� 5"   
   $��2��6   $��6��6!   $��Consolas)�6.   $��16(�6D   $	��150��6R   $��30��6^   $��Center=�6j   $	��Top=�6�   $��50,4,0,0q�6�    � Cursor$
 Hand��6�   ��6�   �� *UpdateGenreClick+.��6�   Изменить6�   �� 5$   
   $��2��6   $��1��6   $��2��6/    �� SelectionMode$ Multiple=�6@   $	��130��6w   ��6Y   �� *Films+6w   �� 5%   
   $��5��6   $��2��6!   $��1��62   $��0,1,0,0q�6B   $��Consolas)�6S   $��14(�6i   $
��Wrap=�6w   ${�False��6�   ��6�   �� *ChosenFilmsNames+�� 5&   
   $��1��6   $��1��6!   $��Consolas)�6.   $��16(�6D   $	��100��6R   $��30��6^   $��Center=�6j   $��Center=�6�   $
 Hand��6�   ��6�   �� *	BackClick+.��6�   
Назад6�   5'      5(      