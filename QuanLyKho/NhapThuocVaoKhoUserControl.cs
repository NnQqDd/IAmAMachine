using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IAmAMachine.DAO;
using IAmAMachine.QuanLyKho;
using SQLAppDemo;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IAmAMachine.UserControlsOfQuanLyKho
{
    public partial class NhapThuocVaoKhoUserControl : UserControl
    {
        UserControl themNCCUserControl = new ThemNCCUserControl();
        ProcessDatabase pd = new ProcessDatabase();
        public NhapThuocVaoKhoUserControl()
        {
            InitializeComponent();

            this.Controls.Add(themNCCUserControl);
            themNCCUserControl.SendToBack();

            DataTable thuoclst = pd.ReadTable("select * from LoaiThuoc");
            DataTable ncclst = pd.ReadTable("select * from NhaCungCap");
            comboBox1.DataSource = ncclst;
            comboBox1.DisplayMember = "TenNCC";
            comboBox1.ValueMember = "MaNCC";
            cbThuoc.DataSource = thuoclst;
            cbThuoc.DisplayMember = "TenThuoc";
            cbThuoc.ValueMember = "MaLoaiThuoc";

      }

        private void xemDanhSachBtn_Click(object sender, EventArgs e)
        {
            themNCCUserControl.SendToBack();
        }

        private void themNCCBtn_Click(object sender, EventArgs e)
        {
            themNCCUserControl.BringToFront();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        List<ChiTietHDN> chiTietHDNs = new List<ChiTietHDN>();

        private void button2_Click(object sender, EventArgs e)
        {
            if(chiTietHDNs.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu về chi tiết hóa đơn");
                return;
            }

            if (MessageBox.Show("Xác nhận nhập hàng và xuất hóa đơn", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Them Hoa don ban
                string mancc = comboBox1.SelectedValue.ToString();
                string manv = "1";
                string ngaynhap = dateTimePicker1.Value.ToString();
                string sqlhdn = "insert into HoaDonNhap values(" + manv + "," + mancc + ",'" + ngaynhap + "')";
                pd.UpdateData(sqlhdn);

                //Them Chi tiet hoa don ban
                
                for(int i = 0; i < chiTietHDNs.Count; i++)
                {
                    string sqlcthdn = "insert into ChiTietHDN values(" + chiTietHDNs[i].MaHDN + "," + chiTietHDNs[i].MaLoaiThuoc + "," + chiTietHDNs[i].SoLuong + "," + chiTietHDNs[i].DonGia  +")";
                    pd.UpdateData(sqlcthdn);
                    for(int j=0;j<int.Parse(chiTietHDNs[i].SoLuong); j++)
                    {
                        int malo = pd.GetIntResult("select top(1) MaLo  from ChiTietHDN Order by MaLo desc");
                        string sqlthuoc = "insert into Thuoc values(" + chiTietHDNs[i].MaLoaiThuoc + ",'" + chiTietHDNs[i].NgaySx + "'," + malo.ToString() +")";
                        pd.UpdateData(sqlthuoc);
                    }
                }


            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DataRowView selectedRow = (DataRowView)cbThuoc.SelectedItem;
            string selectedName = selectedRow[1].ToString();
            string s = selectedName + "\t" + textBox4.Text + "\t"
                + dateTimePicker2.Value.ToString() + "\t" + textBox1.Text+"\n";
            rtnHoaDonNhap.AppendText(s);

            lblTongTienNhap.Text = (int.Parse(textBox1.Text) * int.Parse(textBox4.Text)).ToString();

            int mahdb = pd.GetIntResult("select top(1) MaHDN  from HoaDonNhap Order by MaHDN desc");
            ChiTietHDN chiTietHDN = new ChiTietHDN();
            chiTietHDN.MaHDN = mahdb.ToString();
            chiTietHDN.MaLoaiThuoc = cbThuoc.SelectedValue.ToString();
            chiTietHDN.DonGia = textBox1.Text;
            chiTietHDN.SoLuong = textBox4.Text;
            chiTietHDN.NgaySx = dateTimePicker2.Value.ToString();
            chiTietHDNs.Add(chiTietHDN);

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void rtnHoaDonNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string text = textBox4.Text;

            // Kiểm tra xem nội dung có phải là số hay không
            if (!string.IsNullOrWhiteSpace(text) && !int.TryParse(text, out _))
            {
                label10.Text = "Hãy nhập số";
                // Nếu nội dung không phải số, xóa ký tự cuối cùng vừa được nhập vào
                textBox4.Text = text.Substring(0, text.Length - 1);
                textBox4.SelectionStart = textBox4.Text.Length;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = textBox1.Text;

            // Kiểm tra xem nội dung có phải là số hay không
            if (!string.IsNullOrWhiteSpace(text) && !int.TryParse(text, out _))
            {
                label15.Text = "Hãy nhập số";
                // Nếu nội dung không phải số, xóa ký tự cuối cùng vừa được nhập vào
                textBox1.Text = text.Substring(0, text.Length - 1);
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cbThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
