using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyClass.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public int TopicId { get; set; }

        [Required]
        
        public string Title { get; set; }
        public string Slug { get; set; }//cho phep null
        public string Detail { get; set; }
       [Required]
        public string MetaDesc { get; set; }
        [Required]
        public string MetaKey { get; set; }
        public string PostType { get; set; }
        public string Img { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }//dau ?nghia la gi
        public int Status { get; set; }
    }
}
