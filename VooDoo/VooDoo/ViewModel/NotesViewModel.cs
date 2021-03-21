using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;                                         // for ICommand
using Logging;                                                      // for Log

namespace VooDoo.ViewModel
{
    class NotesViewModel
    {
        public NotesViewModel(int index, Action<int, string> onOKAction)
        {
            Index = index;
            OnOKAction = onOKAction;
        }

        public ICommand OKCommand { get { return new CommandHandler(OnOK); } }
        private void OnOK(object parameter)
        {
            Log.Instance.LogInfo(string.Format("NotesViewModel.OnOK {0}", parameter));

            NotesDialog w = parameter as NotesDialog;
            if (w != null)
            {
                w.Close();
            }

            OnOKAction(Index, w.textBox.Text);
        }

        public ICommand CancelCommand { get { return new CommandHandler(OnCancel); } }
        private void OnCancel(object parameter)
        {
            Log.Instance.LogInfo(string.Format("NotesViewModel.OnCancel {0}", parameter));

            NotesDialog w = parameter as NotesDialog;
            if (w != null)
            {
                w.Close();
            }
        }


        //private member variables
        int Index;
        Action<int, string> OnOKAction;
    }
}
