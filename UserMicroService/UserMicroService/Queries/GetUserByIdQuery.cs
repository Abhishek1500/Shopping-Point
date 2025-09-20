using AutoMapper;
using MediatR;
using UserMicroService.Data;
using UserMicroService.DTOS;
using Week3Assignment.ExceptionHandler;

namespace UserMicroService.Queries
{
    public class GetUserByIdQuery : IRequest<UserSendDto>
    {
        public int Id;
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }


    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserSendDto>
    {
        DataContext _dbCon;
        IMapper _mapper;
        public GetUserByIdHandler(DataContext dbCon, IMapper mapper)
        {
            _dbCon = dbCon;
            _mapper = mapper;
        }
        public async Task<UserSendDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            
            var user=await _dbCon.Users.FindAsync(request.Id);
            if (user == null)
            {
                throw new CustomException(404, "User Not Found");
            }

            return _mapper.Map<UserSendDto>(user);

        }
    }

}
