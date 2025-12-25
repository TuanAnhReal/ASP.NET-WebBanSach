using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBanSach
{
    public partial class Details : System.Web.UI.Page
    {

        [WebMethod(EnableSession = true)] // Bắt buộc EnableSession = true để truy cập Session
        public static string AddToCard(int maSach, string tenSach, double donGia, string anhBia)
        {
            try
            {
                // Lấy giỏ hàng từ Session
                List<CartItem> gioHang = System.Web.HttpContext.Current.Session["GioHang"] as List<CartItem>;
                if (gioHang == null) gioHang = new List<CartItem>();

                // Tìm xem sản phẩm đã có chưa
                var item = gioHang.FirstOrDefault(s => s.MaSach == maSach);
                if (item != null)
                {
                    item.SoLuong++;
                }
                else
                {
                    // Thêm mới và lưu cả AnhBia
                    gioHang.Add(new CartItem
                    {
                        MaSach = maSach,
                        TenSach = tenSach,
                        DonGia = donGia,
                        AnhBia = anhBia, // Lưu thông tin ảnh bìa để trang GioHang.aspx hiển thị được
                        SoLuong = 1
                    });
                }

                System.Web.HttpContext.Current.Session["GioHang"] = gioHang;

                // Trả về tổng số lượng để cập nhật Badge trên Navbar
                return gioHang.Sum(x => x.SoLuong).ToString();
            }
            catch (Exception)
            {
                return "error";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}