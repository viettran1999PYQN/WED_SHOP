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
using System.IO;

namespace Thoi_Trang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private ProductDAO productDAO = new ProductDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        public ActionResult Index()
        {
            return View(productDAO.getList("Index"));
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)//lỗi không lấy mã id
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.ListCast = new SelectList(productDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.Listcategory = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)

        {

            if (ModelState.IsValid)
            {
                //xử lý thêm thông tin có khả năng sai
                product.Slug = XString.str_slug(product.Name);      
                
                var img = Request.Files["img"];
                if (img.ContentLength != 0)
                {

                    string[] FileExtentions = new string[] { ".jpg", ".jebg", ".png", ".gif" };
                    //kiểm tra tập tin
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {

                        string slug = product.Slug;
                        //upload hinh
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        product.Img = imgName;
                         string PathDir = "~/Public/image/products/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }
                product.Create_By = Convert.ToInt32(Session["UserID"].ToString());
                product.Create_At = DateTime.Now;
                
                productDAO.Insert(product);
                TempData["message"] = new XMessage("success", "Thêm Thành Công");
                return RedirectToAction("Index", "Product");
            }
            ViewBag.ListCast = new SelectList(productDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.Listcategory = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListCast = new SelectList(productDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.Listcategory = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {

            if (ModelState.IsValid)
            {
                product.Slug = XString.str_slug(product.Name);
               
                var img = Request.Files["img"];
                if (img.ContentLength != 0)
                {

                    string[] FileExtentions = new string[] { ".jpg", ".jebg", ".png", ".gif" };
                    //kiểm tra tập tin
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string slug = product.Slug;
                        //upload hinh
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        product.Img = imgName;
                        string PathDir = "~/Public/image/products/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        if (product.Img != null)
                        {
                            string DelPath = Path.Combine(Server.MapPath(PathDir), product.Img);
                            System.IO.File.Delete(DelPath);//xoa hinh
                        }
                        img.SaveAs(PathFile);
                    }
                }
                product.Update_By = Convert.ToInt32(Session["UserID"].ToString());
                product.Update_At = DateTime.Now;
                productDAO.Update(product);
                TempData["message"] = new XMessage("success", "Cập Nhật Thành Công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCast = new SelectList(productDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.Listcategory = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productDAO.getRow(id);
            productDAO.Delete(product);
            TempData["message"] = new XMessage("success", "Xóa Mẫu Tin Thành Công");
            return RedirectToAction("Trash", "Product");
        }
        public ActionResult Trash()
        {
            return View(productDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = (product.Status == 1) ? 2 : 1;
            product.Update_By = Convert.ToInt32(Session["UserID"].ToString());
            product.Update_At = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product =productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = 0;//trang thai xoa =0
            product.Update_By = Convert.ToInt32(Session["UserID"].ToString());
            product.Update_At = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Product");
        }

        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            product.Status = 2;//quay về trạng thái cũ
            product.Update_By = Convert.ToInt32(Session["UserID"].ToString());
            product.Update_At = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Khôi phục  thành công");
            return RedirectToAction("Trash", "Product");
        }
    }
}
