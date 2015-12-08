using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace OOPJudge
{
    class GlobalVar
    {
        public static bool Admin = false;
        public static int LabMode = 0; // 1: Test -- Lab Mode
        public static int UserMode = 0; //1: Test -- User Mode
        public static int RunMode
        {
            get
            {
                if (LabMode == 1)
                    return 1;
                if (UserMode == 1)
                    return 1;
                return 0;
            }
        }
        public static int Status = 0;
        public static string AppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string AppName = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
        public static Version Version = Assembly.GetEntryAssembly().GetName().Version;
        public static int LabVersion;
        public static string StudentID;
        public static string StudentName;
        public static string RoomCode;
        public static List<LabInfo> ListLabs = new List<LabInfo>();
        public static double FlexTime = 3600 * 24;
        public static LabInfo CurrentLab;
        public static NotifyIcon MyNotifyIcon;

        public static List<string> DelList = new List<string>();
        public static bool isUpdate = false;

        public static void LoadLab(string filename)
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

            GlobalVar.ListLabs.Sort();
        }
    }
}
