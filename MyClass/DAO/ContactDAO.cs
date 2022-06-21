using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
   public class ContactDAO
    {
        private Thoi_TrangDB db = new Thoi_TrangDB();
        //lay tat ca
        public List<Contact> getList(string status = "ALL")
        {
            List<Contact> list = null;
            switch (status)
            {
                case "Index":
                    //lay ra nhung mau tin co trang thai !=0
                    list = db.Contacts.Where(m => m.Status != 0).ToList();
                    break;
                case "Trash":
                    //lay ra nhung mau tin co trang thai ==0
                    list = db.Contacts.Where(m => m.Status == 0).ToList();
                    break;
                default:
                    //tra ve mat dinh cua mau tin select * from  category
                    list = db.Contacts.ToList();
                    break;
            }
            return list;
        }
        //lay 1 mau tin
        public Contact getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Contacts.Find(id);
            }
        }
        //them mau tin
        public int Insert(Contact row)
        {

            db.Contacts.Add(row);
            return db.SaveChanges();
        }
        //update mau tin
        public int Update(Contact row)
        {


            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xoa mau tin
        public int Delete(Contact row)
        {
            db.Contacts.Remove(row);
            return db.SaveChanges();
        }
    }
}
