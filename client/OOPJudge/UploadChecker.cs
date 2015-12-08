using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OOPJudge
{
    class UploadChecker
    {
        private string filename = "checker.txt";
        private HashSet<string> files;
        public UploadChecker()
        {
            files = new HashSet<string>();
            if (!File.Exists(filename))
                return;

            StreamReader reader = new StreamReader(filename);
            while (!reader.EndOfStream)
            {
                string st = reader.ReadLine();
                files.Add(st);
            }
            reader.Close();
        }

        public void SaveLog()
        {
            StreamWriter writer = new StreamWriter(filename);
            foreach (string st in files)
            {
                writer.WriteLine(st);
            }
            writer.Close();
        }

        public bool HasUpload(string file)
        {
            return files.Contains(file);
        }

        public bool AddFile(string file)
        {
            return files.Add(file);
        }
    }
}
