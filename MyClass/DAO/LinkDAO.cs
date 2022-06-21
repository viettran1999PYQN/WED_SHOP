using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
   public class LinkDAO
    {
        private Thoi_TrangDB db = new Thoi_TrangDB();
        //lay tat ca
        public Link getRow(int tableid,string typelink)
        {
            return db.Links.Where(m => m.TableID == tableid && m.TypeLink == typelink).FirstOrDefault();
        }
        //lay 1 mau tin
        public Link getRow(string slug)
        {
            return db.Links.Where(m =>m.Slug == slug).FirstOrDefault();
        }
        public Link getRow(int? id)
        {
            return db.Links.Find(id);
        }
        public int Insert(Link row)
        {

            db.Links.Add(row);
            return db.SaveChanges();
        }
        //update mau tin
        public int Update(Link row)
        {


            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xoa mau tin
        public int Delete(Link row)
        {
            db.Links.Remove(row);
            return db.SaveChanges();
        }
    }
}
