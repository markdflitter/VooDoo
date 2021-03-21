using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;                               // for ObservableCollection
using System.Windows.Input;                                         // for ICommand
using System.Windows.Controls;                                      // for DataGridRow
using System.Collections.Specialized;                               // for NotifyCollectionChangedEventArgs
using System.Windows;                                               // for Point
using System.Windows.Media;                                         // for HitTestResult
using System.ComponentModel;                                        // for PropertyChangedEventArgs
using Logging;                                                      // for Log
using VooDooModel;

namespace VooDoo.ViewModel
{
    class TaskView : NotifyPropertyChanged
    {
        public TaskView(string description, string colour, string note)
        {
            Description = description;
            Colour = colour;
            Note = note;
        }

        public string Description
        {
            get {return PropertyDescription; }
            set { PropertyDescription = value;  RaisePropertyChanged("Description"); }
        }


        public string Colour
        {
            get { return PropertyColour; }
            set { PropertyColour = value; RaisePropertyChanged("Colour"); RaisePropertyChanged("Brush"); }
        }


        public string Note
        {
            get { return PropertyNote; }
            set { PropertyNote = value; RaisePropertyChanged("Note"); }
        }


        public Brush Brush
        {
            get 
            {
                switch (Colour)
                {
                    case "Black": return new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    case "Grey": return new SolidColorBrush(Color.FromRgb(128, 128, 128));
                    case "Red": return new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    case "Green": return new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    case "Blue": return new SolidColorBrush(Color.FromRgb(0, 0, 255));
                    case "Highlight": return new SolidColorBrush(Color.FromRgb(180, 180, 0));
                }

                return new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }

        //private mmeber variables
        private string PropertyDescription;
        private string PropertyColour;
        private string PropertyNote;
    }

    class DataGridViewModel : NotifyPropertyChanged
    {
        public DataGridViewModel(ITaskListGroup taskListGroup, int currentTaskList)
        {
            Log.Instance.LogInfo("Creating DataGridViewModel");

            PropertyTasks.CollectionChanged += OnViewChanged;

            TaskListGroup = taskListGroup;
            CurrentTaskList = currentTaskList;

            UpdateView();

            RaisePropertyChanged("ContextMenuItems");
        }


        public ObservableCollection<TaskView> Tasks
        {
            get { return PropertyTasks; }
            set { PropertyTasks = value; RaisePropertyChanged("Tasks"); }
        }



        // Delete Task Handling
        public ICommand DeleteTaskCommand { get { Log.Instance.LogDebug("Hooking Delete Task command"); return new CommandHandler(OnDeleteTask); } }
        private void OnDeleteTask(object parameter)
        {
            Log.Instance.LogInfo(string.Format("DataGridViewModel.OnDeleteTask {0}", parameter));

            if (parameter is DataGridRow)
            {
                int r = (parameter as DataGridRow).GetIndex();

                if (r >= 0)
                {
                    Log.Instance.LogDebug(string.Format("DataGridViewModel.OnDeleteTask - deleting task number {0}", r));
                    DeleteTask(r);
                }
            }
            else
                Log.Instance.LogError("DataGridViewModel.OnDeleteTask - parameter is not a DataGridRow");
        }

        public void DeleteTask(int taskIndex)
        {
            Log.Instance.LogDebug(string.Format("DataGridViewModel.DeleteTask - deleting task number {0}", taskIndex));
            PropertyTasks.RemoveAt(taskIndex);
            RaisePropertyChanged("Tasks");
        }


        // Note Handling
        public ICommand EditTaskNoteCommand { get { Log.Instance.LogDebug("Hooking Delete Task command"); return new CommandHandler(OnEditTaskNote); } }
        private void OnEditTaskNote(object parameter)
        {
            Log.Instance.LogInfo(string.Format("DataGridViewModel.OnEditTaskNote {0}", parameter));

            if (parameter is DataGridRow)
            {
                int r = (parameter as DataGridRow).GetIndex();

                if (r >= 0)
                {
                    Log.Instance.LogDebug(string.Format("DataGridViewModel.OnDeleteTask - editing  note for task {0}", r));

                    NotesDialog d = new NotesDialog(r, TaskListGroup.GetTaskNote(CurrentTaskList, r), OnChangeNote);
                    d.ShowDialog();

                }
            }
            else
                Log.Instance.LogError("DataGridViewModel.OnDeleteTask - parameter is not a DataGridRow");
        }


