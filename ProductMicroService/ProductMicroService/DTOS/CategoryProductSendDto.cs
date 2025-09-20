namespace ProductMicroService.DTOS
{
    public class CategoryProductSendDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<ProductSendDto> Products { get; set; }
    }
}
