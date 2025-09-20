using System.ComponentModel.DataAnnotations;

namespace CartMicroService.Models
{
    public class CartRequest
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string status { get; set; }
        public int Count { get; set; }
        public string Remark { get; set; } = "";
        public DateTime LastUpdate { get; set; } = DateTime.Now;
    }
}
