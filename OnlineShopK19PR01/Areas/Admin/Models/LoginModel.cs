﻿namespace OnlineShopK19PR01.Areas.Admin.Models
{
    public class LoginModel
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Redirect { get; set; }
        public bool RememberMe { get; set; }
    }
}