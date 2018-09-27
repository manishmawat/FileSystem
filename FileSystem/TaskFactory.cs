using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem
{
    class TaskFactory : ITask
    {
        public Dictionary<string, ITask> taskDictionary = new Dictionary<string, ITask>();
        public TaskFactory()
        {
            taskDictionary.Add(CommandConstants.CREATEDIRECTORY, new CreateDirectory());
            taskDictionary.Add(CommandConstants.CREATEFILE, new CreateFile());
            taskDictionary.Add(CommandConstants.COMPRESSFILE, new CompressContent());
            //taskDictionary.Add(CommandConstants.ZIPFILE7Zip, new ZipFile7Zip());

            //File operations
            taskDictionary.Add(CommandConstants.SHOWFILEDATA, new ShowFileData());
            taskDictionary.Add(CommandConstants.ENTERDATA, new Enterdata());
        }

        public void DoTask(string taskInput)
        {

        }
    }
}
