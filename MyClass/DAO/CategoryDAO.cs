﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;


namespace MyClass.DAO
{
    
    
    public class CategoryDAO
    {
        private Thoi_TrangDB db = new Thoi_TrangDB();
        //lay tat ca
        public List<Category>  getList(string status="ALL")
        {
            List<Category> list = null;
            switch (status)
            {
                case "Index":
                    //lay ra nhung mau tin co trang thai !=0
                    list = db.Categorys.Where(m => m.Status != 0).ToList();
                    break;
                case"Trash":
                    //lay ra nhung mau tin co trang thai ==0
                    list = db.Categorys.Where(m => m.Status == 0).ToList();
                    break;
                default:
                    //tra ve mat dinh cua mau tin select * from  category
                    list = db.Categorys.ToList();
                    break;
            }
            return list;
        }
        public List<Category> getListBy(int parentID=0)
        {

            return db.Categorys.Where(m=>m.Status==1 && m.ParentID==parentID).ToList();
        }
        public List<Category> getList()
        {
          
            return db.Categorys.ToList();
        }
        //lay 1 mau tin
        public Category getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Categorys.Find(id);
            }
        }
        //them mau tin
        public int Insert(Category row)
        {
            
            db.Categorys.Add(row);
           return db.SaveChanges();
        }
        //update mau tin
        public int Update(Category row)
        {


            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xoa mau tin
        public int Delete(Category row)
        {
            db.Categorys.Remove(row);
            return db.SaveChanges();
        }
    }
}
