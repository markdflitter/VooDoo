using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logging;                                                      // for Log

namespace VooDoo.ViewModel
{
    class ContextMenuItem
    {
        public ContextMenuItem(string name, CustomCommandHandler moveCommand)
        {
            Log.Instance.LogDebug(string.Format("Creating ContextMenuItem {0}", name));
            Name = name;
            MoveCommand = moveCommand;
        }

        public string Name
        {
            get;
            set;
        }

        public CustomCommandHandler MoveCommand
        {
            get;
            set;
        }
    }
}
