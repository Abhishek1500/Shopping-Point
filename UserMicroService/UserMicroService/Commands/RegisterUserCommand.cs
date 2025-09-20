using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using UserMicroService.Data;
using UserMicroService.DTOS;
using UserMicroService.Helper;
using UserMicroService.Models;
using Week2Assignment.IDServivce;
using Week3Assignment.ExceptionHandler;

namespace UserMicroService.Commands
{
    public class RegisterUserCommand : IRequest<LoginRegisterSendDto>
    {
        public RegisterUserDto Rdto;
        public RegisterUserCommand(RegisterUserDto rDto)
        {
            Rdto = rDto;
        }
    }

    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, LoginRegisterSendDto>
    {
        private DataContext _dbCon;
        private IMapper _mapper;
        private ITokenService _tokenGenerator;
        public RegisterUserHandler(DataContext dbCon, IMapper mapper, ITokenService token)
        {
            _mapper = mapper;
            _dbCon = dbCon;
            _tokenGenerator = token;
        }

        public async Task<LoginRegisterSendDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            var look = await _dbCon.Users.Where(x => x.Email == request.Rdto.Email.Trim().ToLower()).FirstOrDefaultAsync();
            if (look != null)
            {
                throw new CustomException(400, "This Email already linked to account");
            }

            var user =_mapper.Map<User>(request.Rdto);
            user.PhotoUrl = "data:image/png;base64,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";

            //128 bit salt and telling the bytes
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            user.PasswordSalt = salt;
            user.Password = user.Password.hashPassword(user.PasswordSalt);

            await _dbCon.Users.AddAsync(user);
            await _dbCon.SaveChangesAsync();

            return new LoginRegisterSendDto {
                Email = user.Email,
                Id = user.Id,
                IsAdmin = user.IsAdmin,
                Name = user.Name,
                ImageUrl=user.PhotoUrl,
                Token = _tokenGenerator.createToken(user)
            } ;
            
        }
    }


}
