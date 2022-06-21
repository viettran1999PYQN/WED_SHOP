using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyClass.Models
{
    [Table("Menus")]
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        [Required]
        
        public string Name { get; set; }
        [Required]
        public string Link { get; set; }
        
        public string Type { get; set; }//KIEU MENU 

        public int Table { get; set; }//cho phep null TableID
        public int? ParentID { get; set; }
        public int Orders { get; set; }
        public string Position { get; set; }//moi them
        public int? CreateBy { get; set; }//moi them

        public DateTime? CreateAt { get; set; }//moi them
        public int? UpdateBy { get; set; }//moi them
        public DateTime? UpdateAt { get; set; }//moi them

        public int Status { get; set; }
    }
}
