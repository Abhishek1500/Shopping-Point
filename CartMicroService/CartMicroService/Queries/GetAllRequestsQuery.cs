using CartMicroService.Data;
using CartMicroService.DTOS;
using CartMicroService.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using Week3Assignment.ExceptionHandler;

namespace CartMicroService.Queries
{
    public class GetAllRequestsQuery : IRequest<List<RequestSendDto>>
    {
        public string Token;
        public GetAllRequestsQuery(string token) { Token = token; }
    }

    public class GetAllCartRequestQueryHandler : IRequestHandler<GetAllRequestsQuery, List<RequestSendDto>>
    {

        public DataContext _dbCon;
        public GetAllCartRequestQueryHandler(DataContext dbCon)
        {
            _dbCon = dbCon;
        }

        public async Task<List<RequestSendDto>> Handle(GetAllRequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = await _dbCon.CartRequests.Where(x=>x.status=="pending").ToListAsync();
            if (requests.IsNullOrEmpty())
            {
                throw new CustomException(404, "No Request Available");
            }
            var userService = new UserService(request.Token);
            var users = await userService.loadAllUsers();
            if (users.IsNullOrEmpty())
            {
                throw new CustomException(404, "No User No Requests");
            }
            var prodServe = new ProductService(request.Token);
            var products = await prodServe.loadAllProducts();
            if (products.IsNullOrEmpty())
            {
                throw new CustomException(404, "No Products No Requests");
            }

            var cartDetails = requests.Join(users, r => r.UserId, u => u.Id, (r, u) => new
            {
                req = r,
                user = u
            }).Join(products, ru => ru.req.ProductId, p => p.Id, (ru, p) => new RequestSendDto
            {
                Id = ru.req.Id,
                UserId = ru.user.Id,
                UserEmail = ru.user.Email,
                UserName = ru.user.Name,
                ProductId = p.Id,
                ProductName = p.ProductName,
                ProductCompany = p.ProductCompany,
                Imageurl = p.Imageurl,
                Price = p.Price,
                CategoryName = p.CategoryName,
                Status=ru.req.status,
                Count=ru.req.Count,
                LastUpdated=ru.req.LastUpdate
            });

            if (cartDetails.IsNullOrEmpty())
            {
                throw new CustomException(404,"Requests Not Found");
            }

            return cartDetails.ToList();

        }
    }
}


