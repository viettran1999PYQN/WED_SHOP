using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{   
    [Table("Categorys")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không thể để trống")]
        [StringLength(255)]
        [Display(Name= "Tên danh mục")]
        public string Name { get; set; }
        public string Slug { get; set; }//cho phep null
        
        public int ? ParentID { get; set; }
        public int? Orders { get; set; }
        [Required(ErrorMessage = "Từ khóa SEO không thể để trống")]
        public string MetaDesc { get; set; }
        [Required(ErrorMessage = "Mô tả SEO không thể để trống")]
        public string  MetaKey { get; set; }
        public int? CreateBy { get; set; }
        
        public DateTime? CreateAt { get; set; }//tao luc nao
        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }//dau ?nghia la gi
        public int Status { get; set; }//trang thai






    }
}
