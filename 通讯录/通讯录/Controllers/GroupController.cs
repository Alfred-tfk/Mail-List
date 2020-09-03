using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 通讯录.Models;

namespace 通讯录.Controllers
{
    public class GroupController : Controller
    {
        Models.DBAddressBookEntities db = new Models.DBAddressBookEntities();
        // GET: Group
        //显示分组列表
        public ActionResult Index()
        {
            List<Group> grouplist = db.Groups.OrderBy(u => u.Id).ToList();
            return View(grouplist);
        }
        //添加分组页面显示
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        //添加分组
        public ActionResult Add(string groupid, string groupname, string remarks)
        {
            Group model = new Group();
            model.GroupId = groupid;
            model.GroupName = groupname;
            model.Remarks = remarks;
            db.Groups.Add(model);
            db.SaveChanges();
            return Redirect("/group/index");
        }
        //删除分组
        public ActionResult Delete(int Id)
        {
            db.Groups.Remove(db.Groups.Find(Id));
            int n = db.SaveChanges();
            return Redirect("/group/index");
        }
        //分组修改页面显示
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Group oldgroup = db.Groups.Find(Id);
            return View(oldgroup);
        }
        //保存用户提交的修改信息
        public ActionResult Edit(Group group)
        {
            if (ModelState.IsValid)
            {
                Group oldgroup = db.Groups.Find(group.Id);
                oldgroup.GroupId = group.GroupId;
                oldgroup.GroupName = group.GroupName;
                oldgroup.Remarks = group.Remarks;
                int n = db.SaveChanges();
                if (n > 0)
                {
                    return Redirect("/group/index");
                }
            }
            return View(group);
        }
    }
}