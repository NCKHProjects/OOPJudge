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
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OOPJudge
{
    public partial class FrmGrader : Form
    {
        public FrmGrader()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtLocation.Text = dlg.SelectedPath;
            }
        }

        private void btnGrade_Click(object sender, EventArgs e)
        {
            btnGrade.Enabled = false;
            bgWorker.RunWorkerAsync(txtLocation.Text);
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;

            StreamWriter wResult = new StreamWriter("result.txt", true, Encoding.UTF8, 10);
            StreamWriter wDetail = new StreamWriter("detail.txt", true, Encoding.UTF8, 10);

            string studentid = "";
            string[] stFolderList = Directory.GetDirectories(e.Argument.ToString());
            foreach (string stFolder in stFolderList)
            {
                studentid = new DirectoryInfo(stFolder).Name;
                worker.ReportProgress(1, "Grade for " + studentid);
                wResult.Write(studentid);

                string[] zipfiles = Directory.GetFiles(stFolder, "*.zip", SearchOption.TopDirectoryOnly);

                foreach (string zipfile in zipfiles)
                {
                    FileInfo fInfo = new FileInfo(zipfile);
                    string unzipdir = stFolder + "\\" + Path.GetFileNameWithoutExtension(zipfile);
                    if (!Directory.Exists(unzipdir))
                        Directory.CreateDirectory(unzipdir);
                    if (fInfo.Length > 0)
                    {
                        try
                        {
                            using (ZipFile zip = ZipFile.Read(zipfile))
                            {
                                foreach (ZipEntry entry in zip)
                                {
                                    try
                                    {
                                        if ((entry.FileName == "Updater.v2.exe") || (entry.FileName == "Ionic.Zip.Reduced.dll"))
                                            continue;
                                        entry.Extract(unzipdir, ExtractExistingFileAction.OverwriteSilently);
                                    }
                                    catch (Exception ex)
                                    {
                                        worker.ReportProgress(1, zipfile + ":" + ex.Message);
                                        continue;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            worker.ReportProgress(1, zipfile + ":" + ex.Message);
                            continue;
                        }
                    }
                }

                string[] sList = Directory.GetDirectories(stFolder);
                foreach (string storedir in sList)
                {
                    string dirname = new DirectoryInfo(storedir).Name;

                    string submissionid = dirname;

                    worker.ReportProgress(1, submissionid);
                    
                    string source = "";
                    source += "using System;";
                    source += "using System.Text;";

                    bool isFound = true;
                    for (int i = 0; i < GlobalVar.CurrentLab.Files.Length; i++)
                    {
                        string[] result = Directory.GetFiles(storedir, GlobalVar.CurrentLab.Files[i], SearchOption.AllDirectories);
                        if (result.Length == 0)
                        {
                            worker.ReportProgress(1, GlobalVar.CurrentLab.Files[i] + " not found");
                            wDetail.WriteLine(studentid + "\t0\t" + GlobalVar.CurrentLab.Files[i] + " not found");
                            isFound = false;
                            break;
                        }
                        Utility.FileProcess(result[0], ref source);
                    }

                    if (!isFound)
                    {
                        wResult.Write("\t0");
                        worker.ReportProgress(1, "...........");
                        continue;
                    }


                    worker.ReportProgress(1, "Compiling...");
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

                    CompilerResults cr = codeProvider.CompileAssemblyFromSource(option, source);
                    if (cr.Errors.Count > 0)
                    {
                        worker.ReportProgress(1, "Compile error");
                        wResult.Write("\t0");
                        wDetail.WriteLine(studentid + "\t0\tCompile error");
                        worker.ReportProgress(1, "...........");
                        continue;
                    }

                    worker.ReportProgress(1, "Running...");
                    Thread.Sleep(1000);
                    Process p = new Process();
                    ProcessStartInfo info = new ProcessStartInfo(GlobalVar.CurrentLab.Path);
                    info.Arguments = submissionid + ".dll -g";
                    info.UseShellExecute = false;
                    info.RedirectStandardOutput = true;
                    info.RedirectStandardInput = true;
                    p.StartInfo = info;
                    p.Start();
                    p.WaitForExit(5000);
                    if (!p.HasExited)
                    {
                        worker.ReportProgress(30, "Finished:0");
                        wResult.Write("\t0");
                        wDetail.WriteLine(studentid + "\t0\tInfinite loop");

                        p.Kill();
                    }
                    else
                    {
                        string output = p.StandardOutput.ReadToEnd();
                        string[] stArr = output.Split(new char[] { '@' });
                        if (stArr.Length > 1)
                        {
                            worker.ReportProgress(1, "Finished:" + stArr[0] + "(" + stArr[1] + ")");
                            wResult.Write("\t" + stArr[0]);
                            wDetail.WriteLine(studentid + "\t" + stArr[0] + "\t" + stArr[1]);
                        }
                        else
                        {
                            worker.ReportProgress(1, "Finished:" + stArr[0]);
                            wResult.Write("\t0");
                            wDetail.WriteLine(studentid + "\t0\t" + stArr[0]);
                        }

                        wResult.Flush();
                        wDetail.Flush();
                    }

                    worker.ReportProgress(1, "...........");
                }

                worker.ReportProgress(1, "*************************");
                wResult.WriteLine();
            }

            wResult.Close();
            wDetail.Close();
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtLog.AppendText(e.UserState.ToString() + "\r\n");
        }

        private void FrmGrader_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnGrade.Enabled = true;
        }
    }
}
