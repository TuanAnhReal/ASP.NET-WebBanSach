using System;
using System.Collections.Generic;
using System.Linq;
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
            }
        }
    }
}