using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class MenuDAO
    {
        private Thoi_TrangDB db = new Thoi_TrangDB();
        //lay tat ca
        public List<Menu> getList()
        {
            return db.Menus.ToList();
        }
        public List<Menu> getListParentID(string position, int parentid = 0)
        {
            return db.Menus.Where(m=>m.ParentID==parentid && m.Status==1 && m.Position==position).OrderBy(m=>m.Orders)
                .ToList();
        }
        public List<Menu> getList(string status = "ALL")
        {
            List<Menu> list = null;
            switch (status)
            {
                case "Index":
                    //lay ra nhung mau tin co trang thai !=0
                    list = db.Menus.Where(m => m.Status != 0).ToList();
                    break;
                case "Trash":
                    //lay ra nhung mau tin co trang thai ==0
                    list = db.Menus.Where(m => m.Status == 0).ToList();
                    break;
                default:
                    //tra ve mat dinh cua mau tin select * from  category
                    list = db.Menus.ToList();
                    break;
            }
            return list;
        }
        //lay 1 mau tin
        public Menu getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Menus.Find(id);
            }
        }
        //them mau tin
        public int Insert(Menu row)
        {

            db.Menus.Add(row);
            return db.SaveChanges();
        }
        //update mau tin
        public int Update(Menu row)
        {


            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xoa mau tin
        public int Delete(Menu row)
        {
            db.Menus.Remove(row);
            return db.SaveChanges();
        }
    }
}
