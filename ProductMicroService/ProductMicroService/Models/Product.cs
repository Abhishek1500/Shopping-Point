using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace ProductMicroService.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCompany { get; set; }
        public DateTime DateOfManuFacture { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Imageurl { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
