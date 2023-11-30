using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IAmAMachine.DAO
{
    internal class ChiTietHDB
    {

        public HoaDonBan HoaDonBan { get; set; }

        public List<Thuoc> thuoclst { get; set; }

        public string soluong { get; set; }

        public string vat { get; set; }

        public string km { get; set; } 

    }
}
