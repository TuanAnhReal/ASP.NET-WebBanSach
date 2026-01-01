using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class Sach
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public string TacGia { get; set; }
        public double DonGia { get; set; }
        public string AnhBia { get; set; }
        public int SoLuongTon { get; set; }
    }
}