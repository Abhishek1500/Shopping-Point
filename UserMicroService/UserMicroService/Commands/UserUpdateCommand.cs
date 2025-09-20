using AutoMapper;
using MediatR;
using UserMicroService.Data;
using UserMicroService.DTOS;
using Week3Assignment.ExceptionHandler;

namespace UserMicroService.Commands
{
    public class UserUpdateCommand : IRequest<UserSendDto>
    {
        public UpdateUserDto Userupdate;
        public int Id;
        public UserUpdateCommand(UpdateUserDto udto,int id) 
        {
            Userupdate = udto;
            Id = id;
        }
    }

    public class UserUpdateHandler : IRequestHandler<UserUpdateCommand, UserSendDto>
    {
        IMapper _mapper;
        DataContext _dbCon;

        public UserUpdateHandler(IMapper mapper,DataContext dbCon)
        {
            _mapper = mapper;
            _dbCon = dbCon;
        }


        public async Task<UserSendDto> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbCon.Users.FindAsync(request.Id);
            if (user == null)
            {
                throw new CustomException(404, "User Not Found");
            }

            _mapper.Map(request.Userupdate, user);
            await _dbCon.SaveChangesAsync();
            return _mapper.Map<UserSendDto>(user);

        }
    }

}
