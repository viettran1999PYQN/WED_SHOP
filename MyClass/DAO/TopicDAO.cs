using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class TopicDAO
    {
        private Thoi_TrangDB db = new Thoi_TrangDB();
        //lay tat ca
        public List<Topic> getList(string status = "ALL")
        {
            List<Topic> list = null;
            switch (status)
            {
                case "Index":
                    //lay ra nhung mau tin co trang thai !=0
                    list = db.Topics.Where(m => m.Status != 0).ToList();
                    break;
                case "Trash":
                    //lay ra nhung mau tin co trang thai ==0
                    list = db.Topics.Where(m => m.Status == 0).ToList();
                    break;
                default:
                    //tra ve mat dinh cua mau tin select * from  category
                    list = db.Topics.ToList();
                    break;
            }
            return list;
        }
        public List<Topic> getList()
        {

            return db.Topics.ToList();
        }
        //lay 1 mau tin
        public Topic getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Topics.Find(id);
            }
        }
        //them mau tin
        public int Insert(Topic row)
        {

            db.Topics.Add(row);
            return db.SaveChanges();
        }
        //update mau tin
        public int Update(Topic row)
        {


            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xoa mau tin
        public int Delete(Topic row)
        {
            db.Topics.Remove(row);
            return db.SaveChanges();
        }
    }
}
