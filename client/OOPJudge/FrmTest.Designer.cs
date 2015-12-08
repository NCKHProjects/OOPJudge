namespace OOPJudge
{
    partial class FrmTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTest));
            this.txtStudentName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStudentID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLabName = new System.Windows.Forms.TextBox();
            this.lblTimer = new System.Windows.Forms.Label();
            this.lstView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSubmit = new System.Windows.Forms.Button();
            this.tLab = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.sStrip = new System.Windows.Forms.StatusStrip();
            this.ssLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.bgConnector = new System.ComponentModel.BackgroundWorker();
            this.tUpload = new System.Windows.Forms.Timer(this.components);
            this.sStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtStudentName
            // 
            this.txtStudentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStudentName.Location = new System.Drawing.Point(124, 11);
            this.txtStudentName.Name = "txtStudentName";
            this.txtStudentName.ReadOnly = true;
            this.txtStudentName.Size = new System.Drawing.Size(493, 26);
            this.txtStudentName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Student Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Student ID:";
            // 
            // txtStudentID
            // 
            this.txtStudentID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStudentID.Location = new System.Drawing.Point(124, 43);
            this.txtStudentID.Name = "txtStudentID";
            this.txtStudentID.ReadOnly = true;
            this.txtStudentID.Size = new System.Drawing.Size(493, 26);
            this.txtStudentID.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Lab Name:";
            // 
            // txtLabName
            // 
            this.txtLabName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLabName.Location = new System.Drawing.Point(124, 75);
            this.txtLabName.Name = "txtLabName";
            this.txtLabName.ReadOnly = true;
            this.txtLabName.Size = new System.Drawing.Size(493, 26);
            this.txtLabName.TabIndex = 4;
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.Location = new System.Drawing.Point(208, 145);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(212, 55);
            this.lblTimer.TabIndex = 6;
            this.lblTimer.Text = "00:00:00";
            // 
            // lstView
            // 
            this.lstView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.colStatus,
            this.columnHeader4,
            this.columnHeader3});
            this.lstView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstView.FullRowSelect = true;
            this.lstView.Location = new System.Drawing.Point(12, 203);
            this.lstView.Name = "lstView";
            this.lstView.Size = new System.Drawing.Size(605, 182);
            this.lstView.TabIndex = 1;
            this.lstView.UseCompatibleStateImageBehavior = false;
            this.lstView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            this.columnHeader1.Width = 141;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.Width = 63;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 97;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Result";
            this.columnHeader4.Width = 51;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Note";
            this.columnHeader3.Width = 234;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(442, 451);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(175, 38);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "Submit new solution";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // tLab
            // 
            this.tLab.Enabled = true;
            this.tLab.Interval = 1000;
            this.tLab.Tick += new System.EventHandler(this.tLab_Tick);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 391);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(605, 48);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "Click \"Submit new solution\" để nộp bài. Nén toàn bộ thư mục chứa toàn bộ project " +
    "ở dạng file .ZIP và nộp tại đây";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Project Name:";
            // 
            // txtProjectName
            // 
            this.txtProjectName.BackColor = System.Drawing.SystemColors.Info;
            this.txtProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjectName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtProjectName.Location = new System.Drawing.Point(124, 107);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.ReadOnly = true;
            this.txtProjectName.Size = new System.Drawing.Size(493, 26);
            this.txtProjectName.TabIndex = 8;
            this.txtProjectName.Text = "K31103999_T1";
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // sStrip
            // 
            this.sStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssLabel,
            this.ssProgress});
            this.sStrip.Location = new System.Drawing.Point(0, 499);
            this.sStrip.Name = "sStrip";
            this.sStrip.Size = new System.Drawing.Size(629, 22);
            this.sStrip.SizingGrip = false;
            this.sStrip.TabIndex = 10;
            this.sStrip.Text = "statusStrip1";
            // 
            // ssLabel
            // 
            this.ssLabel.Name = "ssLabel";
            this.ssLabel.Size = new System.Drawing.Size(39, 17);
            this.ssLabel.Text = "Ready";
            // 
            // ssProgress
            // 
            this.ssProgress.Name = "ssProgress";
            this.ssProgress.Size = new System.Drawing.Size(100, 16);
            this.ssProgress.Visible = false;
            // 
            // bgConnector
            // 
            this.bgConnector.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgConnector_DoWork);
            this.bgConnector.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgConnector_RunWorkerCompleted);
            // 
            // tUpload
            // 
            this.tUpload.Enabled = true;
            this.tUpload.Interval = 10000;
            this.tUpload.Tick += new System.EventHandler(this.tUpload_Tick);
            // 
            // FrmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 521);
            this.Controls.Add(this.sStrip);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.lstView);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLabName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStudentID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStudentName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OOP Judge";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTest_FormClosing);
            this.Load += new System.EventHandler(this.FrmTest_Load);
            this.sStrip.ResumeLayout(false);
            this.sStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtStudentName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStudentID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLabName;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.ListView lstView;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Timer tLab;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.StatusStrip sStrip;
        private System.Windows.Forms.ToolStripStatusLabel ssLabel;
        private System.Windows.Forms.ToolStripProgressBar ssProgress;
        private System.ComponentModel.BackgroundWorker bgConnector;
        private System.Windows.Forms.Timer tUpload;
    }
}