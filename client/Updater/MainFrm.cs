using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Updater
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        public int UpdateType;
        string filename;

        string status = "1";
        int t = 0;
        string AppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        BackgroundWorker worker;
        delegate void SetValueCallback(int value);

        private void sTimer_Tick(object sender, EventArgs e)
        {
            if ((t == 0) || ((t == 1) && (status == "2")))
            {
                worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerAsync(status);
                t++;
            }
            else if (t == 2)
                sTimer.Stop();
        }


        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = "http://fit.hcmup.edu.vn/~khanhndk/OOPJudge/download.php?type=" + UpdateType.ToString();
            if (e.Argument.ToString() != "1")
                url = "http://khanhndkhcmup.appspot.com/download.jsp?type=" + UpdateType.ToString();

            try
            {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                client.DownloadFileAsync(new Uri(url), filename);
            }
            catch
            {
            }
            
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (status == "2")
                {
                    MessageBox.Show("Error:\n" + e.Error.Message, "Updater");
                    Application.Exit();
                }
                else
                {
                    status = "2";
                }
                return;
            }
            else
            {
                if (File.Exists(filename))
                {
                    FileInfo info = new FileInfo(filename);
                    if (info.Length > 0)
                    {
                        using (ZipFile zip = ZipFile.Read(filename))
                        {

                            // here, we extract every entry, but we could extract conditionally
                            // based on entry name, size, date, checkbox status, etc.  
                            foreach (ZipEntry entry in zip)
                            {
                                try
                                {
                                    if ((entry.FileName == "Updater.v2.exe") || (entry.FileName == "Ionic.Zip.Reduced.dll"))
                                        continue;
                                    entry.Extract(AppDir, ExtractExistingFileAction.OverwriteSilently);
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    File.Delete(filename);
                }
            }

            SetValue(100);
            InitMainProgram();
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            SetValue(e.ProgressPercentage);
        }

        private void SetValue(int value)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.pBar.InvokeRequired)
            {
                SetValueCallback d = new SetValueCallback(SetValue);
                this.Invoke(d, new object[] { value });
            }
            else
            {
                this.pBar.Value = value;
            }
        }

        protected void InitMainProgram()
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo("OOPJudge.exe");
            if (UpdateType > 0)
                p.StartInfo.Arguments = "-l";
            p.Start();
            Application.Exit();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            if (UpdateType == 0)
                filename = "OOPJudge.zip";
            else
                filename = "Lab.zip";
        }
    }
}
