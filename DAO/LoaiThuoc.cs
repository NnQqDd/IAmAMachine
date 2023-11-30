using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmAMachine.DAO
{
    internal class LoaiThuoc
    {
        public LoaiThuoc() { }

        public int MaLoaiThuoc { get; set; }
        public string TenThuoc { get; set; }
        public string DonVi { get; set; }
        public string NhaSX { get; set; }
        public string CongDung { get; set; }
        public float GiaBan { get; set; }
        public int HanSD { get; set; }
        public string DangDieuChe { get; set; }
        public string Anh { get; set; }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
