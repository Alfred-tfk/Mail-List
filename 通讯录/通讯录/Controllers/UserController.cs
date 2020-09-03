using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 通讯录.Models;

namespace 通讯录.Controllers
{
    public class UserController : Controller
    {
        Models.DBAddressBookEntities db = new Models.DBAddressBookEntities();
        // GET: User
        //用户列表
        public ActionResult Index()
        {
            List<User> userlist = db.Users.OrderBy(u => u.Id).ToList();
            return View(userlist);
        }
        //添加用户页面显示
        [HttpGet]
        public ActionResult Add()
        {
            List<SelectListItem> lstrole = new List<SelectListItem>();
            lstrole.Add(new SelectListItem { Text = "--请选择角色--", Value = "0" });
            lstrole.Add(new SelectListItem { Text = "普通用户", Value = "普通用户" });
            lstrole.Add(new SelectListItem { Text = "管理员", Value = "管理员" });
            ViewBag.lstrole = lstrole;
            return View();
        }
        [HttpPost]
        //添加用户
        public ActionResult Add(string username, string password, string role)
        {
            User model = new User();
            model.UserName = username;
            model.PassWord = password;
            model.Role = role;      
            db.Users.Add(model);
            db.SaveChanges();
            return Redirect("/user/index");
        }
        //删除用户
        public ActionResult Delete(int Id)
        {           
            db.Users.Remove(db.Users.Find(Id));
            int n = db.SaveChanges();
            return Redirect("/user/index");
        }
        //用户修改页面显示
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            User olduser = db.Users.Find(Id);
            List<SelectListItem> lstrole = new List<SelectListItem>();
            lstrole.Add(new SelectListItem { Text = "--请选择角色--", Value = "0" });
            lstrole.Add(new SelectListItem { Text = "普通用户", Value = "普通用户" });
            lstrole.Add(new SelectListItem { Text = "管理员", Value = "管理员" });
            ViewBag.lstrole = lstrole;
            return View(olduser);
        }
        //保存用户提交的修改信息
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                User olduser = db.Users.Find(user.Id);
                olduser.UserName = user.UserName;
                olduser.PassWord = user.PassWord;
                olduser.Role = user.Role;
                int n = db.SaveChanges();
                if (n > 0)
                {
                    return Redirect("/user/index");
                }
            }
            return View(user);
        }
    }
}