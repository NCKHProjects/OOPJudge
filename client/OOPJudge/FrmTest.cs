using Ionic.Zip;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OOPJudge
{
    public partial class FrmTest : Form
    {
        public FrmEnd frmEnd = null;

        int remaintime;
        bool acceptclose = false;

        string serveradress = "";
        bool uploadfinished = false;

        public FrmTest()
        {
            InitializeComponent();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            GlobalVar.LabMode = GlobalVar.CurrentLab.Mode;
            txtStudentName.Text = GlobalVar.StudentName;
            txtStudentID.Text = GlobalVar.StudentID;
            txtLabName.Text = GlobalVar.CurrentLab.FullName;
            txtProjectName.Text = GlobalVar.StudentID.Replace(".", "") + "_" + GlobalVar.CurrentLab.Name;
            double end_now =  (GlobalVar.CurrentLab.End - DateTime.Now).TotalMinutes;
            if (end_now < GlobalVar.CurrentLab.Elapse)
                remaintime = (int)(end_now * 60);
            else
                remaintime = GlobalVar.CurrentLab.Elapse * 60;
            UpdateTimer();

            bgConnector.RunWorkerAsync();
        }

        private void UpdateTimer()
        {
            int h = remaintime / 3600;
            int m = (remaintime % 3600) / 60;
            int s = (remaintime % 3600) % 60;
            lblTimer.Text = String.Format("{0}:{1}:{2}", h.ToString("00"), m.ToString("00"), s.ToString("00"));
        }

        private void tLab_Tick(object sender, EventArgs e)
        {
            remaintime--;
            UpdateTimer();
            if (remaintime <= 0)
            {
                btnSubmit.Enabled = false;
                tLab.Enabled = false;

                if (GlobalVar.RunMode == 1)
                {
                    if (uploadfinished)
                    {
                        MessageBox.Show(this, "Đã upload thành công. Bạn KHÔNG cần phải nộp bài qua USB", "Finish");
                    }
                    else
                    {
                        frmEnd = new FrmEnd();
                        frmEnd.Show();
                    }
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Zip Files (*.zip)|*.zip";
            dlg.FilterIndex = 1;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fullpath = dlg.FileName;
                if (fullpath == "")
                    return;

                FileInfo fInfo = new FileInfo(fullpath);
                string filename = fInfo.Name;
                if (filename != txtProjectName.Text + ".zip")
                {
                    MessageBox.Show("Chọn tên file sai cú pháp đặt tên!", "OOP Judge", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SubmissionInfo sInfo = new SubmissionInfo();
                sInfo.TimeSubmit = DateTime.Now;
                sInfo.Size = fInfo.Length;
                sInfo.FullPath = fullpath;
                lstView.Items.Add(new ListViewItem(new string[] { sInfo.TimeSubmit.ToString(), sInfo.Size.ToString(), "", "", "" }));
                btnSubmit.Enabled = false;

                bgWorker.RunWorkerAsync(sInfo);

                uploadfinished = false;
            }

            //FolderBrowserDialog dlg = new FolderBrowserDialog();
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    string fullpath = dlg.SelectedPath;
            //    DirectoryInfo dInfo = new DirectoryInfo(fullpath);
            //    string dirname = dInfo.Name;
            //    if (dirname != txtProjectName.Text)
            //    {
            //        MessageBox.Show("Chọn đường dẫn sai!", "OOP Judge", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //    SubmissionInfo sInfo = new SubmissionInfo();
            //    sInfo.TimeSubmit = DateTime.Now;
            //    sInfo.Size = DirSize(dInfo);
            //    sInfo.FullPath = fullpath;
            //    lstView.Items.Add(new ListViewItem(new string[] { sInfo.TimeSubmit.ToString(), sInfo.Size.ToString(), "", "", "" }));
            //    btnSubmit.Enabled = false;

            //    bgWorker.RunWorkerAsync(sInfo);
            //}
        }

        public static long DirSize(DirectoryInfo d)
        {
            long Size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                Size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                Size += DirSize(di);
            }
            return (Size);
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



        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            SubmissionInfo sInfo = (SubmissionInfo)e.Argument;
            string fullpath = sInfo.FullPath;
            string filename = Path.GetFileNameWithoutExtension(fullpath);
            string submissionid = filename + "_" + DateTime.Now.Ticks.ToString("X");

            sInfo.ID = submissionid;

            BackgroundWorker worker = (BackgroundWorker)sender;
            sInfo.Report("Copying...");
            worker.ReportProgress(5, sInfo);
            Thread.Sleep(1000);

            string zipdir = GlobalVar.AppDir + "\\Zip\\" + GlobalVar.CurrentLab.Name;
            if (!Directory.Exists(zipdir))
                Directory.CreateDirectory(zipdir);
            zipdir += "\\" + submissionid + ".zip";
            File.Copy(fullpath, zipdir);

            sInfo.Report("Unzipping...");
            worker.ReportProgress(10, sInfo);
            Thread.Sleep(1000);

            string storedir = GlobalVar.AppDir + "\\Store\\" + GlobalVar.CurrentLab.Name + "\\" + submissionid;
            //DirectoryCopy(fullpath, storedir , true);

            try
            {
                FileInfo fInfo = new FileInfo(fullpath);
                if (fInfo.Length > 0)
                {
                    if (!Directory.Exists(storedir))
                        Directory.CreateDirectory(storedir);

                    using (ZipFile zip = ZipFile.Read(fullpath))
                    {

                        // here, we extract every entry, but we could extract conditionally
                        // based on entry name, size, date, checkbox status, etc.  
                        foreach (ZipEntry entry in zip)
                        {
                            try
                            {
                                if ((entry.FileName == "Updater.v2.exe") || (entry.FileName == "Ionic.Zip.Reduced.dll"))
                                    continue;
                                entry.Extract(storedir, ExtractExistingFileAction.OverwriteSilently);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sInfo.Report("Finished", "0", "Unzip error: " + ex.Message);
                worker.ReportProgress(100, sInfo);
                return;
            }


            string source = "";
            source += "using System;";
            source += "using System.Text;";
            
            for(int i = 0; i < GlobalVar.CurrentLab.Files.Length;i++)
            {
                string[] result = Directory.GetFiles(storedir, GlobalVar.CurrentLab.Files[i], SearchOption.AllDirectories);
                if (result.Length == 0)
                {
                    sInfo.Report("Finished", "0", GlobalVar.CurrentLab.Files[i] + " not found");
                    worker.ReportProgress(100, sInfo);
                    return;
                }
                Utility.FileProcess(result[0], ref source);
            }
            sInfo.Report("Compiling...");
            worker.ReportProgress(20, sInfo);
            Thread.Sleep(1000);

            if (!Directory.Exists(GlobalVar.AppDir + "\\Out"))
                Directory.CreateDirectory(GlobalVar.AppDir + "\\Out");

            Dictionary<string, string> providerOption = new Dictionary<string, string>();
            providerOption.Add("CompilerVersion", "v2.0");
            CodeDomProvider codeProvider = new CSharpCodeProvider(providerOption);

            CompilerParameters option = new CompilerParameters();
            option.GenerateExecutable = false;
            option.IncludeDebugInformation = true;
            option.OutputAssembly = GlobalVar.AppDir + "\\Out\\" + submissionid + ".dll";
            option.ReferencedAssemblies.Add("System.dll");
            option.ReferencedAssemblies.Add("System.Data.dll");
            
            CompilerResults cr = codeProvider.CompileAssemblyFromSource(option,  source);
            if (cr.Errors.Count > 0)
            {
                sInfo.Report("Finished", "0", "Compile error");
                worker.ReportProgress(30, sInfo);
                return;
            }

            sInfo.Report("Running...");
            worker.ReportProgress(40, sInfo);
            Thread.Sleep(1000);
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo(GlobalVar.CurrentLab.Path);
            info.Arguments = submissionid + ".dll";
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            info.RedirectStandardInput = true;
            p.StartInfo = info;
            p.Start();
            p.WaitForExit(5000);
            if (!p.HasExited)
            {
                sInfo.Report("Finished", "0");
                worker.ReportProgress(30, sInfo);
                p.Kill();
            }
            else
            {
                string output = p.StandardOutput.ReadToEnd();
                string[] stArr = output.Split(new char[] { '@' });
                if (stArr.Length > 1)
                    sInfo.Report("Finished", stArr[0], stArr[1]);
                else
                    sInfo.Report("Finished", "0", stArr[0]);
                worker.ReportProgress(30, sInfo);
            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SubmissionInfo sInfo = (SubmissionInfo)e.UserState;

            lstView.Items[lstView.Items.Count - 1].SubItems[2].Text = sInfo.Status;
            lstView.Items[lstView.Items.Count - 1].SubItems[3].Text = sInfo.Result;
            lstView.Items[lstView.Items.Count - 1].SubItems[4].Text = sInfo.Note;
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSubmit.Enabled = true;
        }

        private void FrmTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (acceptclose)
                return;
            if (MessageBox.Show("Nếu bạn thoát nghĩa là bạn sẽ kết thúc bài kiểm tra tại đây. Bạn có thật sự muốn thoát không?", "OOP Judge",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                == DialogResult.No)
            {
                e.Cancel = true;
                acceptclose = false;
            }
            else
            {
                e.Cancel = false;
                acceptclose = true;
                GlobalVar.MyNotifyIcon.Visible = false;
                Application.Exit();
            }
        }

        private void bgConnector_DoWork(object sender, DoWorkEventArgs e)
        {
            if (GlobalVar.RunMode != 1)
                return;

            WebClient client;
            ssLabel.Text = "Connecting ...";
            string update = "";
            try
            {
                //Thread.Sleep(3000);
                client = new WebClient();
                update = client.DownloadString(new Uri("http://fit.hcmup.edu.vn/~khanhndk/OOPJudge/upload.php?u="
                    + GlobalVar.StudentID));
                serveradress = "http://fit.hcmup.edu.vn/~khanhndk/OOPJudge/upload.php";
            }
            catch
            {
                ssLabel.Text = "Connect to another server!";
                try
                {
                    client = new WebClient();
                    update = client.DownloadString(new Uri("http://elearning.fit.hcmup.edu.vn/OOPJudge/upload.php?u="
                        + GlobalVar.StudentID));
                    serveradress = "http://elearning.fit.hcmup.edu.vn/OOPJudge/upload.php";
                }
                catch
                {
                    ssLabel.Text = "FAIL to connect to server!";
                    return;
                }
            }

            ssLabel.Text = "Connect to server SUCCESSFULLY";
            string zipdir = GlobalVar.AppDir + "\\Zip\\" + GlobalVar.CurrentLab.Name;
            if (!Directory.Exists(zipdir))
                return;

            string[] zipfiles = Directory.GetFiles(zipdir);

            UploadChecker checker = new UploadChecker();
            int countupload = 0;

            foreach (string zipfile in zipfiles)
            {
                if (checker.HasUpload(zipfile))
                {
                    countupload++;
                    continue;
                }

                string zipname = Path.GetFileName(zipfile);
                ssLabel.Text = "Uploading " + zipname + " ...";
                string result = "";
                try
                {
                    client = new WebClient();
                    string h = Utility.CalculateMD5Hash(zipfile);
                    string m = File.GetCreationTime(zipfile).ToString("yyyy-MM-dd-HH-mm-ss");
                    byte[] data = client.UploadFile(
                        serveradress + "?u=" + GlobalVar.StudentID + "&n=" + GlobalVar.StudentName + "&l=" + GlobalVar.CurrentLab.Name 
                        + "&m=" + m + "&h=" + h + "&r=" + GlobalVar.RoomCode, zipfile);
                    result = System.Text.Encoding.ASCII.GetString(data);
                }
                catch
                {
                    ssLabel.Text = "FAIL to upload " + zipname + " to server!";
                    continue;
                }

                if (result != "OK")
                {
                    ssLabel.Text = "FAIL to upload " + zipname + " to server!";
                    continue;
                }

                checker.AddFile(zipfile);
                checker.SaveLog();

                ssLabel.Text = "Upload " + zipname + " to server SUCCESSFULLY!";
                countupload++;
            }

            if (countupload == zipfiles.Length)
            {
                uploadfinished = true;
                ssLabel.Text = "ALL files has uploaded to server!";
            }
            else
            {
                uploadfinished = false;
                ssLabel.Text = "Upload to server INCOMPLETELY!";
            }
        }

        private void tUpload_Tick(object sender, EventArgs e)
        {
            if (bgConnector.IsBusy)
                return;

            if (remaintime <= 0)
                tUpload.Enabled = false;

            bgConnector.RunWorkerAsync();
        }

        private void bgConnector_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (remaintime <= 0)
            {
                if (uploadfinished)
                {
                    if ((frmEnd != null) && (!frmEnd.isClose))
                    {
                        frmEnd.isClose = true;
                        MessageBox.Show(this, "Đã upload thành công. Bạn KHÔNG cần phải nộp bài qua USB", "Finish");
                    }
                }
            }
        }
    }
}
