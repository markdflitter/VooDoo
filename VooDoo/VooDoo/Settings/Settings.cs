using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;                                                    // for streamWriter
using System.Xml.Serialization;                                     // for XmlSerializer
using System.Windows;                                               // for Size
using Logging;                                                      // for Log

namespace VooDoo.Settings
{

    public class Settings
    {
        public string CurrentFile
        {
            get;
            set;
        }

        public Size Size
        {
            get;
            set;
        }

    }


    class AppSettings
    {
        // the one and only instance of the Settings and an accessor to get it
        private static AppSettings myInstance = new AppSettings();
        public static AppSettings Instance { get { return myInstance; } }

        private readonly string SettingsFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +@"\VooDoo.xml";

        private Settings s = new Settings();


        // initialise the Settings
        public AppSettings()
        {
            Settings loadedSettings = Read(SettingsFile);
            if (loadedSettings != null)
                s = loadedSettings;
        }


        public string CurrentFile
        {
            get { return s.CurrentFile; }
            set { s.CurrentFile = value; Save(SettingsFile); }
        }


        public Size Size
        {
            get { return s.Size; }
            set { s.Size = value; Save(SettingsFile); }
        }

        public void Save(string filename)
        {
            Log.Instance.LogDebug(string.Format("Settings.Save {0}", filename));
            using (StreamWriter sw = new StreamWriter(filename))
            {
                XmlSerializer xmls = new XmlSerializer(typeof(Settings));
                xmls.Serialize(sw, s);
            }
        }


        private Settings Read(string filename)
        {
            Log.Instance.LogDebug(string.Format("Settings.Read {0}", filename));

            try
            {

                using (StreamReader sw = new StreamReader(filename))
                {
                    XmlSerializer xmls = new XmlSerializer(typeof(Settings));
                    return xmls.Deserialize(sw) as Settings;
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Log.Instance.LogDebug(string.Format("Settings.Read - FileNotFoundException"));
            }

            return null;
        }
    }
}