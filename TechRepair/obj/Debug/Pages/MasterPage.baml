   M S B A M L   `   `   `  ����  
TechRepair!    TechRepair.Pages.MasterPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$%  Страница мастера��5      $	��464��6    $	��527��6-   .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��10q�6;   .+�5      �� -   5         ��Name$ 	SearchBox��5      $	��200��6)   $��5q�65    � ToolTip$/ )Поиск по номеру заказа��5      �� 5      -   5         ��Name$ TechTypeFilter��5      $	��150��6/   $��5q�6;   5      �� 5      -   5        $��Обновить��5      $	��100��6(   $��5q�64   6?   5      �� 5   
   $��1��6   $��10q�6"   .@�5      �� $%��Доступные заказы��6   .��5      &   System.Windows.Controls.DataGrid  -   5          Name$ AvailableOrdersGrid��5         AutoGenerateColumns .     
IsReadOnly . 	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 № заказа��6-   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *Id+  5      $
 Дата создания��6-    6D   �� *	StartDate+ �� StringFormat$ 
dd.MM.yyyy��  5      $
 Модель��6-    6=   �� *TechModel.TechModelName+  5      $
 Проблема��6-    6?   �� *ProblemDescription+  5      $
 Статус��6-    6=   �� *OrderStatusType.StatusName+4  .System.Windows.Controls.DataGridTemplateColumn  5      $
 Действия��61      CellTemplate 5      �� 5   "   .R�5   &   �� -   5        $��Взять заказ��5   -   6C   5    #   5"      5$      5%      �� 5&      $��Мои заказы��6   .��5'        -   5        $ MyOrdersGrid��5'       .   . 	 5(        5)      $
 № заказа��6-    6?   �� *Id+  5*      $
 Дата создания��6-    6D   �� *	StartDate+$ 
dd.MM.yyyy��  5+      $
 Модель��6-    6=   �� *TechModel.TechModelName+  5,      $
 Проблема��6-    6?   �� *ProblemDescription+  5-      $
 Статус��6-    6=   �� *OrderStatusType.StatusName+  5.      $
 Действия��61    5/      �� 50   "   .R�51   &   �� $ 
Horizontal=�61   .+�52   *   �� -   5        $#��Изменить статус��52   1   $��2q�6f   �� 53   *   -   5        $��Комментарии��53   1   $��2q�6b   54   '   55   #   57      59      5:      5;      5<      5=      