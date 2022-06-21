using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyClass.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        //cho phep null
        public string Node { get; set; }
        public int? ParentID { get; set; }
        public int? Orders { get; set; }
        [Required]
       
        
        public DateTime? CreateAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }//dau ?nghia la gi
        public int Status { get; set; }
    }
}
