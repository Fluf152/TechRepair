   M S B A M L   `   `   `  ����  
TechRepair-    'TechRepair.Pages.ClientOrderCommentPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  Order Comments��5      $	��328��6   $	��418��6+   .��5      � $��10q�6   
������� � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ CommentsGrid��5      $��1��6)   $
��0,10q�66      AutoGenerateColumns .     
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Дата��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *	OrderId+  5      $) #Коментарий мастера��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6]    6A   �� *	Comment+6]   5      �� 5   
   -   5        $��2��5      $
��Back��6   $��Right=�6-   $	��100��5      6   5      5      