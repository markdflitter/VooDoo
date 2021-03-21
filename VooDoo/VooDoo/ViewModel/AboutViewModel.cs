using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;                                         // for ICommand
using System.Windows.Controls;                                      // for Button
using System.Reflection;                                            // for Assembly
using Logging;                                                      // for Log

namespace VooDoo.ViewModel
{
    class AboutViewModel
    {
        public string Author
        {
            get { return "Mark D Flitter"; }
        }

        public string Version
        {
            get { return Assembly.GetEntryAssembly().GetName().Version.ToString(); }
        }

        public string BuildDate
        {
            get { return ""; }// Properties.Resources.BuildDate; 
        }

        public ICommand CloseAboutCommand { get { return new CommandHandler(OnCloseAbout); } }
        private void OnCloseAbout(object parameter)
        {
            Log.Instance.LogInfo(string.Format("AboutViewModel.OnCloseAbout {0}", parameter));

            AboutDialog w = parameter as AboutDialog;
            if (w != null)
            {
                w.Close();
            }
        }
    }
}
