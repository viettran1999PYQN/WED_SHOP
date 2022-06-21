using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyClass.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]//cho phep null
        public string phone { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public int PassWord { get; set; }
        [Required]
        public string Roles { get; set; }//phan quyen
        public int? CreateBy { get; set; }//tao boi ai 
        public DateTime? CreateAt { get; set; }//tao ngay nao
        public int? UpdateBy { get; set; }//cap nhat boi ai 
        public DateTime? UpdateAt { get; set; }//dau ?nghia la gi:nghia la co the rong khi khai bao hay dung
        public int Status { get; set; }//trang thai
    }
}
