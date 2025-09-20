using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductMicroService.Data;
using ProductMicroService.DTOS;
using ProductMicroService.Models;
using Week3Assignment.ExceptionHandler;

namespace ProductMicroService.Commands
{
    public class AddCategoryCommand :IRequest<CategorySendDto>
    {
        public string CategoryName;
        public AddCategoryCommand(AddUpdateCategoryDto catDto)
        {
            CategoryName = catDto.CategoryName;
        }
    }


    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, CategorySendDto>
    {
        private DataContext _dbCon;
        public AddCategoryCommandHandler(DataContext dbCon)
        {
            _dbCon = dbCon;
        }
        public async Task<CategorySendDto> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var cat=await _dbCon.Categories.Where(x => x.CategoryName == request.CategoryName.ToUpper().Trim()).FirstOrDefaultAsync();
            if (cat != null)
            {
                throw new CustomException(400, "Category is Already Present");
            }
            var newCat = new Category
            {
                CategoryName = request.CategoryName.Trim().ToUpper()
            };

            await _dbCon.AddAsync(newCat);
            await _dbCon.SaveChangesAsync();
            return new CategorySendDto
            {
                Id = newCat.Id,
                CategoryName = newCat.CategoryName
            };


        }
    }
}
