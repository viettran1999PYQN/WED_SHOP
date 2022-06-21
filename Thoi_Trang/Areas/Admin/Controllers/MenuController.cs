using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace Thoi_Trang.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        CategoryDAO categoryDAO = new CategoryDAO();
        TopicDAO topicDAO = new TopicDAO();
        PostDAO postDAO = new PostDAO();
        SupplierDAO supplierDAO = new SupplierDAO();
        MenuDAO menuDAO = new MenuDAO();
        // GET: Admin/Menu
        public ActionResult Index()
        {
            ViewBag.ListCategory = categoryDAO.getList("Index");
            ViewBag.ListTopic = topicDAO.getList("Index");
            ViewBag.ListPage = postDAO.getList("Index", "Page");
            List<Menu> menu = menuDAO.getList("Index");
            return View("Index", menu);
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            if (!string.IsNullOrEmpty(form["ThemCategory"]))
            {
                if (!string.IsNullOrEmpty(form["itemcat"]))
                {
                    var listItem = form["itemcat"];//lay danh sach id cua category thanh 1,2,3,4,5,6
                    var listarr = listItem.Split(',');
                    foreach (var row in listarr)
                    {
                        int id = int.Parse(row);
                        Category category = categoryDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = category.Name;
                        menu.Table = category.Id;
                        menu.Link = category.Slug;
                        menu.Type = "category";//table menu
                        menu.Position = form["Position"];
                        menu.ParentID = 0;
                        menu.Orders = 0;
                        menu.CreateBy = (Session["UserID"].Equals("")) ? 1 : int.Parse(Session["UserID"].ToString());
                        menu.CreateAt = DateTime.Now;
                        menu.Status = 2;
                        menuDAO.Insert(menu);

                    }
                    TempData["message"] = new XMessage("success", "thêm menu thành công ");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "chưa chọn danh mục sản phẩm ");
                }
            }
            //topic
            if (!string.IsNullOrEmpty(form["ThemTopic"]))
            {
                if (!string.IsNullOrEmpty(form["itemTopic"]))
                {
                    var listItem = form["itemTopic"];//lay danh sach id cua category thanh 1,2,3,4,5,6
                    var listarr = listItem.Split(',');
                    foreach (var row in listarr)
                    {
                        int id = int.Parse(row);
                        Topic topic = topicDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = topic.Name;
                        menu.Table = topic.Id;
                        menu.Link = topic.Slug;
                        menu.Type = "topic";//table menu
                        menu.Position = form["Position"];
                        menu.ParentID = 0;
                        menu.Orders = 0;
                        menu.CreateBy = (Session["UserID"].Equals("")) ? 1 : int.Parse(Session["UserID"].ToString());
                        menu.CreateAt = DateTime.Now;
                        menu.Status = 2;
                        menuDAO.Insert(menu);

                    }
                    TempData["message"] = new XMessage("success", "thêm menu thành công ");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "chưa chọn danh mục sản phẩm ");
                }
            }
            //page
            if (!string.IsNullOrEmpty(form["ThemPage"]))
            {
                if (!string.IsNullOrEmpty(form["itempage"]))
                {
                    var listItem = form["itempage"];//lay danh sach id cua category thanh 1,2,3,4,5,6
                    var listarr = listItem.Split(',');
                    foreach (var row in listarr)
                    {
                        int id = int.Parse(row);
                        Post post = postDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = post.Title;
                        menu.Table = post.Id;
                        menu.Link = post.Slug;
                        menu.Type = "page";//table menu
                        menu.Position = form["Position"];
                        menu.ParentID = 0;
                        menu.Orders = 0;
                        menu.CreateBy = (Session["UserID"].Equals("")) ? 1 : int.Parse(Session["UserID"].ToString());
                        menu.CreateAt = DateTime.Now;
                        menu.Status = 2;
                        menuDAO.Insert(menu);

                    }
                    TempData["message"] = new XMessage("success", "thêm menu thành công ");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "chưa chọn danh mục sản phẩm ");
                }
            }
            //ThemCustom
            if (!string.IsNullOrEmpty(form["ThemCustom"]))
            {
                if (!string.IsNullOrEmpty(form["name"]) && !string.IsNullOrEmpty(form["link"]))
                {
                    Menu menu = new Menu();
                    menu.Name = form["name"];
                    menu.Table = 0;
                    menu.Link = form["link"];
                    menu.Type = "custom";//table menu 
                    menu.Position = form["Position"];
                    menu.ParentID = 0;
                    menu.Orders = 0;
                    menu.CreateBy = (Session["UserID"].Equals("")) ? 1 : int.Parse(Session["UserID"].ToString());
                    menu.CreateAt = DateTime.Now;
                    menu.Status = 2;
                    menuDAO.Insert(menu);


                    TempData["message"] = new XMessage("success", "thêm menu thành công ");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "chưa nhập đủ thông tin ");
                }
            }
            return RedirectToAction("Index", "Menu");
        }
        public ActionResult Edit(int? id)
        {
            ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrders = new SelectList(menuDAO.getList("Index"), "Orders", "Name");
            Menu menu = menuDAO.getRow(id);
            return View("Edit", menu);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menu)
        {

            if (ModelState.IsValid)
            {

                if (menu.ParentID == null)
                {
                    menu.ParentID = 0;
                }
                if (menu.Orders == null)
                {
                    menu.Orders = 1;
                }
                else
                {
                    menu.Orders += 1;
                }
                menu.UpdateBy = (Session["UserID"].Equals("")) ? 1 : int.Parse(Session["UserID"].ToString());
                menu.UpdateAt = DateTime.Now;
                menuDAO.Update(menu);
                TempData["message"] = new XMessage("success", "Cập Nhật Thành Công");
                return RedirectToAction("Index");
            }
            ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrders = new SelectList(menuDAO.getList("Index"), "Orders", "Name");
            return View(menu);
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index");
            }
            menu.Status = (menu.Status == 1) ? 2 : 1;
            menu.UpdateBy = (Session["UserID"].Equals("")) ? 1 : int.Parse(Session["UserID"].ToString());
            menu.UpdateAt = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Cập Nhật Thành Công");
            return RedirectToAction("Index");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index");
            }
            menu.Status = 0;//trang thai thung rac la 0
            menu.UpdateBy = (Session["UserID"].Equals("")) ? 1 : int.Parse(Session["UserID"].ToString());
            menu.UpdateAt = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Đưa Vào Thùng Rác Thành Công");
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            List<Menu> menu = menuDAO.getList("Trash");
            return View("Trash", menu);
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash");
            }
            menu.Status = 2;
            menu.UpdateBy = (Session["UserID"].Equals("")) ? 1 : int.Parse(Session["UserID"].ToString());
            menu.UpdateAt = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Cập Nhật Thành Công");
            return RedirectToAction("Trash");
        }
    }
}