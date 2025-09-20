namespace CartMicroService.DTOS
{
    public class HistorySendDto
    {
        public int RequestId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCompany { get; set; }
        public string Imageurl { get; set; }
        public int Price { get; set; }
        public string CategoryName { get; set; }
        public string status { get; set; }
        public int Count { get; set; }
        public string Remark { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
