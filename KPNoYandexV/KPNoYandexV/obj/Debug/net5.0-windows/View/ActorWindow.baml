   M S B A M L   `   `   `  ����F  BKPNoYandexV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:KPNoYandexV.ViewKPNoYandexV.View  5   	   "    KPNoYandexV.View.ActorWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:KPNoYandexV.View     5   	     � Title$  ActorWindow��5   	   $	��400��6   $	��550��6*   .��5	      �  � RowDefinitions 5
   
   �� 5      $��30 �6   6*   �� 5      $��2* �6   6*   �� 5      $��10 �6   6*   �� 5      $	��.7* �6   6+   �� 5      $��30 �6   6*    � ColumnDefinitions 5   
   �� 5      $
��0.1* �6   6.   �� 5      $
��0.6* �6   6.   �� 5      $��0.05* �6   6/   �� 5      $��* �6   6+   �� 5      $
��0.1* �6   6.   .+�5   
   �� $��1��6   $��1��6   ��6-   �� *Path+�� 5   
   $��1��6   $��3��6!   $��Center=�61   $	��Top=�6N   $��20��6f   $��Consolas)�6r   $��16(�6�   $	��297��6�    �� Padding$ 5,0,5,0q�6�   ��6�   �� *
FullName+6�   �� 5   
   $��1��6   $��3��6!   $��Center=�61   $	��Top=�6N   $	��297��6f   $��20��6r   $��Consolas)�6~   $��12(�6�   $
��Wrap=�6�   $ 5,0,5,0q�6�   $��0,35,0,0q�6�   ��6�   �� *	DateBirth+6�   �� 5   
   $��1��6   $��3��6!   $��Center=�61   $	��Top=�6N   $	��297��6f   $��20��6r   $��0,60,0,0q�6~   $��Consolas)�6�   $��12(�6�   $ 5,0,5,0q�6�   ��6�   �� *CurrentActor.Country+6�   �� 5   
   $��3��6   $��1��6   
���   �$
��Left=�6@   $	��Top=�6[   $��10,0,0,0q�6s   $��Consolas)�6�   $��16(�6�   .��6�   Films6�   �� 5    
   -   5        $��FilmsStackPanel��5    q   $��3��6   $��1��6"   $��3��62    �� Orientation$ 
Horizontal=�6F   $��0,29,0,0q�6_   5"      5#      5$      