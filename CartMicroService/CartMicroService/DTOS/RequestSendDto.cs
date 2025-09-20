namespace CartMicroService.DTOS
{
    public class RequestSendDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCompany { get; set; }
        public string Imageurl { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public string CategoryName { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
