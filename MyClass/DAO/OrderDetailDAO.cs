using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class OrderDetailDAO
    {
        private Thoi_TrangDB db = new Thoi_TrangDB();
        //lay tat ca
        public List<OrderDetail> getList(string status = "ALL")
        {
            List<OrderDetail> list = null;
            switch (status)
            {
                case "Index":
                    //lay ra nhung mau tin co trang thai !=0
                    list = db.OrderDetails.Where(m => m.Status != 0).ToList();
                    break;
                case "Trash":
                    //lay ra nhung mau tin co trang thai ==0
                    list = db.OrderDetails.Where(m => m.Status == 0).ToList();
                    break;
                default:
                    //tra ve mat dinh cua mau tin select * from  category
                    list = db.OrderDetails.ToList();
                    break;
            }
            return list;
        }
        //lay 1 mau tin
        public OrderDetail getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.OrderDetails.Find(id);
            }
        }
        //them mau tin
        public int Insert(OrderDetail row)
        {

            db.OrderDetails.Add(row);
            return db.SaveChanges();
        }
        //update mau tin
        public int Update(Config row)
        {


            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xoa mau tin
        public int Delete(OrderDetail row)
        {
            db.OrderDetails.Remove(row);
            return db.SaveChanges();
        }
    }
}
