using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VooDooModel
{
    // represents a set of task lists, indexed by integer starting from 0
    public interface ITaskListGroup
    {
        //probably should be an event
        void SetPersistCallback(IPersistCallback p);

        // add a new list to the group and return count of lists
        int AddNewList(string name, bool populateAsExample);


        // get the name of a list
        string ListName(int taskLIstIndex);


        // rename a list
        void RenameList(int taskListIndex, string newName);


        // remove a list from group
        void DeleteList(int taskListIndex);


        // return count of the number of lists
        int NumLists { get; }



        // add a new task to a list - return count of tasks in that list
        int AddNewTask(int taskListIndex, string name, string colour, string note);


        // return a task details
        string GetTask(int taskListIndex, int taskIndex);
        string GetTaskColour(int taskListIndex, int taskIndex);
        string GetTaskNote(int taskListIndex, int taskIndex);


        // remove all tasks in a list
        void RemoveAllTasks(int taskListIndex);


        // return number of tasks in a list
        int NumTasks(int taskListIndex);

        //currently active task list in group
        int ActiveList
        {
            get;
            set;
        }


        void Persist();
    }
}