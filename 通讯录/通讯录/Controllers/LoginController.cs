using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 通讯录.Models;

namespace 通讯录.Controllers
{
    public class LoginController : Controller
    {
        Models.DBAddressBookEntities db = new Models.DBAddressBookEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            int n = db.Users.Where(u => u.UserName == username && u.PassWord == password).Count();
            if (n == 1)
            {
                var user = db.Users.Where(u => u.UserName == username).SingleOrDefault();
                Session["userid"] = user.Id;//将用户名放入session中
                return Redirect("/contact/index");
            }
            Response.Write("<script>alert('用户名或密码错误')</script>");
            return View();
        }
    }
}