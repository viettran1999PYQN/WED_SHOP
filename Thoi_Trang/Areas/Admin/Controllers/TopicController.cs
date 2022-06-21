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
    public class TopicController : Controller
    {
        TopicDAO TopicDAO = new TopicDAO();
         LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/topic
        public ActionResult Index()
        {
            return View(TopicDAO.getList("Index"));
        }

        // GET: Admin/topic/Details/5
        public ActionResult Details(int? id)//lỗi không lấy mã id
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = TopicDAO.getRow(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Admin/topic/Create
        public ActionResult Create()
        {
            ViewBag.ListCast = new SelectList(TopicDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrders = new SelectList(TopicDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/topic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Topic Topic)

        {

            if (ModelState.IsValid)
            {
                //xử lý thêm thông tin
                Topic.Slug = XString.str_slug(Topic.Name);
                if (Topic.ParentID == null)
                {
                    Topic.ParentID = 0;
                }
                if (Topic.Orders == null)
                {
                    Topic.Orders = 1;
                }
                else
                {
                    Topic.Orders += 1;
                }
                Topic.CreateBy = Convert.ToInt32(Session["UserID"].ToString());
                Topic.CreateAt = DateTime.Now;
                if (TopicDAO.Insert(Topic) == 1)
                {

                    Link link = new Link();
                    link.TableID = Topic.Id;
                    link.Slug = Topic.Slug;
                    link.TypeLink = "topic";
                    link.Status = Topic.Status;
                    linkDAO.Insert(link);

                }
                TempData["message"] = new XMessage("success", "Thêm Thành Công");
                return RedirectToAction("Index", "Topic");
            }
            ViewBag.ListCast = new SelectList(TopicDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrders = new SelectList(TopicDAO.getList("Index"), "Orders", "Name", 0);
            return View(Topic);
        }

        // GET: Admin/topic/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListCast = new SelectList(TopicDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrders = new SelectList(TopicDAO.getList("Index"), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = TopicDAO.getRow(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Admin/topic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Topic topic)
        {

            if (ModelState.IsValid)
            {
                topic.Slug = XString.str_slug(topic.Name);
                if (topic.ParentID == null)
                {
                    topic.ParentID = 0;
                }
                if (topic.Orders == null)
                {
                    topic.Orders = 1;
                }
                else
                {
                    topic.Orders += 1;
                }
                topic.UpdateBy = Convert.ToInt32(Session["UserID"].ToString());
                topic.UpdateAt = DateTime.Now;
                if (TopicDAO.Update(topic) == 1)
                {
                    Link link = linkDAO.getRow(topic.Id, "topic");
                    link.Slug = topic.Slug;
                    linkDAO.Update(link);
                }
                TempData["message"] = new XMessage("success", "Cập Nhật Thành Công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCast = new SelectList(TopicDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrders = new SelectList(TopicDAO.getList("Index"), "Orders", "Name", 0);
            return View(topic);
        }

        // GET: Admin/topic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = TopicDAO.getRow(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Admin/topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topic topic = TopicDAO.getRow(id);
            Link link = linkDAO.getRow(topic.Id, "topic");
            if (TopicDAO.Delete(topic) == 1)
            {

                linkDAO.Delete(link);
            }
            TempData["message"] = new XMessage("success", "Xóa Mẫu Tin Thành Công");
            return RedirectToAction("Trash", "Topic");
        }
        public ActionResult Trash()
        {
            return View(TopicDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Topic");
            }
            Topic topic = TopicDAO.getRow(id);
            if (topic == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Index", "Topic");
            }
            topic.Status = (topic.Status == 1) ? 2 : 1;
            topic.UpdateBy = Convert.ToInt32(Session["UserID"].ToString());
            topic.UpdateAt = DateTime.Now;
            TopicDAO.Update(topic);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Topic");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Topic");
            }
            Topic topic = TopicDAO.getRow(id);
            if (topic == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Index", "Topic");
            }
            topic.Status = 0;//trang thai xoa =0
            topic.UpdateBy = Convert.ToInt32(Session["UserID"].ToString());
            topic.UpdateAt = DateTime.Now;
            TopicDAO.Update(topic);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Topic");
        }

        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Topic");
            }
            Topic topic = TopicDAO.getRow(id);
            if (topic == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Trash", "Topic");
            }
            topic.Status = 2;//quay về trạng thái cũ
            topic.UpdateBy = Convert.ToInt32(Session["UserID"].ToString());
            topic.UpdateAt = DateTime.Now;
            TopicDAO.Update(topic);
            TempData["message"] = new XMessage("success", "Khôi phục  thành công");
            return RedirectToAction("Trash", "Topic");
        }
    }
}
