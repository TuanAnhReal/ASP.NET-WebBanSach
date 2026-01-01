using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBanSach.Models;

namespace WebBanSach
{
    public partial class Details : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            int maSach = int.Parse(((LinkButton)sender).CommandArgument);

            Cart cart = null;//tao gio hang moi
            if(Session["CART"] == null)//chua co gio hang
            {
                cart = new Cart();
                Session["CART"] = cart;
            }
            else//da co gio hang
            {
                cart = (Cart)Session["CART"];
            }

            //truy xuat sql de lay thong tin sp
            Sach s = SachDAO.laySachTheoMa(maSach);

            //tao mot cart item tu sach lay duoc
            CartItem item = new CartItem
            {
                ID = s.MaSach,
                Name = s.TenSach,
                Picture = s.AnhBia,
                Price = s.DonGia,
                Quantity = 1
            };
            //them cart item vao gio hang
            cart.AddItem(item);
        }
    }
}