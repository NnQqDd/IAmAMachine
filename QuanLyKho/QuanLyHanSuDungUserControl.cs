using SQLAppDemo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IAmAMachine.UserControlsOfQuanLyKho
{
    public partial class QuanLyHanSuDungUserControl : UserControl
    {
        ProcessDatabase pd = new ProcessDatabase();
        public QuanLyHanSuDungUserControl()
        {
            InitializeComponent();
            string sqltheohsd = "select *,DATEADD(MONTH,lt.HanSD,t.NgaySX) as HSD from Thuoc t inner join LoaiThuoc lt on t.MaLoaiThuoc = lt.MaLoaiThuoc order by HSD desc";
            DataTable data = pd.ReadTable(sqltheohsd);
            dataGridView1.DataSource = data;
                   
         }

        private void btnXoaHetHan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xóa thuốc hết hạn", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlXoahh = "delete from Thuoc where Thuoc.MaLo in (SELECT ChiTietHDN.MaLo FROM ChiTietHDN INNER JOIN Thuoc ON ChiTietHDN.MaLo = Thuoc.MaLo INNER JOIN LoaiThuoc ON LoaiThuoc.MaLoaiThuoc = Thuoc.MaLoaiThuoc WHERE DATEADD(month, LoaiThuoc.HanSD, Thuoc.NgaySX) < GETDATE() GROUP BY ChiTietHDN.MaLo, NgaySX)";
                pd.UpdateData(sqlXoahh);
                MessageBox.Show("Đã xóa thuốc hết hạn");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
