   M S B A M L   `   `   `  ���� H  DVooDoo, Version=1.0.5829.30289, Culture=neutral, PublicKeyToken=null3clr-namespace:VooDoo.ViewModelVooDoo.ViewModel      VooDoo.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ,cclr-namespace:VooDoo.ViewModel     -   $��VooDooMainWindow��$	��350��$	��525��$��16(�  � WindowStyle$  SingleBorderWindow=� � Icon$" /VooDoo;component/VooDoo.jpg�� � Title ��  �� Path$ WindowTitle� � 	Resources $  VooDoo.ViewModel.MainViewModel   mainViewModel��%   &	           � DataContext ��  �� Source# [   J� InputBindings �� 
 �� Key$ F4��	 �� 	Modifiers$		 Altf�
 �� Command
 �� *ExitCommand+ �� CommandParameter ��  �� ElementName$ VooDooMainWindow���� $ N��$	 Controlf�
 �� *
NewCommand+ �� $ VooDooMainWindow���� $ O��$	 Controlf�
 �� *OpenCommand+ �� $ VooDooMainWindow���� $ S��$	 Controlf�
 �� *SaveCommand+ �� $ VooDooMainWindow���� $ L��$	 Controlf�
 �� *NewListCommand+�� $ R��$	 Controlf�
 �� *RenameListCommand+.���� -   $��Canvas��.+�� ���	#FFF0F0F0$��0,0,0,0q�����  �� RelativeSource �� *FindAncestor+ �� AncestorType$ CanvasL� �� AncestorLevel$ 1��$ ActualWidth�����  �� *FindAncestor+$ CanvasL�$ 1��$ ActualHeight�.+�o� -   $��menu1��$��23��$��Stretch=�$	��Top=�.@�m� $��_File��.@�m� $
��_New�� m� InputGestureText$ Ctrl+N�� m� Command �� $ 
NewCommand� m� CommandParameter �� $ VooDooMainWindow��m� $��_Open...��$ Ctrl+O�� �� $ OpenCommand� �� $ VooDooMainWindow���� m� $��_Close�� �� $ CloseCommand� �� $ VooDooMainWindow���� m� $��_Save��$ Ctrl+S�� �� $ SaveCommand� �� $ VooDooMainWindow��m� $��Save _As...�� �� $ SaveAsCommand� �� $ VooDooMainWindow���� m� $��_Exit��$ Alt+F4�� �� $ ExitCommand� �� $ VooDooMainWindow��m� $��
_Task List��.@�m� $��	_New List��$ Ctrl+L�� �� $ NewListCommand�m� $��_Rename Current List��$ Ctrl+R�� �� $ RenameListCommand�m� $��_Clear Current List�� �� $ ClearListCommand�m� $��_Delete Current List�� �� $ DeleteListCommand�m� $��_Help��.@�m� $��	_About...�� �� $ AboutCommand��� -   $��
separator1��$��1��$��Stretch=�$��0,23,0,0q�$	��Top=��� -   $��	comboBox1��$��23��$
��Left=�$	��Top=�$	��104��$��12(�$��5,25,0,0q����� *	TaskLists+ �� SelectedIndex �� *SelectedTaskList+�� -   $��label1��$��New Task��$��14(�$
��Left=�$��
115,23,0,0q�$	��Top=�$��71���� -   $��NewTaskTextBox��$��14(�$��
184,25,5,0q�$	��Top=����	#FFF0F0F0.�� �� $ Enter��
 �� $) #DataGridViewModel.AddNewTaskCommand� �� $ NewTaskTextBox��.���� $��
VooDoo.jpg��$��	6,55,6,19q�$
��Fill=� J� IsHitTestVisible .  &   System.Windows.Controls.DataGrid  -   $��taskListGrid��   AutoGenerateColumns .  $��	6,55,6,19q�   CanUserReorderColumns .     CanUserResizeColumns .     CanUserSortColumns .     CanUserResizeRows .    RowHeaderWidth$ 0Q�   GridLinesVisibility$ 
Horizontal=�   HeadersVisibility$
 None=�$��14(�$��Tahoma)�$��Normal"�   AlternatingRowBackground �	#FFC8C8C8   RowBackground �	#FFC8C8C8$��2,2,2,2q�
���   ����	#FFF0F0F0|�.  $��Stretch=� J� Opacity$	 0.9Q� �� *DataGridViewModel+���� $ Tasks� �� $ VooDooMainWindow��$ DataContext.SelectedTask�  � ContextMenu  ��  �� # [  .@�m� $��Colour��.@�m� $
��Grey�����	#FF808081 �� *SetColourCommand+ ��  �� *Self+$ Header�m� $	��Red��
���  �� �� *SetColourCommand+ ��  �� *Self+$ Header�m� $��	Highlight�����	#FFB4B500 �� *SetColourCommand+ ��  �� *Self+$ Header��� m� $��Black�� �� *SetColourCommand+ ��  �� *Self+$ Header��� m� $��Green��
��� � � �� *SetColourCommand+ ��  �� *Self+$ Header�m� $
��Blue��
����  � �� *SetColourCommand+ ��  �� *Self+$ Header�m� $��Move to������ $ ContextMenuItems����� ! �� DataType$! MenuItem��.R�m� ���� *Name+ �� *MoveCommand+ ��  �� *FindAncestor+$ DataGridL�$ 1��"   Columns" 4  .System.Windows.Controls.DataGridTemplateColumn  #   CellTemplate# �� .R��� 
$ � Tag$$  ������  �� *FindAncestor+$ DataGridRowL�����  �� *FindAncestor+$ DataGridL�$ 1��$# DataContext.DeleteTaskCommand�.���� $��
delete.gif��$
��Fill=�$��10��$��10��0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn%   Width5  /System.Windows.Controls.DataGridLengthConverter$% * 1  +System.Windows.Controls.DataGridBoundColumn&   Binding& �� $ Description�'   	CellStyle' �� ( �� 
TargetType*  $System.Windows.Controls.DataGridCell"(  .��� ) �� Property) � ��* �� Value* �� *Brush+  # �� .R��� $$  ������  �� *FindAncestor+$ DataGridRowL�����  �� *FindAncestor+$ DataGridL�$ 1��$% DataContext.EditTaskNoteCommand�.���� $��dotdotdot.png��$
��Fill=�$��10��$��10��+   HorizontalGridLinesBrush+ �� , �� EndPoint$, 1,0.5&�- �� 
StartPoint$- 0,0.5&�.��� . � Color$. 	#FFE3E3E3��/ � Offset$/ 1Q�� $. White��$/ 1Q��� 
�������$��Bottom=�$��18��.@��� 
�������$��1q�$��Right=�.���� -	   $��lblNumTasks��$��Right=�$��87��$��12(�0 �� TextAlignment$0 Right=�$��Center=����� $ NumTasks��� -
   $��lblDateTime��$��0,4,4,0q�$��Right=�$	��100��$��12(�$0 Right=�$	��Top=����� $ CurrentDateTime