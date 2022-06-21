using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
   public class SliderDAO
    {
        private Thoi_TrangDB db = new Thoi_TrangDB();
        //lay tat ca
        public List<Slider>  getSliderBy()
        {
            return db.Sliders.Where(m=>m.Status==1).ToList();
        }
        public List<Slider> getList(string status = "ALL")
        {
            List<Slider> list = null;
            switch (status)
            {
                case "Index":
                    //lay ra nhung mau tin co trang thai !=0
                    list = db.Sliders.Where(m => m.Status != 0).ToList();
                    break;
                case "Trash":
                    //lay ra nhung mau tin co trang thai ==0
                    list = db.Sliders.Where(m => m.Status == 0).ToList();
                    break;
                default:
                    //tra ve mat dinh cua mau tin select * from  category
                    list = db.Sliders.ToList();
                    break;
            }
            return list;
        }
        //lay 1 mau tin
        public Slider getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Sliders.Find(id);
            }
        }
        //them mau tin
        public int Insert(Slider row)
        {

            db.Sliders.Add(row);
            return db.SaveChanges();
        }
        //update mau tin
        public int Update(Slider row)
        {


            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xoa mau tin
        public int Delete(Slider row)
        {
            db.Sliders.Remove(row);
            return db.SaveChanges();
        }
    }
}
