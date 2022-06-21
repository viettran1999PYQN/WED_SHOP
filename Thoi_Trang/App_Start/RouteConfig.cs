using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Thoi_Trang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "TatCaSanPham",
               url: "tat-ca-san-pham",
               defaults: new { controller = "Home", action = "Product", id = UrlParameter.Optional }
           );
            //thoi trang nam
            routes.MapRoute(
              name: "ThoiTrangNam",
              url: "thoi-trang-nam",
              defaults: new { controller = "Home", action = "ProductMan", id = UrlParameter.Optional }
          );
            //thoi trang nu
            routes.MapRoute(
              name: "ThoiTrangNu",
              url: "thoi-trang-nu",
              defaults: new { controller = "Home", action = "ProductWomen", id = UrlParameter.Optional }
          ); 
            //phu kien
            routes.MapRoute(
               name: "PhuKien",
               url: "phu-kien",
               defaults: new { controller = "Home", action = "ProductAccessory", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "TatCaBaiViet",
               url: "tat-ca-bai-viet",
               defaults: new { controller = "Home", action = "Post", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "LienHe",
              url: "lien-he",
              defaults: new { controller = "Lienhe", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
             name: "GioHang",
             url: "gio-hang",
             defaults: new { controller = "Giohang", action = "Index", id = UrlParameter.Optional }
         );
            routes.MapRoute(
            name: "TimKiem",
            url: "tim-kiem",
            defaults: new { controller = "Timkiem", action = "Index", id = UrlParameter.Optional }
        );
            //khai báo url động-nằm kế trên default
            routes.MapRoute(
            name: "SiteSlug",
            url: "{Slug}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
