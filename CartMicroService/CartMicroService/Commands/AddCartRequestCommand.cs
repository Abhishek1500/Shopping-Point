using CartMicroService.Data;
using CartMicroService.DTOS;
using CartMicroService.Helper;
using CartMicroService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Week3Assignment.ExceptionHandler;

namespace CartMicroService.Commands
{
    public class AddCartRequestCommand : IRequest<Unit>
    {
        public AddCartRequestDto cartAdddto;
        public int UserId;
        public string Token;
        public string Status;
        public AddCartRequestCommand(int userid ,AddCartRequestDto crDto,string token,string status)
        {
            cartAdddto = crDto;
            UserId = userid;
            Token = token;
            Status = status;
        }
    }


    public class AddCartRequestCommandHandler : MediatR.IRequestHandler<AddCartRequestCommand, Unit>
    {

        DataContext _dbCon;
        public AddCartRequestCommandHandler(DataContext dbCon)
        {
            _dbCon = dbCon;
        }

        public async Task<Unit> Handle(AddCartRequestCommand request, CancellationToken cancellationToken)
        {
            ProductService prodServe = new ProductService(request.Token);
            var product = await prodServe.loadProductById(request.cartAdddto.ProductId);
            if (product == null)
            {
                throw new CustomException(404, "Product Not Found");
            }

            UserService userServe = new UserService(request.Token);
            var user = await userServe.loadUserById(request.UserId);
            if (user == null)
            {
                throw new CustomException(404, "User Not Found");
            }



            if (request.Status == "carted" ){
                var req = await _dbCon.CartRequests.Where(x => x.ProductId == request.cartAdddto.ProductId && request.UserId == x.UserId && x.status=="carted").FirstOrDefaultAsync();
                if (req != null)
                {
                    req.Count += request.cartAdddto.Count;
                    await _dbCon.SaveChangesAsync();
                    return Unit.Value;
                }
            }

            if (request.Status == "pending")
            {
                var req=await _dbCon.CartRequests.Where(x => x.ProductId == request.cartAdddto.ProductId && request.UserId == x.UserId && x.status == "pending").FirstOrDefaultAsync();
                if (req != null)
                {
                    throw new CustomException(400, "Already request for this product is in Queue");
                }
            }
            

            await _dbCon.CartRequests.AddAsync(new CartRequest
            {
                status = request.Status,
                UserId = request.UserId,
                ProductId = request.cartAdddto.ProductId,
                Count = request.cartAdddto.Count
            });

            await _dbCon.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
