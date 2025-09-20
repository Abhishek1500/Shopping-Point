using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductMicroService.Data;
using ProductMicroService.DTOS;
using Week3Assignment.ExceptionHandler;

namespace ProductMicroService.Queries
{
    public class GetAllProductQuery :IRequest<List<ProductSendDto>>
    {}

    public class GetAllProductQueyHandler : IRequestHandler<GetAllProductQuery, List<ProductSendDto>>
    {

        private DataContext _dbCon;
        private IMapper _mapper;
        public GetAllProductQueyHandler(DataContext dbCon, IMapper mapper)
        {
            _dbCon = dbCon;
            _mapper = mapper;
        }

        public async Task<List<ProductSendDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _dbCon.Products.Include(x=>x.Category).ToListAsync();
            if (products.IsNullOrEmpty())
            {
                throw new CustomException(404, "Products Not Found");
            }
            return _mapper.Map<List<ProductSendDto>>(products);
        }

    }


}
