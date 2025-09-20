using System.Diagnostics;
using System.Reflection.Metadata;

namespace UserMicroService.DTOS
{
    public class LoginRegisterSendDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; } = "";
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public Boolean IsAdmin { get; set; } 
    }
}
