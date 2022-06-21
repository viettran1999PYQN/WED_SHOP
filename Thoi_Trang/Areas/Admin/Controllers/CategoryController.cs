using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using Thoi_Trang.Library;

namespace Thoi_Trang.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        

        CategoryDAO CategoryDAO = new CategoryDAO();
        LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View("Index",CategoryDAO.getList("Index"));
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)//lỗi không lấy mã id
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = CategoryDAO.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
          ViewBag.ListCast= new SelectList(CategoryDAO.getList("Index"), "Id", "Name",0);
          ViewBag.ListOrders = new SelectList(CategoryDAO.getList("Index"), "Orders", "Name",0);
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)

        {

            if (ModelState.IsValid)
            {
                //xử lý thêm thông tin
                category.Slug = XString.str_slug(category.Name);
                if (category.ParentID == null)
                {
                    category.ParentID = 0;
                }
                if (category.Orders == null)
                {
                    category.Orders = 1;
                }
                else
                {
                    category.Orders += 1;
                }
                category.CreateBy = Convert.ToInt32(Session["UserID"].ToString());
                category.CreateAt = DateTime.Now;


                if (CategoryDAO.Insert(category) == 1)
                {
                    
                    Link link = new Link();
                    link.TableID = category.Id;
                    link.Slug = category.Slug;
                    link.TypeLink = "category";
                    link.Status = category.Status;
                    linkDAO.Insert(link);
                    
                }
                TempData["message"] = new XMessage("success", "Thêm Thành Công");
                return RedirectToAction("Index","Category");
            }
            ViewBag.ListCast = new SelectList(CategoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrders = new SelectList(CategoryDAO.getList("Index"), "Orders", "Name", 0);
            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListCast = new SelectList(CategoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrders = new SelectList(CategoryDAO.getList("Index"), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = CategoryDAO.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            
            if (ModelState.IsValid)
            {
                category.Slug = XString.str_slug(category.Name);
                if (category.ParentID == null)
                {
                    category.ParentID = 0;
                }
                if (category.Orders == null)
                {
                    category.Orders = 1;
                }
                else
                {
                    category.Orders += 1;
                }
                category.UpdateBy = Convert.ToInt32(Session["UserID"].ToString());
                category.UpdateAt = DateTime.Now;
               if(CategoryDAO.Update(category) == 1)
                {
                    Link link = linkDAO.getRow(category.Id,"category");
                    link.Slug = category.Slug;
                    linkDAO.Update(link);
                }
                TempData["message"] = new XMessage("success", "Cập Nhật Thành Công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCast = new SelectList(CategoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrders = new SelectList(CategoryDAO.getList("Index"), "Orders", "Name", 0);
            return View(category);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = CategoryDAO.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = CategoryDAO.getRow(id);
            Link link = linkDAO.getRow(category.Id, "category");
            if (CategoryDAO.Delete(category) == 1)
            {
                
                linkDAO.Delete(link);
            }
            TempData["message"] = new XMessage("success", "Xóa Mẫu Tin Thành Công");
            return RedirectToAction("Trash","Category");
        }
        public ActionResult Trash()
        {
            return View(CategoryDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] =new XMessage("danger", "Mã Loại sản phẩm không tồn tại");
                return RedirectToAction("Index","Category");
            }
            Category category = CategoryDAO.getRow(id);
            if (category == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            category.Status = (category.Status == 1) ? 2 : 1;
            category.UpdateBy = Convert.ToInt32(Session["UserID"].ToString());
            category.UpdateAt = DateTime.Now;
            CategoryDAO.Update(category);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index","Category");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            Category category = CategoryDAO.getRow(id);
            if (category == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            category.Status = 0;//trang thai xoa =0
            category.UpdateBy = Convert.ToInt32(Session["UserID"].ToString());
            category.UpdateAt = DateTime.Now;
            CategoryDAO.Update(category);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Category");
        }
        
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Category");
            }
            Category category = CategoryDAO.getRow(id);
            if (category == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Trash", "Category");
            }
            category.Status = 2;//quay về trạng thái cũ
            category.UpdateBy = Convert.ToInt32(Session["UserID"].ToString());
            category.UpdateAt = DateTime.Now;
            CategoryDAO.Update(category);
            TempData["message"] = new XMessage("success", "Khôi phục  thành công");
            return RedirectToAction("Trash", "Category");
        }



    }
}
