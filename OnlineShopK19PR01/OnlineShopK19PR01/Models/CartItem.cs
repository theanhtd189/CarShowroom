using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopK19PR01.Models
{
    public class CartItem
    {
        public Product Product { set; get; }
        public int quantity { set; get; }
    }
}