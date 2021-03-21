using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;                                                    // for streamWriter
using System.Runtime.Serialization.Formatters.Binary;               // for BinaryFormatter
using Logging;                                                      // for Log

namespace VooDooModel
{
    public class FilePersister
    {
        public FilePersister(string filePath)
        {
            Log.Instance.LogDebug(string.Format("Creating FilePersister {0}", filePath));
            FilePath = filePath;
        }


        public string FilePath
        {
            get;
            set;
        }


        public void Save(ITaskListGroup tlg)
        {
            if (FilePath != null)
            {
                Log.Instance.LogFineDebug(string.Format("FilePersister.Save {0}", FilePath));

                Stream s = File.Create(FilePath);

                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(s, tlg);

                s.Close();
            }
        }


        public ITaskListGroup Load()
        {
            Log.Instance.LogFineDebug(string.Format("FilePersister.Load {0}", FilePath));

            ITaskListGroup result = null;

            try
            {
                Stream s = File.OpenRead(FilePath);
                BinaryFormatter deserializer = new BinaryFormatter();
                result = (TaskListGroup)deserializer.Deserialize(s);
                s.Close();
            }
            catch (System.IO.FileNotFoundException)
            {
                Log.Instance.LogError(string.Format("FilePersister.Load file not found"));

            }

            return result;
        }
    }
}
