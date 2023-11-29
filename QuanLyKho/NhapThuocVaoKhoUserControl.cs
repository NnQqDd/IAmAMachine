using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IAmAMachine.QuanLyKho;

namespace IAmAMachine.UserControlsOfQuanLyKho
{
    public partial class NhapThuocVaoKhoUserControl : UserControl
    {
        UserControl themNCCUserControl = new ThemNCCUserControl();
        public NhapThuocVaoKhoUserControl()
        {
            InitializeComponent();

            this.Controls.Add(themNCCUserControl);
            themNCCUserControl.SendToBack();
        }

        private void xemDanhSachBtn_Click(object sender, EventArgs e)
        {
            themNCCUserControl.SendToBack();
        }

        private void themNCCBtn_Click(object sender, EventArgs e)
        {
            themNCCUserControl.BringToFront();
        }
    }
}
