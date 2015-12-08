using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OOPJudge
{
    public partial class FrmTerm : Form
    {
        public FrmTerm()
        {
            InitializeComponent();
        }
        Form frm;
        BackgroundWorker worker;
        private void btnAccept_Click(object sender, EventArgs e)
        {
            btnAccept.Enabled = false;
            frm = new FrmChkUpdate();
            frm.Show(this);
            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
            tUpdate.Enabled = true;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tUpdate.Enabled = false;
            string update = e.Result.ToString();
            if ((update == "yes")||(update == "lab"))
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo("Updater.v2.exe");
                if (update == "lab")
                    p.StartInfo.Arguments = "-u " + GlobalVar.LabVersion.ToString();
                p.Start();
                Application.Exit();
            }
            else
            {
                frm.Close();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = "";
            try
            {                
                //Thread.Sleep(3000);
                WebClient client = new WebClient();
                string update = client.DownloadString(new Uri("http://fit.hcmup.edu.vn/~khanhndk/OOPJudge/?u=" + GlobalVar.Version.ToString() 
                    + "&l=" + GlobalVar.LabVersion.ToString() ));
                e.Result = update;
            }
            catch
            {

            }
            if ((e.Result.ToString() != "yes") && (e.Result.ToString() != "lab") && (e.Result.ToString() != "no"))
            {
                try
                {
                    //Thread.Sleep(3000);
                    WebClient client = new WebClient();
                    string update = client.DownloadString(new Uri("http://khanhndkhcmup.appspot.com/OOPJudge.jsp?u=" + GlobalVar.Version.ToString()
                       + "&l=" + GlobalVar.LabVersion.ToString()));
                    e.Result = update;
                }
                catch
                { }
            }
        }

        private void tUpdate_Tick(object sender, EventArgs e)
        {
            if (worker.IsBusy)
            {
                frm.Close();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
