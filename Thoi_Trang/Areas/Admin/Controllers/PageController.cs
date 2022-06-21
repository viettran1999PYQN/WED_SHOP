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
    public class PageController : Controller
    {
        private PostDAO postDAO = new PostDAO();
        private LinkDAO linkDAO = new LinkDAO();


        // GET: Admin/Post
        public ActionResult Index()
        {
            return View(postDAO.getListPage("Index", "page"));
        }

        // GET: Admin/Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Admin/Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostType = "page";
                post.Slug = XString.str_slug(post.Title);
                post.CreateBy = Convert.ToInt32(Session["UserID"].ToString());
                post.CreateAt = DateTime.Now;

                var img = Request.Files["img"];
                if (img.ContentLength != 0)
                {

                    string[] FileExtentions = new string[] { ".jpg", ".jebg", ".png", ".gif" };
                    //kiểm tra tập tin
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {

                        string slug = post.Slug;
                        //upload hinh
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        post.Img = imgName;
                        string PathDir = "~/Public/image/pages/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }
               
                TempData["message"] = new XMessage("success", "Thêm Thành Công");
                if (postDAO.Insert(post) == 1)
                {
                    Link link = new Link();
                    link.TableID = post.Id;
                    link.Slug = post.Slug;
                    link.TypeLink = "page";
                    linkDAO.Insert(link);
                    TempData["message"] = new XMessage("success", "Thêm Thành Công");
                }
                return RedirectToAction("Index");
            }
            
            return View(post);
        }

        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            
            return View(post);
        }

        // POST: Admin/Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostType = "page";
                post.Slug = XString.str_slug(post.Title);
                post.UpdateBy = Convert.ToInt32(Session["UserID"].ToString());
                post.UpdateAt = DateTime.Now;
                var img = Request.Files["img"];
                if (img.ContentLength != 0)
                {

                    string[] FileExtentions = new string[] { ".jpg", ".jebg", ".png", ".gif" };
                    //kiểm tra tập tin
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {

                        string slug = post.Slug;
                        //upload hinh
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        post.Img = imgName;
                        string PathDir = "~/Public/image/pages/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        if (post.Img == null)
                        {
                            string DelPath = Path.Combine(Server.MapPath(PathDir), post.Img);
                            System.IO.File.Delete(DelPath);//xoa hinh
                        }
                        img.SaveAs(PathFile);
                    }
                }
               
                TempData["message"] = new XMessage("success", "Cập Nhật Thành Công");
                postDAO.Update(post);
                return RedirectToAction("Index");
            }
            
            return View(post);
        }

        // GET: Admin/Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = postDAO.getRow(id);
            postDAO.Delete(post);
            TempData["message"] = new XMessage("success", "Xóa Mẫu Tin Thành Công");
            return RedirectToAction("Trash", "Page");
        }
        public ActionResult Trash()
        {
            return View(postDAO.getListPage("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            post.Status = (post.Status == 1) ? 2 : 1;
            post.UpdateBy = Convert.ToInt32(Session["UserID"].ToString());
            post.UpdateAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Page");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            post.Status = 0;//trang thai xoa =0
            post.UpdateBy = Convert.ToInt32(Session["UserID"].ToString());
            post.UpdateAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Page");
        }

        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Page");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Trash", "Page");
            }
            post.Status = 2;//quay về trạng thái cũ
            post.UpdateBy = Convert.ToInt32(Session["UserID"].ToString());
            post.UpdateAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Khôi phục  thành công");
            return RedirectToAction("Trash", "Page");
        }
    }
}
