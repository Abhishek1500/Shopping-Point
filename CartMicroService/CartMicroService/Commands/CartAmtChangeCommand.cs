using CartMicroService.Data;
using CartMicroService.Helper;
using MediatR;
using Week3Assignment.ExceptionHandler;

namespace CartMicroService.Commands
{
    public class CartAmtChangeCommand : IRequest<Unit>
    {
        public int RequestId;
        public int NewValue;
        public int Id;
        public string Token;

        public CartAmtChangeCommand(int requestId,int newValue,int id,string token)
        {
            RequestId = requestId;
            NewValue = newValue;
            Id = id;
            Token = token;

        }

    }


    public class CartAmtChangeCommandHandler : IRequestHandler<CartAmtChangeCommand, Unit>
    {
        DataContext _dbCon;

        public CartAmtChangeCommandHandler(DataContext dbCon)
        {
            _dbCon = dbCon;
        }

        public async Task<Unit> Handle(CartAmtChangeCommand request, CancellationToken cancellationToken)
        {
            var req = await _dbCon.CartRequests.FindAsync(request.RequestId);

            if (req.status != "carted")
                throw new CustomException(404, "Cart Item Not Found");

            if (req.UserId != request.Id)
                throw new CustomException(403, "Not Allowed to change value for this cart Item");

            ProductService prodServe = new ProductService(request.Token);
            var product = await prodServe.loadProductById(req.ProductId);
            
            if (product == null)
                throw new CustomException(400, "Cart Item is Deleted as Product is Delted");


            req.Count = request.NewValue;

            await _dbCon.SaveChangesAsync();


            return Unit.Value;
        }
    }
}
