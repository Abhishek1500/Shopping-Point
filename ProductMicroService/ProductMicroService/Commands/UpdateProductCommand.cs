using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductMicroService.Data;
using ProductMicroService.DTOS;
using ProductMicroService.Models;
using Week3Assignment.ExceptionHandler;

namespace ProductMicroService.Commands
{
    public class UpdateProductCommand : MediatR.IRequest<ProductSendDto>
    {
        public AddUpdateProductDto ProductAddDto;
        public int Id;
        public UpdateProductCommand(AddUpdateProductDto productdto,int id)
        {
            Id = id;
            ProductAddDto = productdto;
        }
    }


    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductSendDto>
    {
        private IMapper _mapper;
        private DataContext _dbCon;
        public UpdateProductCommandHandler(IMapper mapper, DataContext dbCon)
        {
            _dbCon = dbCon;
            _mapper = mapper;

        }
        public async Task<ProductSendDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productexistance = await _dbCon.Products.Where(x => x.ProductName == request.ProductAddDto.ProductName.ToUpper().Trim()
            && x.ProductCompany == request.ProductAddDto.ProductCompany.ToUpper().Trim()&&request.Id!=x.Id).FirstOrDefaultAsync();
            if (productexistance != null)
            {
                throw new CustomException(400, "The Product with same name and company Already Exist cant Update to this values");
            }


            var product = _dbCon.Products.Find(request.Id);
            if (product == null)
            {
                throw new CustomException(404, "Product Not Found");
            }
            var cat = await _dbCon.Categories.FindAsync(request.ProductAddDto.CategoryId);
            if (cat == null)
            {
                throw new CustomException(404, "Category Not Found");
            }
            _mapper.Map(request.ProductAddDto, product);
            //Experiment this part
            product.Category = cat;
            await _dbCon.SaveChangesAsync();
            return _mapper.Map<ProductSendDto>(product);
        }
    }



}
