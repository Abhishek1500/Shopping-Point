using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductMicroService.Commands;
using ProductMicroService.DTOS;
using ProductMicroService.Queries;
using Week3Assignment.ExceptionHandler;

namespace ProductMicroService.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProductController : ControllerBase
    {
            
        private IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Authorize(Roles = "admin,user")]
        [HttpGet("product/{id}")]
        public async Task<ActionResult<ProductSendDto>> GetProductById(int id)
        {
            var token=Request.Headers.Authorization;
            try
            {
               var product= await _mediator.Send(new GetProductByIdQuery(id));
                return product;
            }catch(CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }

        [Authorize(Roles = "admin,user")]

        [HttpGet("products")]
        public async Task<ActionResult<List<ProductSendDto>>> GetAllProducts()
        {
            try
            {
               return await _mediator.Send(new GetAllProductQuery());
            }catch(CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }


        [Authorize(Roles = "admin")]
        [HttpPost("product")]

        public async Task<ActionResult<ProductSendDto>> AddProduct(AddUpdateProductDto pdto)
        {
            try
            {
                var product = await _mediator.Send(new AddProductCommand(pdto));
                return product;
            }
            catch (CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("product/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _mediator.Send(new DeleteProductCommand(id));
                return NoContent();
            }
            catch (CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("product/{id}")]
        public async Task<ActionResult<ProductSendDto>> UpdateProduct(int id, AddUpdateProductDto pDto)
        {
            try
            {
                var product = await _mediator.Send(new UpdateProductCommand(pDto, id));
                return product;
            }
            catch (CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }

        }

        [Authorize(Roles = "admin")]
        [HttpPut("product/{id}/accept")]
        public async Task<ActionResult> AcceptAndChangeValue(int id, AcceptingChangeQuantityDto Adto)
        {
            try
            {
                await _mediator.Send(new ChangeQuantityByValueCommand(id, Adto.AskedQuantity));
                return NoContent();
            }catch(CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }

    }
}
