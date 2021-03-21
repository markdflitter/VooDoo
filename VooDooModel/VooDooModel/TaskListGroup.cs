using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;                               // for IEnumerable
using System.IO;                                                    // for stream writer
using System.Runtime.Serialization.Formatters.Binary;               // for BinaryFormatter
using Logging;                                                       // for Log


namespace VooDooModel
{
    // a single list of tasks
    [Serializable()]
    public class TaskListGroup : ITaskListGroup
    {
        // ITaskListGroup
        public void SetPersistCallback(IPersistCallback p)
        {
            PersistCallback = p;
        }

        private IPersistCallback PersistCallback
        {
            get { return PropertyPersistCallback; }
            set { PropertyPersistCallback = value; }
        }

        public int AddNewList(string name, bool populateAsExample)
        {
            Log.Instance.LogDebug(string.Format("TaskListGroup.AddNewList {0} {1}", name, populateAsExample));

            TaskList newList = new TaskList(name);
            TaskLists.Add(newList);

            if (populateAsExample)
            {
                newList.AddExampleTasks();
                Persist();
            }

            return TaskLists.Count;
        }


        public string ListName(int taskListIndex)
        {
            Log.Instance.LogDebug(string.Format("TaskListGroup.ListName {0}", taskListIndex));

            if (taskListIndex >= TaskLists.Count)
                throw new IndexOutOfRangeException();

            return TaskLists[taskListIndex].Name;
        }



        public void RenameList(int taskListIndex, string newName)
        {
            Log.Instance.LogDebug(string.Format("TaskListGroup.RenameList {0} {1}", taskListIndex, newName));

            if (taskListIndex >= TaskLists.Count)
                throw new IndexOutOfRangeException();

            TaskLists[taskListIndex].Name = newName;

            Persist();
        }


        public void DeleteList(int taskListIndex)
        {
            Log.Instance.LogDebug(string.Format("TaskListGroup.DeleteList {0}", taskListIndex));

            if (taskListIndex >= TaskLists.Count)
                throw new IndexOutOfRangeException();

            TaskLists.RemoveAt(taskListIndex);

            Persist();
        }


        public int NumLists
        {
            get { return TaskLists.Count; }
        }



        public int AddNewTask(int taskListIndex, string task, string colour, string note)
        {
            Log.Instance.LogDebug(string.Format("TaskListGroup.AddNewTask {0} {1} {2}", taskListIndex, task, colour, note));

            if (taskListIndex >= TaskLists.Count)
                throw new IndexOutOfRangeException();

            TaskLists[taskListIndex].AddNewTask(new Task(task, colour, note));

            Persist();
        
            return TaskLists[taskListIndex].NumTasks;
        }


        public string GetTask(int taskListIndex, int taskIndex)
        {
            Log.Instance.LogDebug(string.Format("TaskListGroup.GetTask {0} {1}", taskListIndex, taskIndex));

            if (taskListIndex >= TaskLists.Count)
                throw new IndexOutOfRangeException();

            return TaskLists[taskListIndex].GetTask(taskIndex);
        }


        public string GetTaskColour(int taskListIndex, int taskIndex)
        {
            Log.Instance.LogDebug(string.Format("TaskListGroup.GetTaskColour {0} {1}", taskListIndex, taskIndex));

            if (taskListIndex >= TaskLists.Count)
                throw new IndexOutOfRangeException();

            return TaskLists[taskListIndex].GetTaskColour(taskIndex);
        }


        public string GetTaskNote(int taskListIndex, int taskIndex)
        {
            Log.Instance.LogDebug(string.Format("TaskListGroup.GetTaskNote {0} {1}", taskListIndex, taskIndex));

            if (taskListIndex >= TaskLists.Count)
                throw new IndexOutOfRangeException();

            return TaskLists[taskListIndex].GetTaskNote(taskIndex);
        }


        public void RemoveAllTasks(int taskListIndex)
        {
            Log.Instance.LogDebug(string.Format("TaskListGroup.RemoveAllTasks {0}", taskListIndex));

            if (taskListIndex >= TaskLists.Count)
                throw new IndexOutOfRangeException();

            TaskLists[taskListIndex].RemoveAllTasks();

            Persist();
        }


        public int NumTasks(int taskListIndex)
        {
            Log.Instance.LogDebug(string.Format("TaskListGroup.NumTasks {0}", taskListIndex));

            if (taskListIndex >= TaskLists.Count)
                throw new IndexOutOfRangeException();

            return TaskLists[taskListIndex].NumTasks;
        }

        public void Persist()
        {
            Log.Instance.LogFineDebug(string.Format("TaskListGroup.Persist"));

            if (PersistCallback != null)
                PersistCallback.OnPersist();
            else
                Log.Instance.LogInfo(string.Format("TaskListGroup.Persist - PersistCallback is null, skipping"));
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("{");
            bool first = true;

            foreach (TaskList t in TaskLists)
            {
                if (!first) sb.Append(", ");
                sb.Append(t.ToString());
            }

            sb.Append("}");

            return sb.ToString();
        }

        public int ActiveList
        {
            get { return PropertyActiveList; }
            set { PropertyActiveList = value; Persist(); }
        }


        // private member variables
        private List<TaskList> TaskLists = new List<TaskList>(); // the actual list of tasks
        [field: NonSerializedAttribute]
        IPersistCallback PropertyPersistCallback = null;
        int PropertyActiveList = -1;
    }
}


