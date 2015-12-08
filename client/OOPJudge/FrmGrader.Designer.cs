namespace OOPJudge
{
    partial class FrmGrader
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnGrade = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose location:";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(104, 24);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.Size = new System.Drawing.Size(255, 20);
            this.txtLocation.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(365, 22);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnGrade
            // 
            this.btnGrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGrade.Location = new System.Drawing.Point(161, 64);
            this.btnGrade.Name = "btnGrade";
            this.btnGrade.Size = new System.Drawing.Size(144, 30);
            this.btnGrade.TabIndex = 3;
            this.btnGrade.Text = "Start grading";
            this.btnGrade.UseVisualStyleBackColor = true;
            this.btnGrade.Click += new System.EventHandler(this.btnGrade_Click);
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtLog.Location = new System.Drawing.Point(12, 113);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(443, 227);
            this.txtLog.TabIndex = 4;
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // FrmGrader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 352);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnGrade);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.label1);
            this.Name = "FrmGrader";
            this.Text = "OOP Grader";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmGrader_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnGrade;
        private System.Windows.Forms.TextBox txtLog;
        private System.ComponentModel.BackgroundWorker bgWorker;
    }
}