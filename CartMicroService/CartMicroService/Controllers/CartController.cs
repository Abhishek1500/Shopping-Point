using CartMicroService.Commands;
using CartMicroService.DTOS;
using CartMicroService.Helper;
using CartMicroService.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Encodings;
using Week3Assignment.ExceptionHandler;

namespace CartMicroService.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {

        private IMediator _mediator;
        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles ="user")]
        [HttpPost("item")]
        public async Task<ActionResult> AddProductToCart(AddCartRequestDto cartAddDto)
        {
            var bearerToken = Request.Headers.Authorization;
            var userId = User.GetUserId();
            try
            {
                await _mediator.Send(new AddCartRequestCommand(userId, cartAddDto, bearerToken, "carted"));
                return NoContent();
            }
            catch (CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }


        [Authorize(Roles = "user")]
        [HttpPut("item/{id}/request")]

        public async Task<ActionResult> RequestTheCartItem(int id)
        {
            var UserId = User.GetUserId();
            try
            {
                await _mediator.Send(new CartToRequestCommand(UserId, id));
                return NoContent();
            } catch (CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }


        [Authorize(Roles = "user")]
        [HttpDelete("item/{id}")]
        public async Task<ActionResult> DeleteTheCartItem(int id)
        {
            var UserId = User.GetUserId();
            try
            {
                await _mediator.Send(new DeleteCartItemCommand(id, UserId));
                return NoContent();
            } catch (CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }


        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<ActionResult<List<HistorySendDto>>> MyCart()
        {
            var bearerToken = Request.Headers.Authorization;
            var UserId = User.GetUserId();
            try
            {
                return await _mediator.Send(new GetUserCartQuery(UserId,bearerToken));
            }catch(CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }

        }


        [Authorize(Roles ="user")]
        [HttpPut("item/{id}/newAmt")]
        public async Task<ActionResult> CartCountChange(int Id ,ChangeCartCountDto cdto)
        {
            var bearerToken = Request.Headers.Authorization;
            var UserId = User.GetUserId();
            try
            {
                await _mediator.Send(new CartAmtChangeCommand(Id,cdto.count, UserId, bearerToken));
                return NoContent();
            }catch(CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }







    }
}
