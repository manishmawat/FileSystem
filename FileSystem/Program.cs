using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello FileSystem");
            string[] input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            TaskFactory task = new TaskFactory();
            ITask taskClass = task.taskDictionary[input[0].ToUpper()];
            taskClass.DoTask(input[1]);
        }
    }

    class CreateDirectory : ITask
    {
        public void DoTask(string directoryName)
        {
            DirectoryInfo createDirectory = new DirectoryInfo(directoryName);
            if (createDirectory.Exists)
            {
                Console.WriteLine("Folder already exist");
                return;
            }
            createDirectory.Create();
        }
    }

    class CreateFile : ITask
    {
        public void DoTask(string fullFileName)
        {
            //char value ='\\';
            int indexPositionBeforeFileName = fullFileName.LastIndexOf('\\');
            Directory.CreateDirectory(fullFileName.Substring(0, indexPositionBeforeFileName));
            //File.Create(fullFileName.Substring(indexPositionBeforeFileName + 1, fullFileName.Length - indexPositionBeforeFileName - 1));
            File.Create(fullFileName);
        }
    }

    class CompressContent : ITask
    {
        public void DoTask(string sourceZipContentDirOrFile/*,string destinationZipContentDirOrFile*/)
        {
            int indexOfSeparator = sourceZipContentDirOrFile.IndexOf('#');
            ZipFile.CreateFromDirectory(sourceZipContentDirOrFile.Substring(0, indexOfSeparator), sourceZipContentDirOrFile.Substring(indexOfSeparator + 1, sourceZipContentDirOrFile.Length - indexOfSeparator - 1));
        }
    }

    interface ITask
    {
        void DoTask(string taskInput);
    }
}
