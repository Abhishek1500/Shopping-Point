using MediatR;
using ProductMicroService.Data;
using Week3Assignment.ExceptionHandler;

namespace ProductMicroService.Commands
{
    public class ChangeQuantityByValueCommand : IRequest<Unit>
    {
        public int Id;
        public int Value;
        public ChangeQuantityByValueCommand(int id, int value)
        {
            Id = id;
            Value = value;
        }
    }



    public class ChangeQuantityByValueCommandHandler : IRequestHandler<ChangeQuantityByValueCommand, Unit>
    {
        private DataContext _dbCon;
        public ChangeQuantityByValueCommandHandler(DataContext dbCon)
        {
            _dbCon = dbCon;
        }
        public async Task<Unit> Handle(ChangeQuantityByValueCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbCon.Products.FindAsync(request.Id);
            if (product == null)
            {
                throw new CustomException(404, "The Product Not Found");
            }

            if(product.Quantity - Math.Abs(request.Value) < 0)
            {
                throw new CustomException(400, "Sorry but present Stock can't fulfill your Request");
            }

            product.Quantity += -1*Math.Abs(request.Value);
            await _dbCon.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
