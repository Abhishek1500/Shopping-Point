using CartMicroService.Commands;
using CartMicroService.DTOS;
using CartMicroService.Helper;
using CartMicroService.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Week3Assignment.ExceptionHandler;

namespace CartMicroService.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {

        IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Authorize(Roles = "user")]
        [HttpPost("request")]
        public async Task<ActionResult> CreateNewRequest(AddCartRequestDto cartAddDto)
        {
            var bearerToken = Request.Headers.Authorization;
            var userId = User.GetUserId();
            try
            {
                await _mediator.Send(new AddCartRequestCommand(userId, cartAddDto, bearerToken,"pending"));
                return NoContent();
            }
            catch (CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }


        [Authorize(Roles = "user")]
        [HttpGet("myrequest")]
        public async Task<ActionResult<List<HistorySendDto>>> MyRequest()
        {
            var bearetoken = Request.Headers.Authorization;
            var UserId = User.GetUserId();
            try
            {
                var cart = await _mediator.Send(new GetUserRequestsQuery(UserId, bearetoken));
                return cart;
            }
            catch (CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }


        [Authorize(Roles = "user")]
        [HttpGet("myrequesthistory")]
        public async Task<ActionResult<List<HistorySendDto>>> MyRequestHistory()
        {
            var bearetoken = Request.Headers.Authorization;
            var UserId = User.GetUserId();
            try
            {
                var cart = await _mediator.Send(new GetUserRequestHistoryQuery(UserId, bearetoken));
                return cart;
            }
            catch (CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }
        }



        [Authorize(Roles = "admin")]
        [HttpGet("requests")]
        public async Task<ActionResult<List<RequestSendDto>>> GetAllRequests()
        {
            var bearertoken = Request.Headers.Authorization;
            try
            {
                var list = await _mediator.Send(new GetAllRequestsQuery(bearertoken));
                return list;
            }
            catch (CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;

            }
        }


        [Authorize(Roles = "admin")]
        [HttpPut("request/{id}/respond")]
        public async Task<ActionResult> RespondToRequest(int id,RespondToRequestDto respond)
        {
            var bearertoken = Request.Headers.Authorization;
            try
            {
                await _mediator.Send(new RespondToCartRequestCommand(id, respond, bearertoken));
                return NoContent();
            }catch(CustomException e)
            {
                Response.StatusCode = e.StatusCode;
                throw e;
            }

        }





    }
}
