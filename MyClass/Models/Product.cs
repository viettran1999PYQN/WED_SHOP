using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        public int CatId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        
        public string Detail { get; set; }//thông tin chi tiết
        
        public string MetaKey { get; set; }
        
        public string Metadesc{ get; set; }
        public string Img{ get; set; }
        public int Number { get; set; }
        public double Price { get; set; }//giá nhập
        public double Pricesale { get; set; }//gia bán
        public int? Create_By { get; set; }
        public DateTime? Create_At { get; set;}

        public int? Update_By { get; set; }
        public DateTime? Update_At { get; set; }

        public int Status { get; set; }

    }
}
