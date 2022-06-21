using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
   public class OrderDAO
    {
        private Thoi_TrangDB db = new Thoi_TrangDB();
        //lay tat ca
        public List<Order> getList(string status = "ALL")
        {
            List<Order> list = null;
            switch (status)
            {
                case "Index":
                    //lay ra nhung mau tin co trang thai !=0
                    list = db.Orders.Where(m => m.Status != 0).ToList();
                    break;
                case "Trash":
                    //lay ra nhung mau tin co trang thai ==0
                    list = db.Orders.Where(m => m.Status == 0).ToList();
                    break;
                default:
                    //tra ve mat dinh cua mau tin select * from  category
                    list = db.Orders.ToList();
                    break;
            }
            return list;
        }
        //lay 1 mau tin
        public Order getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Orders.Find(id);
            }
        }
        //them mau tin
        public int Insert(Order row)
        {

            db.Orders.Add(row);
            return db.SaveChanges();
        }
        //update mau tin
        public int Update(Order row)
        {


            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xoa mau tin
        public int Delete(Order row)
        {
            db.Orders.Remove(row);
            return db.SaveChanges();
        }
    }
}
