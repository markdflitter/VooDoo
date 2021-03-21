using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;                                         // for ICommand
using Logging;                                                      // for Log

namespace VooDoo.ViewModel
{
    class CommandHandler : ICommand
    {
        public event EventHandler CanExecuteChanged; // required by interface ICommand but never used as CanExecute is always true

        public CommandHandler(Action<object> action)
        {
            Log.Instance.LogDebug(string.Format("Creating CommandHandler {0}", action));
            Action = action; 
        }

        // ICommand
        public void Execute(object parameter) { Action(parameter); }
        public bool CanExecute(object parameter) { return true; }

        // private member variables
        Action<object> Action;
    }


    class CustomCommandHandler : ICommand
    {
        public event EventHandler CanExecuteChanged; // required by interface ICommand but never used as CanExecute is always true

        public CustomCommandHandler(int i, Action<int, object> action)
        {
            Log.Instance.LogDebug(string.Format("Creating CustomCommandHandler {0} {1}", i, action));
            I = i; Action = action;
        }

        // ICommand
        public void Execute(object parameter) { Action(I, parameter); }
        public bool CanExecute(object parameter) { return true; }

        // private member variables
        Action<int, object> Action;
        int I;
    }

}
