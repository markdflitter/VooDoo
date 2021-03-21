using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;                                         // for ICommand
using System.Collections.ObjectModel;                               // for ObservableCollection
using System.Windows.Controls;                                      // for TextBox
using System.Windows.Media;                                         // for VisualTreeHelper
using System.Windows;                                               // for SizeChangedEventArgs
using Logging;                                                      // for Log
using VooDooModel;

using VooDoo.Settings;

namespace VooDoo.ViewModel
{

    class TaskListView
    {
        public TaskListView(string name) { Name = name; }
        public string Name { get; set; }
        override public string ToString() {return Name; }
    }




    // the top level view model for the main window
    class MainViewModel : NotifyPropertyChanged, IPersistCallback
    {
        private readonly string DefaultFileName = @"VooDoo";
        private readonly string DefaultFileType = @".udo";
        private readonly string DefaultFileFilter = @"VooDoo ToDo Lists (.udo)|*.udo";

        public MainViewModel() 
        {
            Log.Instance.LogInfo("Creating MainViewModel");
            LoadCurrentFile(); 
        }


        //IPersistCallback
        public void OnPersist()
        {
            new FilePersister(VooDoo.Settings.AppSettings.Instance.CurrentFile).Save(TaskListGroup);
        }



        public string WindowTitle
        {
            get { return "VooDoo - [" + CurrentFileName + "]"; }
        }


        // TaskList Selector Combo
        public ObservableCollection<TaskListView> TaskLists 
        {
            get { return PropertyTaskLists; }
            set { PropertyTaskLists = value; RaisePropertyChanged("TaskLists"); }
        }


        public int SelectedTaskList
        {
            get { return PropertySelectedTaskList; }
            set { 
                PropertySelectedTaskList = value; 
                RaisePropertyChanged("SelectedTaskList");

                RaisePropertyChanged("NumTasks");

                PropertyDataGridViewModel = new DataGridViewModel(TaskListGroup, SelectedTaskList);
                RaisePropertyChanged("DataGridViewModel");

                TaskListGroup.ActiveList = value;

                OnPersist();
            }
        }

        public int SelectedTask
        {
            get;
            set;
        }



        // File Menu
        public ICommand NewCommand { get { Log.Instance.LogDebug("Hooking New menu");  return new CommandHandler(OnNew); } }
        public ICommand OpenCommand { get { Log.Instance.LogDebug("Hooking Open menu"); return new CommandHandler(OnOpen); } }
        public ICommand CloseCommand { get { Log.Instance.LogDebug("Hooking Close menu"); return new CommandHandler(OnClose); } }
        public ICommand SaveCommand { get { Log.Instance.LogDebug("Hooking Save menu"); return new CommandHandler(OnSave); } }
        public ICommand SaveAsCommand { get { Log.Instance.LogDebug("Hooking SaveAs menu"); return new CommandHandler(OnSaveAs); } }
        public ICommand ExitCommand { get { Log.Instance.LogDebug("Hooking Exit menu"); return new CommandHandler(OnExit); } }


        private void OnNew(object parameter)
        {
            Log.Instance.LogInfo(string.Format("MainViewModel.OnNew {0}", parameter));

            // reset filename
            CurrentFileName = null;

            // create a new group
            TaskListGroup = new TaskListGroup();
            TaskListGroup.AddNewList ("Example", true);

            UpdateView(0);
        }

        private void OnOpen(object parameter)
        {
            Log.Instance.LogInfo(string.Format("MainViewModel.OnOpen {0}", parameter));

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = DefaultFileName;         // Default file name
            dlg.DefaultExt = DefaultFileType;       // Default file extension
            dlg.Filter = DefaultFileFilter;         // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                Log.Instance.LogInfo(string.Format("MainViewModel.OnOpen - loading file {0}", dlg.FileName));

                // Load document
                CurrentFileName = dlg.FileName;
                LoadCurrentFile();
            }
            else
                Log.Instance.LogInfo("MainViewModel.OnOpen - load cancelled");
        }

        private void OnClose(object parameter)
        {
            Log.Instance.LogInfo(string.Format("MainViewModel.OnClose {0}", parameter));

            TaskListGroup = null;
            CurrentFileName = null;

            UpdateView(-1);
        }