        void OnChangeNote(int index, string s)
        {
            PropertyTasks[index].Note = s;
            CopyViewChangesToModel();
            RaisePropertyChanged("Tasks");
        }

        // Add Task Handling
        public ICommand AddNewTaskCommand { get { Log.Instance.LogDebug("Hooking Add New Task command"); return new CommandHandler(OnAddNewTask); } }
        private void OnAddNewTask(object parameter)
        {
            Log.Instance.LogInfo(string.Format("DataGridViewModel.OnAddNewTask {0}", parameter));

            TextBox t = parameter as TextBox;
            if (t != null)
            {
                string s = t.Text;
                Log.Instance.LogInfo(string.Format("DataGridViewModel.OnAddNewTask {0}", s));

                t.Clear();

                if (TaskListGroup != null)
                {
                    if (s.Length > 0)
                    {
                        PropertyTasks.Insert(0, new TaskView(s, "Black", ""));
                        RaisePropertyChanged("Tasks");
                    }
                    else
                        Log.Instance.LogMsg("DataGridViewModel.OnAddNewTask - task is empty, skipping");

                }
                else
                    Log.Instance.LogDebug("DataGridViewModel.OnAddNewTask - TaskListGroup is null, skipping");
            }
            else
                Log.Instance.LogError("DataGridViewModel.OnAddNewTask - parameter is not a TextBox");

        }


        // Drag & drop Handling
        public void OnPreviewMouseLeftButtonDown(DataGrid dg, Canvas c, MouseButtonEventArgs e)
        {
            IsLeftMouseDown = true;
            IsDragging = false;

            MouseDownPosition = e.GetPosition(c);
            Log.Instance.LogInfo(string.Format("DataGridViewModel.OnPreviewMouseLeftButtonDown - Mouse pressed at ({0},{1})", MouseDownPosition.X, MouseDownPosition.Y));
        }


        public void OnPreviewMouseMove(DataGrid dg, Canvas c, MouseEventArgs e)
        {
            if (IsLeftMouseDown && !IsEditingTask)
            {
                if (!IsDragging)
                {
                    if ((System.Math.Abs(MouseDownPosition.X - e.GetPosition(c).X) > SystemParameters.MinimumHorizontalDragDistance) ||
                        (System.Math.Abs(MouseDownPosition.Y - e.GetPosition(c).Y) > SystemParameters.MinimumVerticalDragDistance))
                    {
                        int index = getIndexUnderCursor(dg, c, MouseDownPosition);
                        if (index >= 0)
                        {
                            Log.Instance.LogInfo(string.Format("Started dragging"));

                                IsDragging = true;
                                LastDragPosition = index;

                                Mouse.Capture(c);
                                dg.IsHitTestVisible = false;
                        }
                    }
                }
            }

            if (IsDragging)
            {
                int curDragPosition = getIndexUnderCursor(dg, c, e.GetPosition(c));

                if ((curDragPosition >= 0) && (curDragPosition != LastDragPosition))
                {
                    PropertyTasks.Move(LastDragPosition, curDragPosition);

                    LastDragPosition = curDragPosition;
                }
            }
        }


        public void OnPreviewMouseLeftButtonUp(DataGrid dg, Canvas c, MouseButtonEventArgs e)
        {
            Log.Instance.LogInfo("DataGridViewModel.OnPreviewMouseLeftButtonUp - Mouse released");

            IsLeftMouseDown = false;

            if (IsDragging)
            {
                Log.Instance.LogDebug("DataGridViewModel.OnPreviewMou - Stopped dragging");

                IsDragging = false;
                LastDragPosition = -1;
                dg.IsHitTestVisible = true;
                Mouse.Capture(null);
            }
        }


