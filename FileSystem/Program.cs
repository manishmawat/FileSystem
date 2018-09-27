using SevenZip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello FileSystem");
            try
            {
                string[] input = Console.ReadLine().Split(' '/*, StringSplitOptions.RemoveEmptyEntries*/);
                TaskFactory task = new TaskFactory();
                ITask taskClass = task.taskDictionary[input[0].ToUpper()];
                taskClass.DoTask(string.Join(" ", input.Skip(1)));
            }
            catch(Exception ex)
            {
                Console.WriteLine("You have entered a invalid command");
            }
        }

        static void ZipmyFile()
        {
            SevenZip.Compression.LZMA.Encoder encode = new SevenZip.Compression.LZMA.Encoder();
            using(var inputFileStream=new FileStream(@"d:\manishnewfolder\abc.txt", FileMode.Open))
            {
                using (var outputFileStream = new FileStream(@"d:\manishnewfolder\abc.txt.zip", FileMode.Create))
                {
                    encode.Code(inputFileStream, outputFileStream, inputFileStream.Length, inputFileStream.Length, null);
                    outputFileStream.Flush();
                }
            }
        }
    }

    #region file operations
    class ShowFileData : ITask
    {
        public void DoTask(string filePath)
        {
            FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read);
            Console.WriteLine(fs.ReadByte());
            fs.Dispose();
            byte[] file = File.ReadAllBytes(filePath);
            using (var ms = new MemoryStream(file))
            {
                for (int i = 0; i < ms.Length; i++)
                    Console.WriteLine(ms.ReadByte());
            }

            //Console.WriteLine(File.ReadAllText(filePath));
        }
    }

    class Enterdata : ITask
    {
        public void DoTask(string filePathWithdata)
        {
            string filePath = filePathWithdata.Substring(0, filePathWithdata.IndexOf("#"));
            File.WriteAllText(filePath, filePathWithdata.Substring(filePathWithdata.IndexOf("#"), filePathWithdata.Length - filePathWithdata.IndexOf("#") - 1));
        }
    }
    #endregion file operations

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

    //class ZipFile7Zip : ITask
    //{
    //    public void DoTask(string fullPath)
    //    {
    //        string inFile, outFile;
    //        inFile = @"d:\manishnewfolder\abc.txt";//fullPath.Substring(0, fullPath.LastIndexOf('#'));
    //        outFile = @"d:\manishnewfolder\abc.zip";//fullPath.Substring(fullPath.LastIndexOf('#') + 1, fullPath.Length - fullPath.LastIndexOf('#') - 1);
    //        Int32 dictionary = 1 << 23;
    //        Int32 posStateBits = 2;
    //        Int32 litContextBits = 3; // for normal files
    //        // UInt32 litContextBits = 0; // for 32-bit data
    //        Int32 litPosBits = 0;
    //        // UInt32 litPosBits = 2; // for 32-bit data
    //        Int32 algorithm = 2;
    //        Int32 numFastBytes = 128;

    //        string mf = "bt4";
    //        bool eos = true;
    //        bool stdInMode = false;


    //        CoderPropID[] propIDs =  {
    //            CoderPropID.DictionarySize,
    //            CoderPropID.PosStateBits,
    //            CoderPropID.LitContextBits,
    //            CoderPropID.LitPosBits,
    //            CoderPropID.Algorithm,
    //            CoderPropID.NumFastBytes,
    //            CoderPropID.MatchFinder,
    //            CoderPropID.EndMarker
    //        };

    //        object[] properties = {
    //            (Int32)(dictionary),
    //            (Int32)(posStateBits),
    //            (Int32)(litContextBits),
    //            (Int32)(litPosBits),
    //            (Int32)(algorithm),
    //            (Int32)(numFastBytes),
    //            mf,
    //            eos
    //        };

    //        using (FileStream inStream = new FileStream(inFile, FileMode.Open))
    //        {
    //            using (FileStream outStream = new FileStream(outFile, FileMode.Create))
    //            {
    //                SevenZip.Compression.LZMA.Encoder encoder = new SevenZip.Compression.LZMA.Encoder();
    //                encoder.SetCoderProperties(propIDs, properties);
    //                encoder.WriteCoderProperties(outStream);
    //                Int64 fileSize;
    //                if (eos || stdInMode)
    //                    fileSize = -1;
    //                else
    //                    fileSize = inStream.Length;
    //                for (int i = 0; i < 8; i++)
    //                    outStream.WriteByte((Byte)(fileSize >> (8 * i)));
    //                encoder.Code(inStream, outStream, -1, -1, null);
    //            }
    //        }
    //    }
    //}

    interface ITask
    {
        void DoTask(string taskInput);
    }
}
