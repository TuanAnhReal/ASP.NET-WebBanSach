using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBanSach
{
    public partial class Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //nếu không có mã chủ đề thì chuyển về trang default
                if (string.IsNullOrEmpty(Request.QueryString["MaCD"]))
                {
                    Response.Redirect("Default.aspx");
                }

                LoadPhanTrang();
            }
        }

        private void LoadPhanTrang()
        {
            string macd = Request.QueryString["MaCD"] ?? "1";
            //lấy số trang hiện tại
            int page = string.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : int.Parse(Request.QueryString["page"]);
            int pagesize = 6; //số sản phẩm trên mỗi trang

            //kết nối csdl
            string connStr = ConfigurationManager.ConnectionStrings["BookStoreDBConnStr"].ConnectionString;
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"SELECT *, COUNT(*) OVER() AS TongSoSach 
                                FROM Sach 
                                WHERE MaCD = @MaCD 
                                ORDER BY MaSach 
                                OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaCD", macd);
                cmd.Parameters.AddWithValue("@Offset", (page - 1) * pagesize);
                cmd.Parameters.AddWithValue("@Limit", pagesize);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // Lấy tổng số sách từ cột 'TotalRows' của dòng đầu tiên
                    int tongSoSach = Convert.ToInt32(dt.Rows[0]["TongSoSach"]);

                    rptSachTheoCD.DataSource = dt;
                    rptSachTheoCD.DataBind();

                    // Gọi hàm tạo nút phân trang
                    TaoNutPhanTrang(macd, page, pagesize, tongSoSach);
                }
                else
                {
                    rptSachTheoCD.DataSource = null;
                    rptSachTheoCD.DataBind();
                    ltrPhanTrang.Text = "";
                }
            }
        }

        private void TaoNutPhanTrang(string macd, int page, int pagesize, int tongSoSach)
        {
            int tongSoTrang = (int)Math.Ceiling((double)tongSoSach / pagesize);

            if (tongSoTrang <= 1)
            {
                ltrPhanTrang.Text = "";
                return;
            }

            StringBuilder sb = new StringBuilder();

            //Thêm nút trước
            if (page > 1)
            {
                sb.AppendFormat($"<li class='page-item'><a class='page-link' href='Product.aspx?MaCD={macd}&page={page - 1}'>&laquo; Trước</a></li>");
            }
            else
            {
                // Nếu đang ở trang 1 thì làm mờ nút Pre
                sb.Append("<li class='page-item disabled'><span class='page-link'>&laquo; Trước </span></li>");
            }

            //Lặp tạo các nút số trang (1, 2, 3...)
            for (int i = 1; i <= tongSoTrang; i++)
            {
                if (i == page)
                {
                    sb.AppendFormat($"<li class='page-item active'><span class='page-link'>{i}</span></li>");
                }
                else
                {
                    sb.AppendFormat($"<li class='page-item'><a class='page-link' href='Product.aspx?MaCD={macd}&page={i}'>{i}</a></li>");
                }
            }

            //Thêm nút sau
            if (page < tongSoTrang)
            {
                sb.AppendFormat($"<li class='page-item'><a class='page-link' href='Product.aspx?MaCD={macd}&page={page + 1}'>Sau &raquo;</a></li>");
            }
            else
            {
                // Nếu đang ở trang cuối thì làm mờ nút Next
                sb.Append("<li class='page-item disabled'><span class='page-link'>Sau &raquo;</span></li>");
            }

            ltrPhanTrang.Text = sb.ToString();
        }
    }
}