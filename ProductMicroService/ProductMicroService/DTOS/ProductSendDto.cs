namespace ProductMicroService.DTOS
{
    public class ProductSendDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCompany { get; set; }
        public DateTime DateOfManuFacture { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Imageurl { get; set; }
        public int Price { get; set; }
        public string CategoryName { get; set; }
    }
}
