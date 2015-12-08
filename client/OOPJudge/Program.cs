using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Management;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace OOPJudge
{
    static class Program
    {
        static Form frm;
        public static NotifyIcon mNotifyIcon;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadExit += Application_ThreadExit;

            mNotifyIcon = new NotifyIcon();
            mNotifyIcon.Icon = Properties.Resources.main;
            mNotifyIcon.Visible = true;
            GlobalVar.MyNotifyIcon = mNotifyIcon;

            LoadLabVer();

            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-u")
                        GlobalVar.isUpdate = true;
                    else if (args[i] == "-t")
                        GlobalVar.Admin = true;
                }
            }

            if (!GlobalVar.isUpdate)
            {
                frm = new FrmTerm();
                if (frm.ShowDialog() != DialogResult.OK)
                {
                    mNotifyIcon.Visible = false;
                    return;
                }
            }

            CleanAccessor();

            GlobalVar.Status = 1;

            Thread thread = new Thread(new ParameterizedThreadStart(AddInsetUSBHandler));
            thread.Start();
            //AddInsetUSBHandler(null);

            if ((!GlobalVar.isUpdate)&&(!GlobalVar.Admin))
            {
                frm = new FrmLogin();
                if (frm.ShowDialog() != DialogResult.OK)
                {
                    mNotifyIcon.Visible = false;
                    return;
                }

                GlobalVar.StudentID = ((FrmLogin)frm).StudentID;
                GlobalVar.StudentName = ((FrmLogin)frm).StudentName;
                GlobalVar.RoomCode = ((FrmLogin)frm).RoomCode;
            }


            GlobalVar.Status = 2;

            if (GlobalVar.isUpdate)
            {
                LoadGlobal();
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerAsync();

                string deviceid = "";
                foreach (DriveInfo dInfo in DriveInfo.GetDrives())
                {
                    if ((dInfo.IsReady) && (dInfo.VolumeLabel.ToLower() == GlobalVar.AppName.ToLower()))
                        deviceid = dInfo.Name;
                }

                ProcessStartInfo pInfo = new ProcessStartInfo(deviceid + "OOPStarter.exe");
                Process.Start(pInfo);
            }


            SaveGlobal();

            LoadLabs();

            frm = new FrmChoose();
            if (frm.ShowDialog() != DialogResult.OK)
            {
                mNotifyIcon.Visible = false;
                return;
            }

            GlobalVar.Status = 3;

            try
            {
                string storepath = GlobalVar.AppDir + "\\Store\\" + GlobalVar.CurrentLab.Name;
                if (Directory.Exists(storepath))
                    Directory.Delete(storepath, true);
            }
            catch(Exception e)
            {
                MessageBox.Show("Warning: " + e.Message);
            }

            //worker = new BackgroundWorker();
            //worker.DoWork += worker_DoWork;
            //worker.RunWorkerAsync();

            try
            {
            string outpath = GlobalVar.AppDir + "\\Out";
            if (Directory.Exists(outpath))
                Directory.Delete(outpath, true);
            }
            catch (Exception e)
            {
                MessageBox.Show("Warning: " + e.Message);
            }

            if (GlobalVar.Admin)
            {
                frm = new FrmGrader();
                frm.Show();
            }
            else
            {
                frm = new FrmTest();
                frm.Show();
            }
            Application.Run();
            
        }

        static void Application_ThreadExit(object sender, EventArgs e)
        {
            mNotifyIcon.Visible = false;
        }

        private static void LoadGlobal()
        {
            StreamReader r = new StreamReader("info.txt");
            GlobalVar.StudentID = r.ReadLine();
            GlobalVar.StudentName = r.ReadLine();
            string st = r.ReadLine();

            if ((!r.EndOfStream) && (!st.Contains(";")))
            {
                GlobalVar.RoomCode = st;
                st = r.ReadLine();
            }
            string[] files = st.Split(new char[] { ';' });
            for (int i = 0; i < files.Length; i++)
                if (files[i].Trim() != "")
                    File.Delete(GlobalVar.AppDir + "\\Lab\\" + files[i]);
            r.Close();
        }

        static void LoadLabVer()
        {
            StreamReader r = new StreamReader("lab.txt");
            GlobalVar.LabVersion = Int32.Parse(r.ReadToEnd());
            r.Close();
        }

        static void CleanAccessor()
        {
            if (!File.Exists("info.txt"))
                return;
            StreamReader r = new StreamReader("info.txt");
            r.ReadLine();
            r.ReadLine();
            string st = r.ReadLine();

            if ((!r.EndOfStream) && (!st.Contains(";")))
            {
                st = r.ReadLine();
            }
            string[] files = st.Split(new char[] { ';' });
            for (int i = 0; i < files.Length; i++)
                if (files[i].Trim() != "")
                    File.Delete(GlobalVar.AppDir + "\\Lab\\" + files[i]);
            r.Close();
        }

        static void SaveGlobal()
        {
            StreamWriter w = new StreamWriter("info.txt");
            w.WriteLine(GlobalVar.StudentID);
            w.WriteLine(GlobalVar.StudentName);
            w.WriteLine(GlobalVar.RoomCode);
            foreach (string file in GlobalVar.DelList)
            {
                w.Write(file + ";");
            }
            w.WriteLine();
            w.Close();
        }

        public static void LoadLabs()
        {
            string[] filenames = Directory.GetFiles(GlobalVar.AppDir + "\\Lab");
            foreach (string filename in filenames)
            {
                Assembly a = Assembly.LoadFrom(filename);
                Type[] types = a.GetTypes();
                foreach (Type type in types)
                {
                    if (type.Name == "ProgramXXX")
                    {
                        LabInfo info = new LabInfo();
                        info.Path = filename;
                        info.Start = (DateTime)type.GetProperty("Start", typeof(DateTime)).GetValue(null, null);
                        info.End = (DateTime)type.GetProperty("End", typeof(DateTime)).GetValue(null, null);
                        info.Elapse = (int)type.GetProperty("Elapse", typeof(int)).GetValue(null, null);
                        info.Name = (string)type.GetProperty("Name", typeof(string)).GetValue(null, null);
                        info.FullName = (string)type.GetProperty("FullName", typeof(string)).GetValue(null, null);
                        info.Files = (string[])type.GetProperty("Files", typeof(string[])).GetValue(null, null);
                        if (type.GetProperty("Mode", typeof(int)) != null)
                            info.Mode = (int)type.GetProperty("Mode", typeof(int)).GetValue(null, null);
                        else
                            info.Mode = 0;

                        if ((GlobalVar.Admin) || ((info.Start <= DateTime.Now.AddSeconds(GlobalVar.FlexTime))
                            && (info.End.AddSeconds(GlobalVar.FlexTime) >= DateTime.Now)))
                            GlobalVar.ListLabs.Add(info);

                        if ((!GlobalVar.Admin) && (info.End.AddSeconds(GlobalVar.FlexTime) < DateTime.Now))
                            GlobalVar.DelList.Add(Path.GetFileName(filename));
                        break;
                    }
                }
            }

            GlobalVar.ListLabs.Sort();

            SaveGlobal();
        }

        static ManagementEventWatcher w = null;

        static void AddInsetUSBHandler(object obj)
        {
            WqlEventQuery q;
            ManagementScope scope = new ManagementScope("root\\CIMV2");
            scope.Options.EnablePrivileges = true;

            try
            {
                q = new WqlEventQuery();
                q.EventClassName = "__InstanceCreationEvent";
                q.WithinInterval = new TimeSpan(0, 0, 3);
                q.Condition = @"TargetInstance ISA 'Win32_PnPEntity' OR TargetInstance ISA 'Win32_LogicalDisk'";
                w = new ManagementEventWatcher(scope, q);
                w.EventArrived += new EventArrivedEventHandler(USBAdded);
                w.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                if (w != null)
                    w.Stop();
            }
        }

        public static void USBAdded(object sender, EventArrivedEventArgs e)
        {
            object tmpObj = GetPropertyData("TargetInstance", e.NewEvent.Properties);
            //Detect PnP Device
            if (tmpObj != null)
            {
                ManagementBaseObject device = (ManagementBaseObject)tmpObj;
                tmpObj = GetPropertyData("CreationClassName", device.Properties);
                if ((tmpObj != null) && (tmpObj.ToString() == "Win32_LogicalDisk"))
                {
                    tmpObj = GetPropertyData("VolumeName", device.Properties);
                    if ((tmpObj != null) && (tmpObj.ToString().ToLower() == GlobalVar.AppName.ToLower()))
                    {
                        string deviceid = GetPropertyData("DeviceID", device.Properties).ToString();
                        if ((deviceid[0] < 'A') || (deviceid[0] > 'Z') || (deviceid.Length < 2) || (deviceid[1] != ':'))
                            MessageBox.Show("Invalid device id");

                        BackgroundWorker worker = new BackgroundWorker();
                        worker.DoWork += worker_DoWork;
                        worker.RunWorkerAsync();
                        
                        ProcessStartInfo pInfo = new ProcessStartInfo(deviceid + "\\OOPStarter.exe");
                        Process.Start(pInfo);

                    }
                }
            }
        }

        static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            NamedPipeServerStream pServer = new NamedPipeServerStream("OOPJudge", PipeDirection.InOut);
            pServer.WaitForConnection();
            StreamString ss = new StreamString(pServer);
            string msg = GlobalVar.Status.ToString() + "|" + GlobalVar.Version.ToString() + "|" + GlobalVar.StudentID + "|" + GlobalVar.StudentName;
            if (GlobalVar.Status == 3)
                msg += "|" + GlobalVar.CurrentLab.Name;
            ss.WriteString(msg);
            string st = ss.ReadString();
            if (st.Contains("|"))
            {
                string[] args = st.Split(new char[] { '|' });
                if (args[0] == "Start")
                {
                    FrmChoose frmChoose = (FrmChoose)frm;
                    frmChoose.filename = args[1];
                    GlobalVar.LabMode = 1;
                    frmChoose.isReload = true;
                }
            }
            else if (st == "Submit")
            {
                FrmTest frmTest = (FrmTest)frm;
                if ((frmTest.frmEnd != null) && (!frmTest.frmEnd.isClose))
                    frmTest.frmEnd.isClose = true;
            }
            else if (st == "Kill")
            {
                pServer.Close();
                Application.Exit();
            }
            pServer.Close();
        }

        private static object GetPropertyData(string id, PropertyDataCollection collection)
        {
            foreach (PropertyData data in collection)
            {
                if (data.Name == id)
                    return data.Value;
            }
            return null;
        }

    }
}
