using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyClass.Models
{
    [Table("Suppliers")]
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        public string Slug { get; set; }
        public int? Order { get; set; }
        public string Img { get; set; }
        public int? Create_By { get; set; }
        public DateTime? Create_At { get; set; }

        public int? Update_By { get; set; }
        public DateTime? Update_At { get; set; }

        public int Status { get; set; }
    }
}
