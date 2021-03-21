using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;                                         // for ICommand
using Logging;                                                      // for Log

namespace VooDoo.ViewModel
{
    class RenameViewModel
    {
        public RenameViewModel(Action<string> renameAction)
        {
            RenameAction = renameAction;
        }

        public ICommand OKRenameCommand { get { return new CommandHandler(OnOKRename); } }
        private void OnOKRename(object parameter)
        {
            Log.Instance.LogInfo(string.Format("RenameViewModel.OnOKRename {0}", parameter));

            RenameDialog w = parameter as RenameDialog;
            if (w != null)
            {
                w.Close();
            }

            Log.Instance.LogInfo(string.Format("RenameViewModel - renaming list to {0}", w.textBox1.Text));

            RenameAction(w.textBox1.Text);
        }

        public ICommand CancelRenameCommand { get { return new CommandHandler(OnCancelRename); } }
        private void OnCancelRename(object parameter)
        {
            Log.Instance.LogInfo(string.Format("RenameViewModel.OnCancelRename {0}", parameter));

            RenameDialog w = parameter as RenameDialog;
            if (w != null)
            {
                w.Close ();
            }
        }


        //private member variables
        Action<string> RenameAction;
    }
}
