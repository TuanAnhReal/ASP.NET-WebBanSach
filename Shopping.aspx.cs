using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBanSach
{
    public partial class Shopping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGioHang();
            }
        }
        private void LoadGioHang()
        {
            // Lấy giỏ hàng từ Session
            List<CartItem> dsGioHang = Session["GioHang"] as List<CartItem>;

            if (dsGioHang != null && dsGioHang.Count > 0)
            {
                // Đổ dữ liệu vào Repeater
                rptGioHang.DataSource = dsGioHang;
                rptGioHang.DataBind();

                // Tính tổng tiền
                double tongTien = dsGioHang.Sum(x => x.ThanhTien);
                ltrTongTien.Text = tongTien.ToString("#,##0");
            }
            else
            {
                // Nếu giỏ hàng trống, có thể ẩn bảng và hiện thông báo
                //sử dụng ClientScript.RegisterStartupScript thay vì Response.Write tránh tình trạng chạy trước khi page load
                ClientScript.RegisterStartupScript(
                this.GetType(),
                    "EmptyCart",
                        @"
                        Swal.fire({
                            icon: 'warning',
                            title: 'Thông báo',
                            text: 'Chưa có sách được thêm!',
                            confirmButtonText: 'OK'
                        });
                        ",
                        true
                );
            }
        }

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

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object CapNhatSoLuong(int maSach, int soLuong, int soLuongMoi)
        {
            var gioHang = System.Web.HttpContext.Current.Session["GioHang"] as List<CartItem>;
            if (gioHang != null)
            {
                var item = gioHang.FirstOrDefault(x => x.MaSach == maSach);
                if (item != null)
                {
                    item.SoLuong = soLuongMoi;
                    return new
                    {
                        thanhTien = item.ThanhTien.ToString("#,##0"),
                        tongTien = gioHang.Sum(x => x.ThanhTien).ToString("#,##0"),
                        tongSL = gioHang.Sum(x => x.SoLuong)
                    };
                }
            }
            return "error";
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object XoaSanPham(int maSach)
        {
            var gioHang = System.Web.HttpContext.Current.Session["GioHang"] as List<CartItem>;
            if (gioHang != null)
            {
                gioHang.RemoveAll(x => x.MaSach == maSach);
                return new
                {
                    tongTien = gioHang.Sum(x => x.ThanhTien).ToString("#,##0"),
                    tongSL = gioHang.Sum(x => x.SoLuong)
                };
            }
            return "error";
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static bool XoaToanBo()
        {
            System.Web.HttpContext.Current.Session["GioHang"] = null;
            return true;
        }
    }
}