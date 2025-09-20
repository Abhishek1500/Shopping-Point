using MediatR;
using ProductMicroService.DTOS;

namespace ProductMicroService.Queries
{
    public class GetCategories : IRequest<CategorySendDto>
    {

    }
}
