using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OOPJudge
{
    public partial class FrmEnd : Form
    {
        public bool isClose = false;
        public FrmEnd()
        {
            InitializeComponent();
        }

        private void tClose_Tick(object sender, EventArgs e)
        {
            if (isClose)
                this.Close();
        }
    }
}
