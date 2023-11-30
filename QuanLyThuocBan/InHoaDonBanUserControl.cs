using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using IAmAMachine.DAO;
using SQLAppDemo;
using ZXing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IAmAMachine.UserControlsOfQuanLyThuocBan
{
    public partial class InHoaDonBanUserControl : UserControl
    {
        ProcessDatabase pd = new ProcessDatabase();
        int index;
        List<ChiTietHDB> cthdblist ;
        public InHoaDonBanUserControl()
        {
            InitializeComponent();
        }

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        private void tenDangNhapTxt_TextChanged(object sender, EventArgs e)
        {
            detail(txtMaThuoc.Text);
        }

        private void InHoaDonBanUserControl_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filterInfoCollection)
                cbCamera.Items.Add(device.Name);
            cbCamera.SelectedIndex = 0;

            txtVat.Text = "10";
            txtKM.Text = "0";
            index = 0;
            cthdblist = new List<ChiTietHDB>();
        }

        private void btnQR_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cbCamera.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }

        private void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            BarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap);
            if (result != null)
            {
                txtMaThuoc.Invoke(new MethodInvoker(delegate ()
                {
                    txtMaThuoc.Text = result.ToString();
                    detail(txtMaThuoc.Text);
                }));
            }
            pictureBox1.Image = bitmap;
        }

        private void btnDungQR_Click(object sender, EventArgs e)
        {
            if (videoCaptureDevice != null)
            {
                if (videoCaptureDevice.IsRunning)
                    videoCaptureDevice.Stop();
            }
        }

        private void detail(string malo)
        {
            string sqlmalo = "select top(1) t.MaLoaiThuoc,MaThuoc,TenThuoc,GiaBan,t.MaLo from ChiTietHDN cthdn join Thuoc t on cthdn.MaLo = t.MaLo inner join LoaiThuoc\r\non t.MaLoaiThuoc = LoaiThuoc.MaLoaiThuoc where t.MaLo = " + malo;
            DataTable thuoc = pd.ReadTable(sqlmalo);
            if(thuoc.Rows.Count != 0)
            {
                DataRow thuoc1 = thuoc.Rows[0];
                txtTenThuoc.Text = thuoc1[2].ToString();
                txtGia.Text = thuoc1[3].ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sl = textBox1.Text;
            string sqlmalo = "select top("+sl+ ") t.MaLoaiThuoc,MaThuoc,TenThuoc,GiaBan,t.MaLo from ChiTietHDN cthdn join Thuoc t on cthdn.MaLo = t.MaLo inner join LoaiThuoc on t.MaLoaiThuoc = LoaiThuoc.MaLoaiThuoc where t.MaLo = " + txtMaThuoc.Text;
            DataTable thuoc = pd.ReadTable(sqlmalo);
            string cthdb_hienthi = txtTenThuoc.Text + "\t" + sl + "\t" + txtGia.Text + "\n";
            rtbHDB.AppendText(cthdb_hienthi);
            
            //string manv = 

        }

        private void txtSdt_TextChanged(object sender, EventArgs e)
        {
            string text = txtSdt.Text;

            // Kiểm tra xem nội dung có phải là số hay không
            if (!string.IsNullOrWhiteSpace(text) && !int.TryParse(text, out _))
            {
                label10.Text = "Hãy nhập số";
                // Nếu nội dung không phải số, xóa ký tự cuối cùng vừa được nhập vào
                txtSdt.Text = text.Substring(0, text.Length - 1);
                txtSdt.SelectionStart = txtSdt.Text.Length;
            }
            else
            {
                string sqlsdt = "select top(1) * from KhachHang where KhachHang.SoDienThoai = " + txtSdt.Text;
                if (!text.Trim().Equals(""))
                {
                    DataTable kh = pd.ReadTable(sqlsdt);
                    if (kh.Rows.Count != 0)
                    {
                        txtKM.Text = "5";
                    }
                    else
                    {
                        txtKM.Text = "0";
                    }
                }
            }
        }

        private void txtGia_TextChanged(object sender, EventArgs e)
        {
            string text = txtGia.Text;

            // Kiểm tra xem nội dung có phải là số hay không
            if (!string.IsNullOrWhiteSpace(text) && !int.TryParse(text, out _))
            {
                label9.Text = "Hãy nhập số";
                // Nếu nội dung không phải số, xóa ký tự cuối cùng vừa được nhập vào
                txtGia.Text = text.Substring(0, text.Length - 1);
                txtGia.SelectionStart = txtSdt.Text.Length;
            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            string text = txtVat.Text;

            // Kiểm tra xem nội dung có phải là số hay không
            if (!string.IsNullOrWhiteSpace(text) && !int.TryParse(text, out _))
            {
                label11.Text = "Hãy nhập số";
                // Nếu nội dung không phải số, xóa ký tự cuối cùng vừa được nhập vào
                txtVat.Text = text.Substring(0, text.Length - 1);
                txtVat.SelectionStart = txtSdt.Text.Length;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = textBox1.Text;

            // Kiểm tra xem nội dung có phải là số hay không
            if (!string.IsNullOrWhiteSpace(text) && !int.TryParse(text, out _))
            {
                label18.Text = "Hãy nhập số";
                // Nếu nội dung không phải số, xóa ký tự cuối cùng vừa được nhập vào
                textBox1.Text = text.Substring(0, text.Length - 1);
                textBox1.SelectionStart = txtSdt.Text.Length;
            }
        }
    }
}
