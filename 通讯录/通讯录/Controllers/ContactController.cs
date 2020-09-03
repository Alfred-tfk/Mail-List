using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using 通讯录.Models;

namespace 通讯录.Controllers
{
    public class ContactController : Controller
    {
        Models.DBAddressBookEntities db = new Models.DBAddressBookEntities();
        // GET: Contact
        public ActionResult Index()
        {
            List<Contact> contactlist = db.Contacts.OrderBy(u => u.Id).ToList();
            return View(contactlist);
        }
        //添加联系人页面显示
        [HttpGet]
        public ActionResult Add()
        {
            List<SelectListItem> lsgender = new List<SelectListItem>();
            lsgender.Add(new SelectListItem { Text = "--请选择性别--", Value = "0" });
            lsgender.Add(new SelectListItem { Text = "男", Value = "男" });
            lsgender.Add(new SelectListItem { Text = "女", Value = "女" });
            ViewBag.lsgender = lsgender;
            List<SelectListItem> lstgroupId = new List<SelectListItem>();
            List<Group> groupId = db.Groups.OrderBy(t => t.Id).ToList();
            lstgroupId.Add(new SelectListItem() { Text = "--请选择分组编号--", Value = "0" });
            for (int i = 0; i < groupId.Count; i++)
            {
                lstgroupId.Add(new SelectListItem() { Text = groupId[i].GroupId, Value = groupId[i].GroupId.ToString() });
            }
            ViewBag.lstgroupId = lstgroupId;
            return View();
        }
        [HttpPost]
        //添加联系人
        public ActionResult Add(Contact contact)
        {
            List<SelectListItem> lsgender = new List<SelectListItem>();
            lsgender.Add(new SelectListItem { Text = "--请选择性别--", Value = "0" });
            lsgender.Add(new SelectListItem { Text = "男", Value = "男" });
            lsgender.Add(new SelectListItem { Text = "女", Value = "女" });
            ViewBag.lsgender = lsgender;
            List<SelectListItem> lstgroupId = new List<SelectListItem>();
            List<Group> groupId = db.Groups.OrderBy(t => t.Id).ToList();
            lstgroupId.Add(new SelectListItem() { Text = "--请选择分组编号--", Value = "0" });
            for (int i = 0; i < groupId.Count; i++)
            {
                lstgroupId.Add(new SelectListItem() { Text = groupId[i].GroupId, Value = groupId[i].GroupId.ToString() });
            }
            ViewBag.lstgroupId = lstgroupId;
            Contact model = new Contact();
            db.Contacts.Add(contact);
            db.SaveChanges();
            return Redirect("/contact/index");
        }
        //删除分组
        public ActionResult Delete(int Id)
        {
            db.Contacts.Remove(db.Contacts.Find(Id));
            int n = db.SaveChanges();
            return Redirect("/contact/index");
        }
        //分组修改页面显示
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Contact oldContact = db.Contacts.Find(Id);
            List<SelectListItem> lsgender = new List<SelectListItem>();
            lsgender.Add(new SelectListItem { Text = "--请选择性别--", Value = "0" });
            lsgender.Add(new SelectListItem { Text = "男", Value = "男" });
            lsgender.Add(new SelectListItem { Text = "女", Value = "女" });
            ViewBag.lsgender = lsgender;
            List<SelectListItem> lstgroupId = new List<SelectListItem>();
            List<Group> groupId = db.Groups.OrderBy(t => t.Id).ToList();
            lstgroupId.Add(new SelectListItem() { Text = "--请选择分组编号--", Value = "0" });
            for (int i = 0; i < groupId.Count; i++)
            {
                lstgroupId.Add(new SelectListItem() { Text = groupId[i].GroupId, Value = groupId[i].GroupId.ToString() });
            }
            ViewBag.lstgroupId = lstgroupId;
            return View(oldContact);
        }
        //保存用户提交的修改信息
        public ActionResult Edit(Contact contact)
        {
            List<SelectListItem> lsgender = new List<SelectListItem>();
            lsgender.Add(new SelectListItem { Text = "--请选择性别--", Value = "0" });
            lsgender.Add(new SelectListItem { Text = "男", Value = "男" });
            lsgender.Add(new SelectListItem { Text = "女", Value = "女" });
            ViewBag.lsgender = lsgender;
            List<SelectListItem> lstgroupId = new List<SelectListItem>();
            List<Group> groupId = db.Groups.OrderBy(t => t.Id).ToList();
            lstgroupId.Add(new SelectListItem() { Text = "--请选择分组编号--", Value = "0" });
            for (int i = 0; i < groupId.Count; i++)
            {
                lstgroupId.Add(new SelectListItem() { Text = groupId[i].GroupId, Value = groupId[i].GroupId.ToString() });
            }
            ViewBag.lstgroupId = lstgroupId;           
            Contact oldcontact = db.Contacts.Find(contact.Id);
            oldcontact.Name = contact.Name;
            oldcontact.Gender = contact.Gender;
            oldcontact.Birthday = contact.Birthday;
            oldcontact.MobileNumber = contact.MobileNumber;
            oldcontact.HomeNumber = contact.HomeNumber;
            oldcontact.HomeAddress = contact.HomeAddress;
            oldcontact.QQ = contact.QQ;
            oldcontact.Email = contact.Email;
            oldcontact.MSN = contact.MSN;
            oldcontact.CompanyNumber = contact.CompanyNumber;
            oldcontact.CompanyAddress = oldcontact.CompanyAddress;
            oldcontact.GroupId = contact.GroupId;
            oldcontact.Remarks = contact.Remarks;
            int n = db.SaveChanges();
            if (n > 0)
            {
                return Redirect("/contact/index");
            }
           return View(contact);
        }
    }
}