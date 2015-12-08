namespace OOPJudge
{
    partial class FrmChoose
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChoose));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbLab = new System.Windows.Forms.ComboBox();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.btnBegin = new System.Windows.Forms.Button();
            this.lTimer = new System.Windows.Forms.Timer(this.components);
            this.btnReload = new System.Windows.Forms.Button();
            this.bgTestLoader = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::OOPJudge.Properties.Resources.target;
            this.pictureBox1.Location = new System.Drawing.Point(12, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(136, 135);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // cbLab
            // 
            this.cbLab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLab.FormattingEnabled = true;
            this.cbLab.Location = new System.Drawing.Point(163, 12);
            this.cbLab.Name = "cbLab";
            this.cbLab.Size = new System.Drawing.Size(231, 28);
            this.cbLab.TabIndex = 1;
            this.cbLab.SelectedIndexChanged += new System.EventHandler(this.cbLab_SelectedIndexChanged);
            // 
            // txtInfo
            // 
            this.txtInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfo.Location = new System.Drawing.Point(163, 46);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(321, 68);
            this.txtInfo.TabIndex = 2;
            this.txtInfo.Text = "Code name: T1\r\nTotal time: 60 mins\r\nEnd date: 10:57 PM 06/14/2014";
            // 
            // btnBegin
            // 
            this.btnBegin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnBegin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBegin.Location = new System.Drawing.Point(263, 123);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(120, 32);
            this.btnBegin.TabIndex = 3;
            this.btnBegin.Text = "Begin my test";
            this.btnBegin.UseVisualStyleBackColor = true;
            // 
            // lTimer
            // 
            this.lTimer.Enabled = true;
            this.lTimer.Interval = 1000;
            this.lTimer.Tick += new System.EventHandler(this.lTimer_Tick);
            // 
            // btnReload
            // 
            this.btnReload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReload.Location = new System.Drawing.Point(400, 12);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(84, 29);
            this.btnReload.TabIndex = 4;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // bgTestLoader
            // 
            this.bgTestLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgTestLoader_DoWork);
            this.bgTestLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgTestLoader_RunWorkerCompleted);
            // 
            // FrmChoose
            // 
            this.AcceptButton = this.btnBegin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 166);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.btnBegin);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.cbLab);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChoose";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose a lab";
            this.Load += new System.EventHandler(this.FrmChoose_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbLab;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Button btnBegin;
        private System.Windows.Forms.Timer lTimer;
        private System.Windows.Forms.Button btnReload;
        private System.ComponentModel.BackgroundWorker bgTestLoader;
    }
}