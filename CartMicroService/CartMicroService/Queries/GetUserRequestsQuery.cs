using CartMicroService.Data;
using CartMicroService.DTOS;
using CartMicroService.MiddleLayers;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Week3Assignment.ExceptionHandler;

namespace CartMicroService.Queries
{
    public class GetUserRequestsQuery: IRequest<List<HistorySendDto>>
    {
        public int UserId;
        public string Token;
        public GetUserRequestsQuery(int userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }

    public class GetUserRequestQueryHandler : IRequestHandler<GetUserRequestsQuery, List<HistorySendDto>>
    {
        DataContext dbCon;
        public GetUserRequestQueryHandler(DataContext db)
        {
            dbCon = db;
        }

        public async Task<List<HistorySendDto>> Handle(GetUserRequestsQuery request, CancellationToken cancellationToken)
        {
            var history = (await userHistoryMiddleLayer.UserWholeRquestLayer(dbCon, request.UserId, request.Token)).Where(x=>x.status=="pending");

            if (history.IsNullOrEmpty())
            {
                throw new CustomException(404, "No Request Found");
            }

            return history.ToList();


        }
    }

}
