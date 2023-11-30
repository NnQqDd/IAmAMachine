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
    public partial class QuanLyHoaDonNhapUserControl : UserControl
    {
        ProcessDatabase pd = new ProcessDatabase();
        public QuanLyHoaDonNhapUserControl()
        {
            InitializeComponent();
            string sqlhdn = "select MaHDN,HoVaTen,TenNCC from HoaDonNhap inner join NhanVien on HoaDonNhap.MaNV = NhanVien.MaNV inner join NhaCungCap on NhaCungCap.MaNCC = HoaDonNhap.MaNCC";
            DataTable data = pd.ReadTable(sqlhdn);
            dataGridView1.DataSource = data;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
