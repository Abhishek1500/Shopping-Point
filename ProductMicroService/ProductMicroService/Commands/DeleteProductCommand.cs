using MediatR;
using ProductMicroService.Data;
using Week3Assignment.ExceptionHandler;

namespace ProductMicroService.Commands
{
    public class DeleteProductCommand : MediatR.IRequest<Unit>
    {
        public int Id;
        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private DataContext _dbCon;
        public DeleteProductCommandHandler(DataContext dbCon)
        {
            _dbCon = dbCon;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product=await _dbCon.Products.FindAsync(request.Id);
            if (product == null)
            {
                throw new CustomException(404, "Producr Not Found");
            }
            _dbCon.Products.Remove(product);
            await _dbCon.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
