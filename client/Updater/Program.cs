using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Updater
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read("Lab.zip"))
            //{

            //    // here, we extract every entry, but we could extract conditionally
            //    // based on entry name, size, date, checkbox status, etc.  
            //    foreach (Ionic.Zip.ZipEntry entry in zip)
            //    {
            //        try
            //        {
            //            if ((entry.FileName == "Updater.v1.exe") || (entry.FileName == "Ionic.Zip.Reduced.dll"))
            //                continue;
            //            entry.Extract(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), 
            //                Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
            //        }
            //        catch
            //        {
            //            continue;
            //        }
            //    }
            //}

            MainFrm frm = new MainFrm();
            frm.UpdateType = 0;
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-u")
                        frm.UpdateType = Int32.Parse(args[i + 1]);
                }
            }
            Application.Run(frm);
        }
    }
}
