using CartMicroService.Data;
using CartMicroService.DTOS;
using CartMicroService.MiddleLayers;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Week3Assignment.ExceptionHandler;

namespace CartMicroService.Queries
{
    public class GetUserCartQuery: IRequest<List<HistorySendDto>>
    {
        public int UserId;
        public string Token;
        public GetUserCartQuery(int userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }

    public class GetUserCartQueryHandler : IRequestHandler<GetUserCartQuery, List<HistorySendDto>>
    {

        public DataContext _dbCon;
        public GetUserCartQueryHandler(DataContext dbCon)
        {
            _dbCon = dbCon;
        }

        public async Task<List<HistorySendDto>> Handle(GetUserCartQuery request, CancellationToken cancellationToken)
        {
            var cart = (await userHistoryMiddleLayer.UserWholeRquestLayer(_dbCon, request.UserId, request.Token))
                .Where(x=>x.status=="carted");
            if (cart.IsNullOrEmpty())
            {
                throw new CustomException(404, "No Element is present in Cart");
            }
            return cart.ToList();
        }
    }
}
