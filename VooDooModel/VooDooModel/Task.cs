using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logging;                                                       // for Log

namespace VooDooModel
{
    // represents a single task in a list, with an ID and a description
    [Serializable()]
    public class Task
    {
        public Task(string description, string colour, string note)
        {
            Log.Instance.LogDebug(string.Format("Creating Task {0} {1} {2}", description, colour, note));

            Description = description;
            Colour = colour;
            Note = note;
        }

        public string Description
        {
            get;
            set;
        }


        public string Colour
        {
            get;
            set;
        }


        public string Note
        {
            get;
            set;
        }

        override public string ToString()
        {
            return Description;
        }
    }
}
