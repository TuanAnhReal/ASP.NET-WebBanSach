using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                Response.Write("<script>alert('Giỏ hàng của bạn đang trống!');</script>");
            }
        }
    }
}