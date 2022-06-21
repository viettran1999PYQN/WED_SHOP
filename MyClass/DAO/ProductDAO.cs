using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public  class ProductDAO
    {
        private Thoi_TrangDB db = new Thoi_TrangDB();
        //lay tat ca
        public List<Product> getList(string status = "ALL")
        {
            List<Product> list = null;
            switch (status)
            {
                case "Index":
                    //lay ra nhung mau tin co trang thai !=0
                    list = db.Products.Where(m => m.Status != 0).ToList();
                    break;
                case "Trash":
                    //lay ra nhung mau tin co trang thai ==0
                    list = db.Products.Where(m => m.Status == 0).ToList();
                    break;
                default:
                    //tra ve mat dinh cua mau tin select * from  category
                    list = db.Products.ToList();
                    break;
            }
            return list;
        }
        public List<Product> getListBy(int take)
        {
            List<Product> list = db.Products.
                Where(m => m.Status == 1)
                .Take(take)
                .ToList();
            return list;
        }
        public List<Product> getList()
        {

            return db.Products.ToList();
        }
        //lay 1 mau tin
        public Product getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Products.Find(id);
            }
        }
        public Product getRow(string slug)
        {
            return db.Products.Where(m => m.Slug == slug && m.Status==1).FirstOrDefault();
        }
        //them mau tin
        public int Insert(Product row)
        {

            db.Products.Add(row);
            return db.SaveChanges();
        }
        //update mau tin
        public int Update(Product row)
        {

            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xoa mau tin
        public int Delete(Product row)
        {
            db.Products.Remove(row);
            return db.SaveChanges();
        }
    }
}
