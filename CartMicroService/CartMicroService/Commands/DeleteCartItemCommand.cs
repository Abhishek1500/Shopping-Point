using CartMicroService.Data;
using MediatR;
using Week3Assignment.ExceptionHandler;

namespace CartMicroService.Commands
{
    public class DeleteCartItemCommand : IRequest<Unit>
    {
        public int RequestId;
        public int UserId;

        public DeleteCartItemCommand(int requestId,int userId)
        {
            RequestId = requestId;
            UserId = userId;
        }
    }


    public class DeleteCartItemCommandhandler : IRequestHandler<DeleteCartItemCommand,Unit>
    {
        private DataContext _dbCon;

        public DeleteCartItemCommandhandler(DataContext dbCon)
        {
            _dbCon = dbCon;
        }

        public async Task<Unit> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            var cartItem = _dbCon.CartRequests.Find(request.RequestId);
            if (cartItem == null)
            {
                throw new CustomException(404, "Item Not Found");
            }

            if (cartItem.UserId != request.UserId)
            {
                throw new CustomException(403, "Forbidden to Delete this Item");
            }

            if (cartItem.status != "carted")
            {
                throw new CustomException(400,"This Item is not an Cart Item cant Delete");
            }

            _dbCon.CartRequests.Remove(cartItem);
            await _dbCon.SaveChangesAsync();

            return Unit.Value;


        }
    }

}