        public void OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            Log.Instance.LogInfo("DataGridViewModel.OnBeginningEdit");

            IsEditingTask = true;
        }

        public void OnCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Log.Instance.LogInfo("DataGridViewModel.OnCellEditEnding");

            IsEditingTask = false;
        }

        
        //private helper functions
        // copy the task list into an observable collection of list names
        private void UpdateView()
        {
            Log.Instance.LogDebug("DataGridViewModel.UpdateView");

            PropertyTasks.CollectionChanged -= OnViewChanged;

            PropertyTasks.Clear();

            if (CurrentTaskList >= 0)
            {
                for (int i = 0; i < TaskListGroup.NumTasks (CurrentTaskList); i++)
                {
                    TaskView tv = new TaskView(
                        TaskListGroup.GetTask(CurrentTaskList, i),
                        TaskListGroup.GetTaskColour(CurrentTaskList, i),
                        TaskListGroup.GetTaskNote(CurrentTaskList, i));
                    tv.PropertyChanged += OnTaskEdited;

                    PropertyTasks.Add(tv);
                }
            }

            PropertyTasks.CollectionChanged += OnViewChanged;

            RaisePropertyChanged("Tasks");
        }


        void OnViewChanged(object sender, NotifyCollectionChangedEventArgs a)
        {
            Log.Instance.LogDebug("DataGridViewModel.OnViewChanged");

            CopyViewChangesToModel();
        }


        private int getIndexUnderCursor(DataGrid dg, Canvas c, Point pos)
        {
            HitTestResult hitTestResult = VisualTreeHelper.HitTest(c, pos);
            if (hitTestResult == null) return -1;

            // iteratively traverse the visual tree
            DependencyObject dep = hitTestResult.VisualHit;
            while ((dep != null) && !(dep is DataGridRow)) dep = VisualTreeHelper.GetParent(dep);
            if (dep == null) return -1;

            DataGridRow row = dep as DataGridRow;
            return dg.ItemContainerGenerator.IndexFromContainer(row);
        }


        void OnTaskEdited(object sender, PropertyChangedEventArgs a)
        {
            Log.Instance.LogInfo("DataGridViewModel.OnTaskEdited");

            CopyViewChangesToModel();
        }


        private void CopyViewChangesToModel()
        {
            Log.Instance.LogDebug("DataGridViewModel.CopyViewChangesToModel");

            if (TaskListGroup != null)
            {
                TaskListGroup.RemoveAllTasks(CurrentTaskList);

                foreach (TaskView t in PropertyTasks)
                    TaskListGroup.AddNewTask(CurrentTaskList, t.Description, t.Colour, t.Note);
            }
            else
                Log.Instance.LogDebug("DataGridViewModel.CopyViewChangesToModel - TaskListGroup is null, skipping");
        }


        public void SetColour(int taskIndex, string colour)
        {
            Log.Instance.LogDebug(string.Format("DataGridViewModel.SetColour {0} {1}", taskIndex, colour));

            if (TaskListGroup != null)
            {
                PropertyTasks[taskIndex].Colour = colour;
                CopyViewChangesToModel();

                RaisePropertyChanged("Tasks");
            }
            else
                Log.Instance.LogDebug("DataGridViewModel.SetColour - TaskListGroup is null, skipping");
        }


        // private member variables
        ITaskListGroup TaskListGroup;        
        int CurrentTaskList;

        private ObservableCollection<TaskView> PropertyTasks = new ObservableCollection<TaskView>();

        private Point MouseDownPosition;                // where the mouse was pressed
        private bool IsEditingTask = false;             // true if editing a specific task
        private bool IsLeftMouseDown = false;           // true if left mouse button is down
        private bool IsDragging = false;                // true if dragging a task
        private int LastDragPosition = -1;              // current index of row being dragged
    }
}
