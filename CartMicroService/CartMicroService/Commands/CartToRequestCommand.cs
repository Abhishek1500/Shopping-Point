using CartMicroService.Data;
using MediatR;
using Week3Assignment.ExceptionHandler;

namespace CartMicroService.Commands
{
    public class CartToRequestCommand : IRequest<Unit>
    {
        public int UserId;
        public int RequestId;

        public CartToRequestCommand(int userId,int requestId)
        {
            UserId = userId;
            RequestId = requestId;
        }

    }

    public class CartToRequestCommandHandler: IRequestHandler<CartToRequestCommand,Unit>
    {

        private DataContext _dbCon;

        public CartToRequestCommandHandler(DataContext dbCon)
        {
            _dbCon = dbCon;
        }

        public async Task<Unit> Handle(CartToRequestCommand request, CancellationToken cancellationToken)
        {
            var cartItem = _dbCon.CartRequests.Find(request.RequestId);
            if (cartItem == null)
            {
                throw new CustomException(404, "CartItem Not Found");
            }

            if (cartItem.UserId != request.UserId)
            {
                throw new CustomException(403, "Forbidden to change this request");
            }

            if (cartItem.status != "carted")
            {
                throw new CustomException(400, "The Id doesn't belong to Cart Item");
            }

            cartItem.status = "pending";
            cartItem.LastUpdate = DateTime.Now;
            await _dbCon.SaveChangesAsync();
            return Unit.Value;

        }
    }

  


}
