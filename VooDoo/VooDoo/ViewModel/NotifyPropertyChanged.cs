using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;                                        // for INotifyPropertyChanged
using Logging;                                                      // for Log

namespace VooDoo.ViewModel
{
    abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // notify UI that property has changed
        protected void RaisePropertyChanged(string propertyName)
        {
            Log.Instance.LogDebug (string.Format("RaisePropertyChanged {0}", propertyName));

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                Log.Instance.LogDebug(string.Format("RaisePropertyChanged {0}", propertyName));
            }

        }
    }
}
