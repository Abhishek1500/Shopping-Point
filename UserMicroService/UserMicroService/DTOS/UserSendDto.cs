namespace UserMicroService.DTOS
{
    public class UserSendDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string PhotoUrl { get; set; }
        public Boolean IsAdmin { get; set; }
    }
}

