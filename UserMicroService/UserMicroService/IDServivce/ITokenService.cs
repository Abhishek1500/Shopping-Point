using UserMicroService.Models;

namespace Week2Assignment.IDServivce
{
    public interface ITokenService
    {
        string createToken(User user);
    }
}
