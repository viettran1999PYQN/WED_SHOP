using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;



namespace Thoi_Trang.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        LinkDAO linkDAO = new LinkDAO();
        ProductDAO productDAO = new ProductDAO();
        PostDAO postDAO = new PostDAO();
        public ActionResult Index(string slug = null)
        {
            //url mat dinh hoac bat ky
            //Thoi_TrangDB db = new Thoi_TrangDB();
            if (slug == null)
            {
                return this.Home();
            }
            else
            {
                Link link = linkDAO.getRow(slug);
                if (link != null)
                {
                    string typeLink = link.TypeLink;
                    switch (typeLink)
                    {
                        case "category":
                            {
                                return this.ProductCategory(slug);
                            }
                        case "page":
                            {
                                return this.PostPage(slug);
                            }
                        case "topic":
                            {
                                return this.PostTopic(slug);
                            }
                        // case "supplier":
                        default:
                            {
                                return this.Error404(slug);
                            }


                    }
                }
                else
                {
                    Product product = productDAO.getRow(slug);
                    if (product != null)
                    {
                        return this.ProductDetail(product);
                    }
                    else
                    {
                        Post post = postDAO.getRow(slug);
                        if (post != null)
                        {
                            return this.PortDetail(post);
                        }
                        else
                        {
                            return this.Error404(slug);
                        }
                    }
                    //slug trong ban product khong 
                    //slug co trong post
                    //slug khong co tron gbang link
                }
            }
           
        }
        //trang chu
        Thoi_TrangDB db = new Thoi_TrangDB();
        public ActionResult Home()
        {
            ViewBag.Somau = db.Products.Count();
            return View("Home");//thay the "Home" bang "Index"
        }
        //category
        public ActionResult Product()
        {
            return View("Product");
        }
        //chia loại thời trang
        public ActionResult ProductMan()
        {
            return View("ProductMan");
        }
        public ActionResult ProductWomen()
        {
            return View("ProductWomen");
        }
        public ActionResult ProductAccessory()
        {
            return View("ProductAccessory");
        }
        //
        public ActionResult ProductCategory(string slug)
        {
            return View("ProductCategory");
        }
        public ActionResult ProductDetail(Product product)
        {
            return View("ProductDetail");
        }

        //Post
        public ActionResult Post()
        {
            List<Post> list = postDAO.getListAll();
            return View("Post",list);
        }
        public ActionResult PostTopic(string slug)
        {
            return View("PostTopic");
        }
        public ActionResult PostPage(string slug)
        {
            Post page = postDAO.getRowPage(slug);

            return View("PostPage",page);
        }
        public ActionResult PortDetail(Post post)
        {
            return View("PortDetail");
        }

        //nhom ham Lỗi
        public ActionResult Error404(string slug)
        {
            return View("Error404");
        }


    }
}