        private void OnSave(object parameter)
        {
            Log.Instance.LogInfo(string.Format("MainViewModel.OnSave {0}", parameter));

            if (CurrentFileName == null)
            {
                Log.Instance.LogDebug("MainViewModel.OnSave - no filename specified - calling SaveAs");
                OnSaveAs(parameter);
            }
            else
            {
                Log.Instance.LogDebug("MainViewModel.OnSave - filename specified, saving...");
                TaskListGroup.Persist();
            }
        }


        private void OnSaveAs(object parameter)
        {
            Log.Instance.LogInfo(string.Format("MainViewModel.OnSaveAs {0}", parameter));

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = DefaultFileName;         // Default file name
            dlg.DefaultExt = DefaultFileType;       // Default file extension
            dlg.Filter = DefaultFileFilter;         // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                Log.Instance.LogInfo(string.Format("MainViewModel.OnSaveAs Saving as {0}", dlg.FileName));

                CurrentFileName = dlg.FileName;

                //set the filename into the taskListGroup
                if (TaskListGroup != null)
                {
                    TaskListGroup.SetPersistCallback(this);
                    TaskListGroup.Persist();
                }
                else
                    Log.Instance.LogDebug("MainViewModel.OnSaveAs - TaskListGroup is null, skipping");
            }
            else
                Log.Instance.LogInfo("MainViewModel.OnSaveAs - save cancelled");

        }

        private void OnExit(object parameter)
        {
            Log.Instance.LogInfo(string.Format("MainViewModel.OnExit {0}", parameter));

            MainWindow w = parameter as MainWindow;
            if (w != null) w.Close();
        }


        // TaskList Menu
        public ICommand NewListCommand { get { Log.Instance.LogInfo("Hooking New List menu"); return new CommandHandler(OnNewList); } }
        private void OnNewList(object parameter)
        {
            Log.Instance.LogInfo(string.Format("MainViewModel.OnNewList {0}", parameter));

            if (TaskListGroup != null)
            {
                int newList = TaskListGroup.AddNewList("New List", true);
                UpdateView(newList - 1);
            }
            else
                Log.Instance.LogDebug("MainViewModel.OnNewList - TaskListGroup is null, skipping");

        }

        public ICommand RenameListCommand { get { Log.Instance.LogInfo("Hooking Rename List menu"); return new CommandHandler(OnRenameList); } }
        private void OnRenameList(object parameter)
        {
            Log.Instance.LogInfo(string.Format("MainViewModel.OnRenameList {0}", parameter));

            if (TaskListGroup != null)
            {
                RenameDialog w = new RenameDialog(TaskListGroup.ListName(SelectedTaskList), OnDoRename);
                w.ShowDialog();
            }
            else
                Log.Instance.LogDebug("MainViewModel.OnRenameList - TaskListGroup is null, skipping");

        }

        private void OnDoRename(string s)
        {
            Log.Instance.LogInfo(string.Format("MainViewModel.OnDoRename {0}", s));

            if (TaskListGroup != null)
            {
                Log.Instance.LogDebug(string.Format("Rename list {0} to {1}", SelectedTaskList, s));

                TaskListGroup.RenameList(SelectedTaskList, s);
                UpdateView(SelectedTaskList);
            }
            else
                Log.Instance.LogDebug("MainViewModel.OnDoRename - TaskListGroup is null, skipping");
        }


        public ICommand ClearListCommand { get { Log.Instance.LogInfo("Hooking Clear List menu"); return new CommandHandler(OnClearList); } }
        private void OnClearList(object parameter)
        {
            Log.Instance.LogInfo(string.Format("MainViewModel.OnClearList {0}", parameter));

            if (TaskListGroup != null)
            {
                TaskListGroup.RemoveAllTasks (SelectedTaskList);

                UpdateView(SelectedTaskList);
            }
            else
                Log.Instance.LogDebug("MainViewModel.OnClearList - TaskListGroup is null, skipping");
        }


        public ICommand DeleteListCommand { get { Log.Instance.LogInfo("Hooking Delete List menu"); return new CommandHandler(OnDeleteList); } }
        private void OnDeleteList(object parameter)
        {
            Log.Instance.LogInfo(string.Format("MainViewModel.OnDeleteList {0}", parameter));

            if (TaskListGroup != null)
            {
                int newSelection = SelectedTaskList - 1;

                TaskListGroup.DeleteList(SelectedTaskList);

                UpdateView(newSelection);
            }
            else
                Log.Instance.LogDebug("MainViewModel.OnDeleteList - TaskListGroup is null, skipping");
        }



        // Help Menu
        public ICommand AboutCommand { get { Log.Instance.LogInfo("Hooking About menu"); return new CommandHandler(OnAbout); } }
        private void OnAbout(object parameter)
        {
            Log.Instance.LogInfo(string.Format("MainViewModel.OnAbout {0}", parameter));

            AboutDialog w = new AboutDialog();
            w.ShowDialog();
        }




        //DataGridViewModel
        public DataGridViewModel DataGridViewModel
        {
            get { return PropertyDataGridViewModel; }
        }



        public void OnPreviewMouseLeftButtonDown(DataGrid dg, Canvas c, MouseButtonEventArgs e)
        {
            if (PropertyDataGridViewModel != null) PropertyDataGridViewModel.OnPreviewMouseLeftButtonDown(dg, c, e);
        }


        public void OnPreviewMouseMove(DataGrid dg, Canvas c, MouseEventArgs e)
        {
            if (PropertyDataGridViewModel != null) PropertyDataGridViewModel.OnPreviewMouseMove(dg, c, e);
        }


        public void OnPreviewMouseLeftButtonUp(DataGrid dg, Canvas c, MouseButtonEventArgs e)
        {
            if (PropertyDataGridViewModel != null) PropertyDataGridViewModel.OnPreviewMouseLeftButtonUp(dg, c, e);
        }

        public void OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (PropertyDataGridViewModel != null) PropertyDataGridViewModel.OnBeginningEdit(sender, e);
        }

        public void OnCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (PropertyDataGridViewModel != null) PropertyDataGridViewModel.OnCellEditEnding(sender, e);
        }

        public void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            VooDoo.Settings.AppSettings.Instance.Size = e.NewSize;
        }


        // status bar handling
        public string CurrentDateTime
        {
            get
            {
                DateTime dt = DateTime.Now;

                return String.Format("{0:D2}/{1:D2}/{2:D2}", dt.Day, dt.Month, dt.Year);
            }
        }

        public string NumTasks
        {
            get
            {
                int numTasks = 0;

                if ((TaskListGroup != null) && (SelectedTaskList >= 0))
                    numTasks = TaskListGroup.NumTasks(SelectedTaskList);
                
                if (numTasks == 1)
                    return numTasks.ToString () + " task";
                else
                    return numTasks.ToString () + " tasks";
            }
        }


        // Context Menu
        public ObservableCollection<ContextMenuItem> ContextMenuItems
        {
            get { return PropertyContextMenuItems; }
            set { PropertyContextMenuItems = value; RaisePropertyChanged("ContextMenuItems"); }
        }


        private void OnMoveItem(int targetListIndex, object item)
        {
            Log.Instance.LogInfo (string.Format("MainViewModel.OnMoveItem {0} {1}", targetListIndex, item));

            if ((TaskListGroup != null) && (SelectedTaskList < TaskListGroup.NumLists))
            {
                if (item is DataGrid)
                {
                    List<TaskView> tasksToRemove = new List<TaskView>();

                    DataGrid dg = item as DataGrid;
                    
                    foreach (TaskView t in dg.SelectedItems)
                    {
                        Log.Instance.LogInfo(string.Format("MainViewModel.OnMoveItem - move {0} from {1} to {2}", t.Description, SelectedTaskList, targetListIndex));
                        TaskListGroup.AddNewTask(targetListIndex, t.Description, t.Colour, t.Note);
                        tasksToRemove.Add(t);
                    }

                    foreach (TaskView t in tasksToRemove)
                    {
                        PropertyDataGridViewModel.DeleteTask(dg.Items.IndexOf(t));
                    }
                }
                else
                    Log.Instance.LogError("MainViewModel.OnMoveItem - item is not a DataGrid, skipping");
            }
            else
                Log.Instance.LogDebug("MainViewModel.OnMoveItem - TaskListGroup is null, skipping");
        }


        public ICommand SetColourCommand { get { Log.Instance.LogInfo("Hooking SetColour menu"); return new CommandHandler(OnSetColour); } }
        private void OnSetColour(object parameter)
        {
            Log.Instance.LogInfo(string.Format("MainViewModel.OnSetColour {0}", parameter));

            if (TaskListGroup != null)
            {
                    PropertyDataGridViewModel.SetColour(SelectedTask, parameter as string);
             }
            else
                Log.Instance.LogDebug("MainViewModel.OnSetColour - TaskListGroup is null, skipping");
        }





        // private helper functions
        private string CurrentFileName
        {
            get { return Settings.AppSettings.Instance.CurrentFile; }
            set { Settings.AppSettings.Instance.CurrentFile = value; RaisePropertyChanged("WindowTitle"); }
        }



        // copy the task list into an observable collection of list names
        private void UpdateView(int newSelection = 0)
        {
            Log.Instance.LogDebug(string.Format("MainViewModel.UpdateView {0}", newSelection));

            TaskLists.Clear();
            ContextMenuItems.Clear();

            if (TaskListGroup != null)
            {
                for (int i = 0; i < TaskListGroup.NumLists; i++)
                {
                    Log.Instance.LogDebug(string.Format("MainViewModel.Adding Task List {0}", TaskListGroup.ListName(i)));

                    TaskLists.Add(new TaskListView(TaskListGroup.ListName(i)));
                    ContextMenuItems.Add(new ContextMenuItem(TaskListGroup.ListName(i), new CustomCommandHandler(i, OnMoveItem)));
                }
            }
            else
                Log.Instance.LogDebug(string.Format("MainViewModel.UpdateView - TaskListGroup is null, skipping"));

            RaisePropertyChanged("TaskLists");
            RaisePropertyChanged("ContextMenuItems");

            SelectedTaskList = newSelection;

            PropertyDataGridViewModel = new DataGridViewModel(TaskListGroup, SelectedTaskList);
        }


        private void LoadCurrentFile()
        {
            Log.Instance.LogDebug(string.Format("MainViewModel.LoadCurrentFile"));

            if (VooDoo.Settings.AppSettings.Instance.CurrentFile != null)
            {
                Log.Instance.LogDebug(string.Format("MainViewModel.LoadCurrentFile - loading file {0}", VooDoo.Settings.AppSettings.Instance.CurrentFile));

                TaskListGroup = new FilePersister(VooDoo.Settings.AppSettings.Instance.CurrentFile).Load();

                if (TaskListGroup != null)
                {
                    TaskListGroup.SetPersistCallback(this);

                    if (TaskListGroup.NumLists == 0)
                        UpdateView(-1);
                    else
                        UpdateView(TaskListGroup.ActiveList);
                }


                // one off update
                //VooDooModel.TaskListGroup tlg = new VooDooModel.TaskListGroup();
                //for (int i = 0; i < TaskListGroup.NumLists; i++)
                //{
                //    tlg.AddNewList(TaskListGroup.ListName(i), false);
                //    for (int j = 0; j < TaskListGroup.NumTasks(i); j++)
                 //   {
                 //       tlg.AddNewTask(i, TaskListGroup.GetTask(i,j), TaskListGroup.GetTaskColour(i,j), TaskListGroup.GetTaskNote(i,j));
                   // }

//                }
                //new FilePersister(VooDoo.Settings.AppSettings.Instance.CurrentFile + ".new").Save(tlg);
            }
        }
        

        // private member variables
        private ITaskListGroup TaskListGroup = null;

        private DataGridViewModel PropertyDataGridViewModel;
        
        private ObservableCollection<TaskListView> PropertyTaskLists = new ObservableCollection<TaskListView> ();
        private int PropertySelectedTaskList = -1;

        private ObservableCollection<ContextMenuItem> PropertyContextMenuItems = new ObservableCollection<ContextMenuItem>();
    }
}