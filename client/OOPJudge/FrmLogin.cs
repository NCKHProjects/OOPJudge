using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OOPJudge
{
    public partial class FrmLogin : Form
    {
        public string StudentID;
        public string StudentName;
        public string RoomCode;
        public FrmLogin()
        {
            InitializeComponent();
        }

        public string ConvertVN(string chucodau)
        {
            const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            const string ReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
            int index = -1;
            char[] arrChar = FindText.ToCharArray();
            while ((index = chucodau.IndexOfAny(arrChar)) != -1)
            {
                int index2 = FindText.IndexOf(chucodau[index]);
                chucodau = chucodau.Replace(chucodau[index], ReplText[index2]);
            }
            return chucodau;
        } 

        private void btnStart_Click(object sender, EventArgs e)
        {
            StudentID = maskedTextBox1.Text;
            StudentName = ConvertVN(textBox2.Text);
            RoomCode = txtRoomCode.Text;
        }

        private void chkTestMode_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVar.UserMode = chkTestMode.Checked ? 1 : 0;
        }
    }
}
