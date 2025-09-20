using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserMicroService.Data;
using UserMicroService.DTOS;
using UserMicroService.Helper;
using Week2Assignment.IDServivce;
using Week3Assignment.ExceptionHandler;

namespace UserMicroService.Queries
{
    public class UserLoginQuery :IRequest<LoginRegisterSendDto>
    {
        public UserLoginDto loginCred;
        public UserLoginQuery(UserLoginDto cred)
        {
            loginCred = cred;
        }
    }

    public class UserLoginHandler : IRequestHandler<UserLoginQuery, LoginRegisterSendDto>
    {

        DataContext _dbCon;
        ITokenService _tokenGenerator;

        public UserLoginHandler(IMapper mapper, DataContext dbCon,ITokenService token)
        {
            _dbCon = dbCon;
            _tokenGenerator = token;
        }


        public async Task<LoginRegisterSendDto> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbCon.Users.Where(x => x.Email == request.loginCred.Email.ToLower().Trim()).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new CustomException(404, "User Not Found");
            }


            if (!user.Password.isHashOf(request.loginCred.Password,user.PasswordSalt))
            {
                throw new CustomException(401, "You Are UnAuthorized");
            }

            return new LoginRegisterSendDto
            {
                Email = user.Email,
                Id = user.Id,
                IsAdmin = user.IsAdmin,
                Name = user.Name,
                ImageUrl=user.PhotoUrl,
                Token = _tokenGenerator.createToken(user)
            };
        }
    }

}
