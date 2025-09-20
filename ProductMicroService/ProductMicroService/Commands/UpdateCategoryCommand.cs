using MediatR;
using ProductMicroService.Data;
using ProductMicroService.DTOS;
using ProductMicroService.Models;
using Week3Assignment.ExceptionHandler;

namespace ProductMicroService.Commands
{
    public class UpdateCategoryCommand : IRequest<CategorySendDto>
    {
        public string CategoryName;
        public int Id;
        public UpdateCategoryCommand(AddUpdateCategoryDto catDto,int id)
        {
            CategoryName = catDto.CategoryName;
            Id = id;
        }
    }


    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategorySendDto>
    {
        private DataContext _dbCon;
        public UpdateCategoryCommandHandler(DataContext dbCon)
        {
            _dbCon = dbCon;
        }
        public async Task<CategorySendDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var cat = await _dbCon.Categories.FindAsync(request.Id);
            if (cat == null)
            {
                throw new CustomException(404, "Category Not Found");
            }

            if (cat.CategoryName == request.CategoryName.ToUpper().Trim())
            {
                throw new CustomException(400, "Please Change the Category Name on this route");
            }

            if (_dbCon.Categories.Where(x=>x.CategoryName== request.CategoryName.ToUpper().Trim()&&x.Id!=request.Id).FirstOrDefault() != null)
            {
                throw new CustomException(400, "The Category With Same Name Exist");
            }

            

            cat.CategoryName = request.CategoryName.ToUpper().Trim() ;
            await _dbCon.SaveChangesAsync();
            return new CategorySendDto
            {
                Id = cat.Id,
                CategoryName = cat.CategoryName
            };


        }
    }
}
