   M S B A M L   `   `   `  ����H  DVooDoo, Version=1.0.5829.25992, Culture=neutral, PublicKeyToken=null3clr-namespace:VooDoo.ViewModelVooDoo.ViewModel  5   	       VooDoo.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ,cclr-namespace:VooDoo.ViewModel     5   	   -   5        $��VooDooMainWindow��5   t   $	��350��6,   $	��525��69   $��16(�6E     � WindowStyle$  SingleBorderWindow=�6S    � Icon$" /VooDoo;component/VooDoo.jpg��6�    � Title 6	   ��  �� Path$ WindowTitle� � 	Resources 5      $  VooDoo.ViewModel.MainViewModel   mainViewModel��%   &	          5   
   6   5	       � DataContext 5
      �� 5   
    �� Source# [  6    J� InputBindings 5      �� 5   
   
 �� Key$ F4��6   	 �� 	Modifiers$		 Altf�6   
 �� Command
 6.   �� *ExitCommand+ �� CommandParameter 6N   ��  �� ElementName$ VooDooMainWindow���� 5   
   $ N��6   $	 Controlf�6   
 61   �� *
NewCommand+ 6P   �� $ VooDooMainWindow���� 5   
   $ O��6   $	 Controlf�6   
 61   �� *OpenCommand+ 6Q   �� $ VooDooMainWindow���� 5   
   $ S��6   $	 Controlf�6   
 61   �� *SaveCommand+ 6Q   �� $ VooDooMainWindow���� 5   
   $ L��6   $	 Controlf�6   
 61   �� *NewListCommand+�� 5   
   $ R��6   $	 Controlf�6   
 61   �� *RenameListCommand+.��5      �� -   5        $��Canvas��5      .+�5   
   � ���	#FFF0F0F0$��0,0,0,0q�6&   ��67   ��  �� RelativeSource �� *FindAncestor+ �� AncestorType$ CanvasL� �� AncestorLevel$ 1��$ ActualWidth���6�   ��  �� *FindAncestor+$ CanvasL�$ 1��$ ActualHeight�.+�5      o� -   5        $��menu1��5   =   $��23��6   $��Stretch=�6   $	��Top=�6J   .@�5      m� $��_File��6   .@�5      m� $
��_New��6    m� InputGestureText$ Ctrl+N��6-    m� Command 6G   �� $ 
NewCommand� m� CommandParameter 6k   �� $ VooDooMainWindow��m� 5      $��_Open...��6   $ Ctrl+O��61    6K   �� $ OpenCommand� 6p   �� $ VooDooMainWindow���� 5      m� 5      $��_Close��6    6/   �� $ CloseCommand� 6U   �� $ VooDooMainWindow���� 5       m� 5!      $��_Save��6   $ Ctrl+S��6.    6H   �� $ SaveCommand� 6m   �� $ VooDooMainWindow��m� 5"      $��Save _As...��6    64   �� $ SaveAsCommand� 6[   �� $ VooDooMainWindow���� 5#      m� 5$      $��_Exit��6   $ Alt+F4��6.    6H   �� $ ExitCommand� 6m   �� $ VooDooMainWindow��5%      m� 5&      $��
_Task List��6   .@�5'      m� $��	_New List��6   $ Ctrl+L��62    6L   �� $ NewListCommand�m� 5(      $��_Rename Current List��6   $ Ctrl+R��6=    6W   �� $ RenameListCommand�m� 5)      $��_Clear Current List��6    6<   �� $ ClearListCommand�m� 5*      $��_Delete Current List��6    6=   �� $ DeleteListCommand�5+      m� 5-      $��_Help��6   .@�5.      m� $��	_About...��6    62   �� $ AboutCommand�5/      50      �� 51      -   5        $��
separator1��51   S   $��1��6   $��Stretch=�6#   $��0,23,0,0q�6A   $	��Top=�6e   �� 52      -   5        $��	comboBox1��52   >   $��23��6   $
��Left=�6#   $	��Top=�6O   $	��104��6g   $��12(�6�   $��5,25,0,0q�6�   ��6s   �� *	TaskLists+ �� SelectedIndex 6�   �� *SelectedTaskList+6�   �� 54      -   5        $��label1��54   P   $��New Task��6   $��14(�6'   $
��Left=�65   $��
115,23,0,0q�6^   $	��Top=�6s   $��71��6�   �� 55      -   5        $��NewTaskTextBox��55      $��14(�6,   $��
184,25,5,0q�6:   $	��Top=�6N   ���	#FFF0F0F0.��6}    56      �� 57      $ Enter��6�   
 6!   �� $) #DataGridViewModel.AddNewTaskCommand� 6^   �� $ NewTaskTextBox��6�   .��58   )   59      �� 5:      $��
VooDoo.jpg��6   $��	6,55,6,19q�6(   $
��Fill=�6;    J� IsHitTestVisible .  6J   &   System.Windows.Controls.DataGrid  5<      -   5        $��taskListGrid��5<   �      AutoGenerateColumns .  $��	6,55,6,19q�6�      CanUserReorderColumns .     CanUserResizeColumns .     CanUserSortColumns .     CanUserResizeRows .    RowHeaderWidth$ 0Q�6     GridLinesVisibility$ 
Horizontal=�6)     HeadersVisibility$
 None=�6J  $��14(�6c  $��Tahoma)�6q  $��Normal"�6�     AlternatingRowBackground �	#FFC8C8C8   RowBackground �	#FFC8C8C8$��2,2,2,2q�6�  
���   ����	#FFF0F0F0|�.  $��Stretch=�6�   J� Opacity$	 0.9Q�6�   6   �� *DataGridViewModel+��6A   �� $ Tasks� 6�  �� $ VooDooMainWindow��$ DataContext.SelectedTask�  � ContextMenu  5=      �� 5>       6"   �� # [  .@�5?      m� $��Colour��6#   .@�5@      m� $
��Grey��6'   ���	#FF808081 65   �� *SetColourCommand+ 6Z   ��  �� *Self+$ Header�6�   m� 5A      $	��Red��6'   
���  �� 64   �� *SetColourCommand+ 6Y   ��  �� *Self+$ Header�6�   m� 5B      $��	Highlight��6'   ���	#FFB4B500 6:   �� *SetColourCommand+ 6_   ��  �� *Self+$ Header�6�   �� 5C      m� 5D      $��Black��6'    66   �� *SetColourCommand+ 6[   ��  �� *Self+$ Header��� 5E      m� 5F      $��Green��6'   
��� � � 66   �� *SetColourCommand+ 6[   ��  �� *Self+$ Header�6�   m� 5G      $
��Blue��6'   
����  � 65   �� *SetColourCommand+ 6Z   ��  �� *Self+$ Header�6�   5H      m� 5J      $��Move to��6#   ��64   �� $ ContextMenuItems���5K      �� 5L   "   ! �� DataType$! MenuItem��6/   .R�5M   &   m� ��6/   �� *Name+ 6G   �� *MoveCommand+ 6g   ��  �� *FindAncestor+$ DataGridL�$ 1��5N   #   5P      5Q      "   Columns" 5T      4  .System.Windows.Controls.DataGridTemplateColumn  5U      #   CellTemplate# 5V      �� 5W      .R�5X   "   �� 
$ � Tag$$  ��6)   ��60   ��  �� *FindAncestor+$ DataGridRowL���6�   ��  �� *FindAncestor+$ DataGridL�$ 1��$# DataContext.DeleteTaskCommand�.��5Y   &   �� $��
delete.gif��6,   $
��Fill=�6@   $��10��6O   $��10��6[   5Z   #   5[      5]      0  *System.Windows.Controls.DataGridTextColumn  5^      ,  &System.Windows.Controls.DataGridColumn%   Width5  /System.Windows.Controls.DataGridLengthConverter$% * 6)   1  +System.Windows.Controls.DataGridBoundColumn&   Binding& 63   �� $ Description�'   	CellStyle' 5_      �� 5`      ( �� 
TargetType*  $System.Windows.Controls.DataGridCell"(  6    .�5a   "   �� ) �� Property) � ��* �� Value* 6?   �� *Brush+5b      5d        5e      # 5f      �� 5g      .R�5h   "   �� $$  ��6)   ��60   ��  �� *FindAncestor+$ DataGridRowL���6�   ��  �� *FindAncestor+$ DataGridL�$ 1��$% DataContext.EditTaskNoteCommand�.��5i   &   �� $��dotdotdot.png��6,   $
��Fill=�6C   $��10��6R   $��10��6^   5j   #   5k      5m      +   HorizontalGridLinesBrush+ 5p      �� 5q      , �� EndPoint$, 1,0.5&�6*   - �� 
StartPoint$- 0,0.5&�6;   .��5r      � . � Color$. 	#FFE3E3E3��6'   / � Offset$/ 1Q�69   � 5s      $. White��6'   $/ 1Q�65   5t      5v      �� 5w      
�������$��Bottom=�6/   $��18��6J   .@�5x      �� 
�������$��1q�67   $��Right=�6K   .��5y      �� -	   5        $��lblNumTasks��5y       $��Right=�6R   $��87��6n   $��12(�6y   0 �� TextAlignment$0 Right=�6�   $��Center=�6�   ��63   �� $ NumTasks�6�   5z      5{      �� 5|      -
   5        $��lblDateTime��5|      $��0,4,4,0q�6Q   $��Right=�6b   $	��100��6~   $��12(�6�   $0 Right=�6�   $	��Top=�6�   ��6+   �� $ CurrentDateTime�6�   5}      5~      5      