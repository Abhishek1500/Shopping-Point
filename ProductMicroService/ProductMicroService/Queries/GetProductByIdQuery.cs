using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductMicroService.Data;
using ProductMicroService.DTOS;
using Week3Assignment.ExceptionHandler;

namespace ProductMicroService.Queries
{
    public class GetProductByIdQuery:IRequest<ProductSendDto>
    {
        public int Id;
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }


    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductSendDto>
    {
        private DataContext _dbCon;
        private IMapper _mapper;
        public GetProductByIdQueryHandler(DataContext dbCon,IMapper mapper)
        {
            _dbCon = dbCon;
            _mapper = mapper;
        }


        public async Task<ProductSendDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _dbCon.Products.Include(x => x.Category).FirstOrDefaultAsync(x=>x.Id==request.Id);
            
            if (product == null)
            {
                throw new CustomException(404, "Product Not Found");
            }

            return _mapper.Map<ProductSendDto>(product);

        }
    }
}
