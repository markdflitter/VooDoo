using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logging;                                                      // for Log

namespace VooDooModel
{
    // simple list of tasks with a name
    [Serializable()]
    public class TaskList
    {
        // create new named task list
        public TaskList (string name) 
        {
            Log.Instance.LogDebug(string.Format("Creating TaskList {0}", name));

            Name = name; 
        }

     
        // get / set the name of the task list
        public string Name
        {
            get;
            set;
        }


        // return number of tasks in list
        public int NumTasks
        {
            get { return Tasks.Count; }
        }


        // add a new task to the list - returns number of tasks in the list
        public int AddNewTask(Task t)
        {
            Log.Instance.LogDebug(string.Format("TaskList.AddNewTask {0}", t));

            Tasks.Add(t);
            return Tasks.Count;
        }
        

        public string GetTask(int taskIndex)
        {
            Log.Instance.LogDebug(string.Format("TaskList.GetTask {0}", taskIndex));

            if (taskIndex >= Tasks.Count)
                throw new IndexOutOfRangeException();

            return Tasks[taskIndex].Description;
        }


        public string GetTaskColour(int taskIndex)
        {
            Log.Instance.LogDebug(string.Format("TaskList.GetTaskColour {0}", taskIndex));

            if (taskIndex >= Tasks.Count)
                throw new IndexOutOfRangeException();

            return Tasks[taskIndex].Colour;
        }


        public string GetTaskNote(int taskIndex)
        {
            Log.Instance.LogDebug(string.Format("TaskList.GetTaskNote {0}", taskIndex));

            if (taskIndex >= Tasks.Count)
                throw new IndexOutOfRangeException();

            return Tasks[taskIndex].Note;
        }



        // empty out the list
        public void RemoveAllTasks()
        {
            Log.Instance.LogDebug(string.Format("TaskList.RemoveAllTasks"));

            Tasks.Clear();
        }



        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append ("{");
            bool first = true;

            foreach (Task t in Tasks)
            {
                if (!first) sb.Append(", ");
                sb.Append(t.ToString());
            }

            sb.Append("}");

            return sb.ToString();
        }

        public void AddExampleTasks()
        {
            Log.Instance.LogDebug(string.Format("TaskList.AddExampleTasks"));

            AddNewTask(new Task("Do shopping", "Red", "Shopping is red"));
            AddNewTask(new Task("Wash up", "Red",""));
            AddNewTask(new Task("Iron shirts", "Black", "The shirts are black.  Oh dear!"));
            AddNewTask(new Task("Feed cats", "Black",""));
            AddNewTask(new Task("Haircut", "Grey", "My hair is not grey"));
            AddNewTask(new Task("Call garage", "Blue",""));
            AddNewTask(new Task("Make sandwiches", "Blue", "Blue cheese.  Nom!"));
        }

        //private member variables
        private List<Task> Tasks = new List<Task>();
    }
}
