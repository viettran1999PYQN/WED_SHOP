using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
     public  class UserDAO
    {
        private Thoi_TrangDB db = new Thoi_TrangDB();
        //lay tat ca
        public List<User> getList(string status = "ALL")
        {
            List<User> list = null;
            switch (status)
            {
                case "Index":
                    //lay ra nhung mau tin co trang thai !=0
                    list = db.Users.Where(m => m.Status != 0).ToList();
                    break;
                case "Trash":
                    //lay ra nhung mau tin co trang thai ==0
                    list = db.Users.Where(m => m.Status == 0).ToList();
                    break;
                default:
                    //tra ve mat dinh cua mau tin select * from  category
                    list = db.Users.ToList();
                    break;
            }
            return list;
        }
        //lay 1 mau tin
        public User getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Users.Find(id);
            }
        }
        public User getRow(string username=null)
        {
            if (username == null)
            {
                return null;
            }
            else
            {
                return db.Users.Where(m=>m.UserName==username).FirstOrDefault();
            }
        }
        //them mau tin
        public int Insert(User row)
        {

            db.Users.Add(row);
            return db.SaveChanges();
        }
        //update mau tin
        public int Update(User row)
        {


            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xoa mau tin
        public int Delete(User row)
        {
            db.Users.Remove(row);
            return db.SaveChanges();
        }
    }
}
