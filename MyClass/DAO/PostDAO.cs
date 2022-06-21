using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
   public class PostDAO
    {
        private Thoi_TrangDB db = new Thoi_TrangDB();
        //lay tat ca
        public List<Post> getListAll( string type = "post")
        {
            List<Post> list = db.Posts.Where(m => m.Status != 0 && m.PostType == type).ToList();
            return list;
        }
        public List<Post> getList(string status = "ALL",string type="post")
        {
            List<Post> list = null;
            switch (status)
            {
                case "Index":
                    //lay ra nhung mau tin co trang thai !=0
                    list = db.Posts.Where(m => m.Status != 0 &&m.PostType==type).ToList();
                    break;
                case "Trash":
                    //lay ra nhung mau tin co trang thai ==0
                    list = db.Posts.Where(m => m.Status == 0 && m.PostType == type).ToList();
                    break;
                default:
                    //tra ve mat dinh cua mau tin select * from  Supplier
                    list = db.Posts.Where(m=>m.PostType==type).ToList();
                    break;
            }
            return list;
        }
        //lay page
        public List<Post> getListPage(string status = "ALL", string type = "page")
        {
            List<Post> list = null;
            switch (status)
            {
                case "Index":
                    //lay ra nhung mau tin co trang thai !=0
                    list = db.Posts.Where(m => m.Status != 0 && m.PostType == type).ToList();
                    break;
                case "Trash":
                    //lay ra nhung mau tin co trang thai ==0
                    list = db.Posts.Where(m => m.Status == 0 && m.PostType == type).ToList();
                    break;
                default:
                    //tra ve mat dinh cua mau tin select * from  Supplier
                    list = db.Posts.Where(m => m.PostType == type).ToList();
                    break;
            }
            return list;
        }
        //lay page
        public List<Post> getList()
        {

            return db.Posts.ToList();
        }
        public Post getRow(string slug)
        {
            return db.Posts.Where(m => m.Slug == slug && m.Status == 1 && m.PostType=="post").FirstOrDefault();
        }
        public Post getRowPage(string slug)
        {
            return db.Posts.Where(m => m.Slug == slug && m.Status == 1 && m.PostType == "page").FirstOrDefault();
        }
        //lay 1 mau tin
        public Post getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Posts.Find(id);
            }
        }
        //them mau tin
        public int Insert(Post row)
        {

            db.Posts.Add(row);
            return db.SaveChanges();
        }
        //update mau tin
        public int Update(Post row)
        {


            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xoa mau tin
        public int Delete(Post row)
        {
            db.Posts.Remove(row);
            return db.SaveChanges();
        }
    }
}
