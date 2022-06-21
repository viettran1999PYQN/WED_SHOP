using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;
namespace Thoi_Trang.Controllers
{
    public class ModuleController : Controller
    {
        // GET: Module
        private MenuDAO menuDAO = new MenuDAO();
        private SliderDAO sliderDAO = new SliderDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        ProductDAO productDAO = new ProductDAO();
        public ActionResult MainMenu()
        {
            List<Menu> list = menuDAO.getListParentID("mainmenu", 0);
            return View("MainMenu", list);
        }

        public ActionResult MainMenuSup(int id)
        {
            Menu menu = menuDAO.getRow(id);
            List<Menu> list = menuDAO.getListParentID("mainmenu", id);

            if (list.Count == 0)
            {
                return View("MainMenuSup1", menu);//menu hông có cấp con
            }
            else
            {
                ViewBag.Menu = menu;
                return View("MainMenuSup2", list);
                //menu có cấp  con
            }

        }
        //slide show
        public ActionResult Slideshow()
        {
            List<Slider> list = sliderDAO.getSliderBy();
            return View("Slideshow", list);//dùng không được vì đang dùng view trên mạng nên rất khó để thiết kế lại
        }
        //menushow
        public ActionResult MenuShow()
        {
            List<Category> category = categoryDAO.getListBy();
            
            return View("MenuShow",category);
        }
        //product menushow

        public ActionResult ProductMenuShow()
        {
            List<Product> products = productDAO.getListBy(18);
            return View("ProductMenuShow",products);
        }
        //footer post
        public ActionResult Menufooter()
        {
            List<Menu> list = menuDAO.getListParentID("footermenu", 0);
            return View("Menufooter",list);
        }

    }
}