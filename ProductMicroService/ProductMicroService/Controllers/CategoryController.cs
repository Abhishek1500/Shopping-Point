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
    public class CategoryController :ControllerBase
    {

        private IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles ="admin,user")]
        [HttpGet("category/{id}/Summary")]

        public async Task<ActionResult<CategoryProductSendDto>> GetCategorySummayForId(int id)
        {
            try
            {
               var categorysummary= await _mediator.Send(new GetCategorisedProductsOnCategoryId(id));
                return categorysummary;
            }catch(CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }



        [Authorize(Roles = "admin,user")]
        [HttpGet("categories/Summary")]

        public async Task<ActionResult<List<CategoryProductSendDto>>> GetCategorySummay()
        {
            try
            {
                var categorysummary = await _mediator.Send(new GetCategorisedProductQuery());
                return categorysummary;
            }
            catch (CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }


        [Authorize(Roles = "admin")]
        [HttpPost("category")]
        public async Task<ActionResult<CategorySendDto>> CreateNewCategory(AddUpdateCategoryDto cdto)
        {
            try
            {
                var newcat=await _mediator.Send(new AddCategoryCommand(cdto));
                return newcat;
            }catch(CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("category/{id}")]
        public async Task<ActionResult<CategorySendDto>> UpdateCategory(int id,AddUpdateCategoryDto cdto)
        {
            try
            {
                var newcat = await _mediator.Send(new UpdateCategoryCommand(cdto,id));
                return newcat;
            }
            catch (CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("Category/{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                await _mediator.Send(new DeleteCategoryCommand(id));
                return NoContent();
            }catch(CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }


    }
}
