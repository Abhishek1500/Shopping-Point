using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductMicroService.Data;
using ProductMicroService.DTOS;
using ProductMicroService.Models;
using Week3Assignment.ExceptionHandler;

namespace ProductMicroService.Commands
{
    public class AddProductCommand : MediatR.IRequest<ProductSendDto>
    {
        public AddUpdateProductDto ProductAddDto;
        public AddProductCommand(AddUpdateProductDto productdto)
        {
            ProductAddDto = productdto;
        }
    }

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ProductSendDto>
    {
        private IMapper _mapper;
        private DataContext _dbCon;
        public AddProductCommandHandler(IMapper mapper,DataContext dbCon)
        {
            _dbCon = dbCon;
            _mapper = mapper;

        }
        public async Task<ProductSendDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var productexistance = await _dbCon.Products.Where(x => x.ProductName == request.ProductAddDto.ProductName.ToUpper().Trim() 
            && x.ProductCompany == request.ProductAddDto.ProductCompany.ToUpper().Trim()).FirstOrDefaultAsync();
            if (productexistance != null)
            {
                throw new CustomException(400, "The Product with same and company already exist");
            }

            var cat = await _dbCon.Categories.FindAsync(request.ProductAddDto.CategoryId);
            if (cat == null)
            {
                throw new CustomException(404, "Category Not Found");
            }

            Product product=_mapper.Map<Product>(request.ProductAddDto);
            product.Category = cat;
            await _dbCon.Products.AddAsync(product);
            await _dbCon.SaveChangesAsync();
            return _mapper.Map<ProductSendDto>(product);
        }
    }
}
