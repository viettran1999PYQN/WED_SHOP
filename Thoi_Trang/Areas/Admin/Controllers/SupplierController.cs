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
    public class SupplierController : Controller
    {
        SupplierDAO supplierDAO = new SupplierDAO();
        LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/Supplier
        public ActionResult Index()
        {
            return View(supplierDAO.getList("Index"));
        }

        // GET: Admin/Supplier/Details/5
        public ActionResult Details(int? id)//lỗi không lấy mã id
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Admin/Supplier/Create
        public ActionResult Create()
        {
            ViewBag.ListOrders = new SelectList(supplierDAO.getList("Index"), "Order", "Name", 0);
            return View();
        }

        // POST: Admin/Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)

        {
            //Chưa xử lý adress ,phone ,email 

            if (ModelState.IsValid)
            {
                //xử lý thêm thông tin
                supplier.Slug = XString.str_slug(supplier.Name);

                if (supplier.Order == null)
                {
                    supplier.Order = 1;
                }
                else
                {
                    supplier.Order += 1;
                }
                //up load file
                var img = Request.Files["img"];
                if (img.ContentLength != 0)
                {
                  
                    string[] FileExtentions = new string[] { ".jpg", ".jebg", ".png", ".gif" };
                    //kiểm tra tập tin
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                      
                        string slug = supplier.Slug;
                        //upload hinh
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        supplier.Img = imgName;
                        string PathDir = "~/Public/image/suppliers/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }

                //up load file
                supplier.Create_By = Convert.ToInt32(Session["UserID"].ToString());
                supplier.Create_At = DateTime.Now;
                if (supplierDAO.Insert(supplier) == 1)
                {

                    Link link = new Link();
                    link.TableID = supplier.Id;
                    link.Slug = supplier.Slug;
                    link.TypeLink = "supplier";//nhà cung cấp
                    link.Status = supplier.Status;
                    linkDAO.Insert(link);

                }

                TempData["message"] = new XMessage("success", "Thêm Thành Công");
                return RedirectToAction("Index", "Supplier");
            }
            ViewBag.ListOrders = new SelectList(supplierDAO.getList("Index"), "Order", "Name", 0);
            return View(supplier);
        }

        // GET: Admin/Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListOrders = new SelectList(supplierDAO.getList("Index"), "Order", "Name", 0);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {

            if (ModelState.IsValid)
            {
                supplier.Slug = XString.str_slug(supplier.Name);

                if (supplier.Order == null)
                {
                    supplier.Order = 1;
                }
                else
                {
                    supplier.Order += 1;
                }
                //up load file
                var img = Request.Files["img"];
                if (img.ContentLength != 0)
                {

                    string[] FileExtentions = new string[] { ".jpg", ".jebg", ".png", ".gif" };
                    //kiểm tra tập tin
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {

                        string slug = supplier.Slug;
                        //upload hinh
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        supplier.Img = imgName;
                        string PathDir = "~/Public/image/suppliers/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        //xoa file hinh anh truoc
                        if (supplier.Img!=null){
                            string DelPath = Path.Combine(Server.MapPath(PathDir), supplier.Img);
                            System.IO.File.Delete(DelPath);//xoa hinh
                        }
                        img.SaveAs(PathFile);
                    }
                }

                //up load file
                supplier.Update_By = Convert.ToInt32(Session["UserID"].ToString());
                supplier.Update_At = DateTime.Now;
                if (supplierDAO.Update(supplier) == 1)
                {
                    Link link = linkDAO.getRow(supplier.Id, "supplier");
                    link.Slug = supplier.Slug;
                    linkDAO.Update(link);
                }
                TempData["message"] = new XMessage("success", "Cập Nhật Thành Công");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrders = new SelectList(supplierDAO.getList("Index"), "Order", "Name", 0);
            return View(supplier);
        }

        // GET: Admin/Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = supplierDAO.getRow(id);
            Link link = linkDAO.getRow(supplier.Id, "supplier");
            string PathDir = "~/Public/image/suppliers/";
            if (supplier.Img != null)
            {
                string DelPath = Path.Combine(Server.MapPath(PathDir), supplier.Img);
                System.IO.File.Delete(DelPath);//xoa hinh
            }
            if (supplierDAO.Delete(supplier) == 1)
            {

                linkDAO.Delete(link);
            }
            TempData["message"] = new XMessage("success", "Xóa Mẫu Tin Thành Công");
            return RedirectToAction("Trash", "Supplier");
        }
        public ActionResult Trash()
        {
            return View(supplierDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            supplier.Status = (supplier.Status == 1) ? 2 : 1;
            supplier.Update_By = Convert.ToInt32(Session["UserID"].ToString());
            supplier.Update_At = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Supplier");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            supplier.Status = 0;//trang thai xoa =0
            supplier.Update_By = Convert.ToInt32(Session["UserID"].ToString());
            supplier.Update_At = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Supplier");
        }

        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Trash", "Supplier");
            }
            supplier.Status = 2;//quay về trạng thái cũ
            supplier.Update_By = Convert.ToInt32(Session["UserID"].ToString());
            supplier.Update_At = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Khôi phục  thành công");
            return RedirectToAction("Trash", "Supplier");
        }
    }
}
