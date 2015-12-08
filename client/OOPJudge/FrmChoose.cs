using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace OOPJudge
{
    public partial class FrmChoose : Form
    {
        public bool isReload = false;
        public string filename = "";
        public string pathfilename = "";
        public FrmChoose()
        {
            InitializeComponent();
        }

        private void FrmChoose_Load(object sender, EventArgs e)
        {
            LoadLabs();
        }

        private void LoadLabs()
        {
            cbLab.Items.Clear();
            for (int i = 0; i < GlobalVar.ListLabs.Count; i++)
            {
                cbLab.Items.Add(GlobalVar.ListLabs[i].FullName);
            }
            if (GlobalVar.ListLabs.Count > 0)
                cbLab.SelectedIndex = 0;
            if (filename != "")
            {
                for (int i = 0; i < GlobalVar.ListLabs.Count; i++)
                    if (Path.GetFileName(GlobalVar.ListLabs[i].Path) == filename)
                        cbLab.SelectedIndex = i;
            }
        }


        private void cbLab_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabInfo info = GlobalVar.ListLabs[cbLab.SelectedIndex];
            GlobalVar.CurrentLab = info;
            txtInfo.Text = String.Format("Code name: {0}\r\nTotal time: {1} mins\r\nEnd: {2}", info.Name, info.Elapse, info.End.ToString());
            if (GlobalVar.LabMode == 1)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void lTimer_Tick(object sender, EventArgs e)
        {
            if (isReload)
            {
                isReload = false;
                Program.LoadLabs();
                LoadLabs();
            }
        }

        private void bgTestLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            string serveraddress = "http://fit.hcmup.edu.vn/~khanhndk/OOPJudge/exam.php?r=" + GlobalVar.RoomCode;
            int count = 0;
            do
            {
                try
                {
                    string tmpFileName = string.Empty;
                    //Thread.Sleep(3000);
                    using (WebClient client = new WebClient())
                    {
                        using (Stream rawStream = client.OpenRead(serveraddress))
                        {
                            
                            string contentDisposition = client.ResponseHeaders["content-disposition"];
                            if (!string.IsNullOrEmpty(contentDisposition))
                            {
                                string lookFor = "filename=";
                                int index = contentDisposition.IndexOf(lookFor, StringComparison.CurrentCultureIgnoreCase);
                                if (index >= 0)
                                    tmpFileName = contentDisposition.Substring(index + lookFor.Length);
                            }
                            rawStream.Close();
                        }
                    }
                    if (tmpFileName.Length > 0)
                    {
                        pathfilename = "Lab\\" + tmpFileName;
                        WebClient wClient = new WebClient();
                        wClient.DownloadFile(serveraddress, pathfilename);
                    }
                    else
                        pathfilename = "";
                    break;
                }
                catch
                {
                    serveraddress = "http://elearning.fit.hcmup.edu.vn/OOPJudge/exam.php?r=" + GlobalVar.RoomCode;
                    count++;
                }
            } while (count < 2);
        }

        private void bgTestLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnReload.Enabled = true;
            if (pathfilename != "")
            {
                GlobalVar.LoadLab(pathfilename);
                LoadLabs();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            btnReload.Enabled = false;
            bgTestLoader.RunWorkerAsync();
        }
    }
}
