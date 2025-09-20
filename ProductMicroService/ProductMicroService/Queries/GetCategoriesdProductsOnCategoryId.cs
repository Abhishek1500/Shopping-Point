using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductMicroService.Data;
using ProductMicroService.DTOS;
using Week3Assignment.ExceptionHandler;

namespace ProductMicroService.Queries
{
    public class GetCategorisedProductsOnCategoryId: IRequest<CategoryProductSendDto>
    {
        public int Id;
        public GetCategorisedProductsOnCategoryId(int id)
        {
            Id = id;
        }
    }

    public class GetCategorisedProductsOnCategoryIdHandler : IRequestHandler<GetCategorisedProductsOnCategoryId, CategoryProductSendDto>
    {
        private DataContext _dbCon;
        private IMapper _mapper;
        public GetCategorisedProductsOnCategoryIdHandler(DataContext dbCon, IMapper mapper)
        {
            _mapper = mapper;
            _dbCon = dbCon;
        }
        public async Task<CategoryProductSendDto> Handle(GetCategorisedProductsOnCategoryId request, CancellationToken cancellationToken)
        {
            var categorisedProducts = await _dbCon.Categories.Include(x=>x.Products).Where(x=>x.Id==request.Id).FirstOrDefaultAsync();

            if (categorisedProducts==null)
            {
                throw new CustomException(404, "There Are No Categories Present");
            }
            return _mapper.Map<CategoryProductSendDto>(categorisedProducts);
        }
    }

}
