using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using WebBanSach.Models;

namespace WebBanSach.Models
{
    public class SachDAO
    {
        public static Sach laySachTheoMa(int maSach)
        {
            Sach s = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BookStoreDBConnStr"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM SACH WHERE MaSach =" + maSach, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                s = new Sach { MaSach = maSach, TenSach = rd["tensach"].ToString(), AnhBia = rd["anhbia"].ToString(),
                DonGia = double.Parse(rd["dongia"].ToString())};
            }
            return s;
        }
    }
}