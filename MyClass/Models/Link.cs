using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyClass.Models
{
    [Table("Links")]
    public class Link
    {
       [Key]
       public int Id { get; set; }
        [Required]
        public String Slug { get; set; }
        [Required]
        public String TypeLink { get; set; }
        public int TableID { get; set; }
        public int Status { get; set; }
    }
}
