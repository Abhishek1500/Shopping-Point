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
    public class GetCategorisedProductQuery : IRequest<List<CategoryProductSendDto>>
    { }

    public class GetCategorisedproductQueryHandler : IRequestHandler<GetCategorisedProductQuery, List<CategoryProductSendDto>>
    {

        private DataContext _dbCon;
        private IMapper _mapper;
        public GetCategorisedproductQueryHandler(DataContext dbCon,IMapper mapper)
        {
            _mapper = mapper;
            _dbCon = dbCon;
        }

        public async Task<List<CategoryProductSendDto>> Handle(GetCategorisedProductQuery request, CancellationToken cancellationToken)
        {
            var categorisedProducts = _dbCon.Categories.ProjectTo<CategoryProductSendDto>(_mapper.ConfigurationProvider);
            if (categorisedProducts.IsNullOrEmpty())
            {
                throw new CustomException(404, "There Are No Categories Present");
            }
            return await categorisedProducts.ToListAsync();
        }
    }
}
