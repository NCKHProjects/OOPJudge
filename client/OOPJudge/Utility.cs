using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace OOPJudge
{
    class Utility
    {
        public static void FileProcess(string src, ref string output)
        {
            StreamReader reader = new StreamReader(src);
            bool check = false;
            while (!reader.EndOfStream)
            {
                string st = reader.ReadLine();
                string stTrim = st.Trim();
                if (stTrim.Contains("class"))
                    check = true;
                if ((check) || (!check && ((stTrim.Length < 5) || (stTrim.Substring(0, 5) != "using"))))
                    output += st + "\r\n";
            }
            reader.Close();
        }

        public static string CalculateMD5Hash(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
        }
    }
}
