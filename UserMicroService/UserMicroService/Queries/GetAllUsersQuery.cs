using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserMicroService.Data;
using UserMicroService.DTOS;
using Week3Assignment.ExceptionHandler;

namespace UserMicroService.Queries
{
    public class GetAllUsersQuery: IRequest<List<UserSendDto>>{}

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserSendDto>>
    {
        DataContext _dbCon;
        IMapper _mapper;
        public GetAllUsersQueryHandler(DataContext dbCon,IMapper mapper)
        {
            _dbCon = dbCon;
            _mapper = mapper;
        }

        public async Task<List<UserSendDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _dbCon.Users.ToListAsync();
            if (users.IsNullOrEmpty())
            {
                throw new CustomException(404, "No Users Found");
            }
            return _mapper.Map<List<UserSendDto>>(users);
        }
    }


}
