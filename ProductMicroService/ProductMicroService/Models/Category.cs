using System.ComponentModel.DataAnnotations;

namespace ProductMicroService.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
