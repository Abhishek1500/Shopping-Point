using CartMicroService.Data;
using CartMicroService.DTOS;
using CartMicroService.Helper;
using CartMicroService.MiddleLayers;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Week3Assignment.ExceptionHandler;

namespace CartMicroService.Queries
{

    public class GetUserRequestHistoryQuery : IRequest<List<HistorySendDto>>
    {

        public int UserId;
        public string Token;
        public GetUserRequestHistoryQuery(int userId, string token)
        {
            UserId = userId;
            Token = token;
        }

    }


    public class GetUserRequestHistoryQueryHandler : IRequestHandler<GetUserRequestHistoryQuery, List<HistorySendDto>>
    {
        private DataContext _dbCon;
        public GetUserRequestHistoryQueryHandler(DataContext dbCon)
        {
            _dbCon = dbCon;
        }
        public async Task<List<HistorySendDto>> Handle(GetUserRequestHistoryQuery request, CancellationToken cancellationToken)
        {
            var history = (await userHistoryMiddleLayer.UserWholeRquestLayer(_dbCon, request.UserId, request.Token)).Where(x=>x.status!="carted");
            if (history.IsNullOrEmpty())
            {
                throw new CustomException(404, "No Request Found");
            }
            return history.ToList();
        }
    }

}