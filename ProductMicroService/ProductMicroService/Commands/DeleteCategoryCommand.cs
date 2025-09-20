using MediatR;
using ProductMicroService.Data;
using Week3Assignment.ExceptionHandler;

namespace ProductMicroService.Commands
{
    public class DeleteCategoryCommand :IRequest<Unit>
    {
        public int Id;
        public DeleteCategoryCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private DataContext _dbCon;
        public DeleteCategoryCommandHandler(DataContext dbCon)
        {
            _dbCon = dbCon;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var cat=await _dbCon.Categories.FindAsync(request.Id);
            if (cat == null)
            {
                throw new CustomException(404, "Category Not Found");
            }
            _dbCon.Categories.Remove(cat);
            await _dbCon.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
