using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserMicroService.Commands;
using UserMicroService.DTOS;
using UserMicroService.Queries;
using Week3Assignment.ExceptionHandler;

namespace UserMicroService.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController :ControllerBase
    {
        IMediator _mediator;

        public AccountController(IMediator meditor)
        {
            _mediator = meditor;
        }



        [HttpPost("register")]
        public async Task<ActionResult<LoginRegisterSendDto>> registerUser(RegisterUserDto rdto)
        {
            try
            {
                return await _mediator.Send(new RegisterUserCommand(rdto));
            }catch(CustomException ex)
            {
                Response.StatusCode = ex.StatusCode;
                throw ex;
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<LoginRegisterSendDto>> loginUser(UserLoginDto ldto)
        {
            try
            {
                return await _mediator.Send(new UserLoginQuery(ldto));
            }
            catch (CustomException ex)
            {
                Response.StatusCode = ex.StatusCode;
                throw ex;
            }
        }

    }
}
