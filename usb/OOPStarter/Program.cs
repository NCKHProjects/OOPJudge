using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace OOPStarter
{
    public class StreamString
    {
        private Stream ioStream;
        private UnicodeEncoding streamEncoding;

        public StreamString(Stream ioStream)
        {
            this.ioStream = ioStream;
            streamEncoding = new UnicodeEncoding();
        }

        public string ReadString()
        {
            int len = 0;

            len = ioStream.ReadByte() * 256;
            len += ioStream.ReadByte();
            byte[] inBuffer = new byte[len];
            ioStream.Read(inBuffer, 0, len);

            return streamEncoding.GetString(inBuffer);
        }

        public int WriteString(string outString)
        {
            byte[] outBuffer = streamEncoding.GetBytes(outString);
            int len = outBuffer.Length;
            if (len > UInt16.MaxValue)
            {
                len = (int)UInt16.MaxValue;
            }
            ioStream.WriteByte((byte)(len / 256));
            ioStream.WriteByte((byte)(len & 255));
            ioStream.Write(outBuffer, 0, len);
            ioStream.Flush();

            return outBuffer.Length + 2;
        }
    }

    class Program
    {
        static string StudentID;
        static string StudentName;
        static int Status;
        static Version Version = Assembly.GetEntryAssembly().GetName().Version;
        static string AppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        static void Main(string[] args)
        {
            if (AppDir[AppDir.Length - 1] == '\\')
                AppDir = AppDir.Substring(0, AppDir.Length - 1);

            string[] data;
            Process[] p = Process.GetProcessesByName("OOPJudge");
            if (p.Length == 0)
            {
                p = Process.GetProcessesByName("OOPJudge.vshost");
                if (p.Length == 0)
                {
                    Console.WriteLine("Alert: Nothing");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("Warning: Debug");
            }
            if (p.Length > 1)
            {
                Console.WriteLine("Alert: There are more than one app running");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Found!");
            NamedPipeClientStream pClient = new NamedPipeClientStream(".", "OOPJudge", PipeDirection.InOut);
            pClient.Connect();
            StreamString ss = new StreamString(pClient);
            data = ss.ReadString().Split(new char[] { '|' });
            if ((data.Length != 4) && (data.Length != 5))
            {
                Console.WriteLine("Alert: Invalid message format");
                pClient.Close();
                Console.ReadLine();
                return;
            }

            Status = Int32.Parse(data[0]);
            StudentID = data[2];
            StudentName = data[3];
            string filename = p[0].Modules[0].FileName;

            Console.WriteLine("Connected!");

            if (Status < 2)
            {
                Console.WriteLine("Error: User is not ready! User must type StudentID and Name");
                pClient.Close();
                Console.ReadLine();
                return;
            }
            else if (Status == 2)
            {
                if (Version.ToString() != data[1])
                {
                    Console.WriteLine("Need update new version");
                    ss.WriteString("Kill");
                    pClient.Close();
                    do
                    {
                        Console.WriteLine("Try to copy...");
                        Thread.Sleep(3000);
                        p[0].Close();
                    }
                    while (!FileCopy(AppDir + "\\OOPJudge.exe", filename));
                    
                    Process.Start(filename, "-u");
                    return;
                }
            }
            else if (Status == 3)
            {
                if (Version.ToString() != data[1])
                    Console.WriteLine("Warning: Version is out of date");
            }


            
            SHA1 sha1 = SHA1.Create();
            string checksum;
            using (FileStream stream = File.OpenRead(filename))
            {
                byte[] result = sha1.ComputeHash(stream);
                checksum = BitConverter.ToString(result).Replace("-", string.Empty);
            }
            if (checksum != "7E06C1A4917878100BC824DFEB9708F964353CB0")
            {
                Console.WriteLine("Alert: OOPJudge is tampered!!!");
                Console.ReadLine();
                return;
            }

            if (Status == 2)
            {
                string[] files = Directory.GetFiles(AppDir + "\\Lab");
                string file = files[0];
                try
                {
                    File.Copy(file, Path.GetDirectoryName(filename) + "\\Lab\\" + Path.GetFileName(file), true);
                    Console.WriteLine("Copied!");
                    ss.WriteString("Start|" + Path.GetFileName(file));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Warning: " + ex.Message);
                }
            }
            else if (Status == 3)
            {
                ss.WriteString("Submit");
                Console.WriteLine("Coping...");
                string labname = data[4];
                string src = Path.GetDirectoryName(filename) + "\\Zip\\" + labname;
                string dest = AppDir + "\\Zip\\" + StudentID;
                Directory.CreateDirectory(dest);
                DirectoryCopy(src, dest, true);
                Console.WriteLine("Copied");
                Console.WriteLine(dest);
                Process.Start("cmd.exe", "/C explorer " + dest);
            }

            pClient.Close();

            //Console.ReadLine();
        }

        private static bool FileCopy(string src, string dest)
        {
            try
            {
                File.Delete(dest);
                File.Copy(src, dest, true);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
