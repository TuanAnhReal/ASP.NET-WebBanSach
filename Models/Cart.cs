using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class CartItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
    public class Cart
    {
        private List<CartItem> _items;
        public List<CartItem> Items
        {
            get { return _items; }
        }
        public Cart()
        {
            _items = new List<CartItem>();
        }
        public void AddItem(CartItem item)
        {
            var p = _items.Where(x => x.ID == item.ID).FirstOrDefault();
            if (p == null)
            {
                _items.Add(item);
            }
            else
            {
                p.Quantity++;
            }
        }
        public void Remove(CartItem item) {
            var p = _items.Where(x => x.ID == item.ID).FirstOrDefault();
            if (p != null)
            {
                _items.Remove(p);
            }
        }
        public void Update(int id, int soluong) {
            var p = _items.Where(x => x.ID == id).FirstOrDefault();
            if (p != null)
            {
                p.Quantity = soluong;
            }
        }
        public double Total
        {
            get { return 0; }
        }
    }
}