using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserMicroService.Commands;
using UserMicroService.DTOS;
using UserMicroService.Helper;
using UserMicroService.Queries;
using Week3Assignment.ExceptionHandler;

namespace UserMicroService.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {

        IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles ="admin")]
        [HttpGet("users")]
        public async Task<ActionResult<List<UserSendDto>>> GetAllUsers()
        {
            try {
                return await _mediator.Send(new GetAllUsersQuery());

            }catch(CustomException ex)
            {
                Response.StatusCode = ex.StatusCode;
                throw ex;
            }
        }

        [Authorize(Roles = "admin,user")]
        [HttpGet("users/{id}")]
        public async Task<ActionResult<UserSendDto>> GetUserById(int id)
        {

            var UserId = User.GetUserId();
            try
            {
                if (User.GetRole() != "admin" && UserId != id)
                    throw new CustomException(403, "Not Allowed to get data of this user");
                return await _mediator.Send(new GetUserByIdQuery(id));

            }
            catch (CustomException ex)
            {
                Response.StatusCode = ex.StatusCode;
                throw ex;
            }
        }

        [Authorize(Roles ="admin,user")]
        [HttpPut("user")]
        public async Task<ActionResult<UserSendDto>> UpdateUser(UpdateUserDto udto)
        {
            var UserId = User.GetUserId();
            try
            {
                return await _mediator.Send(new UserUpdateCommand(udto,UserId));

            }
            catch (CustomException ex)
            {
                Response.StatusCode = ex.StatusCode;
                throw ex;
            }
        }


        [Authorize(Roles = "admin,user")]
        [HttpPut("user/changepassword")]
        public async Task<ActionResult> changePassword(ChangePasswordDto udto)
        {
            var UserId = User.GetUserId();
            try
            {
                await _mediator.Send(new PasswordChangeCommand(udto, UserId));
                return NoContent();

            }
            catch (CustomException ex)
            {
                Response.StatusCode = ex.StatusCode;
                throw ex;
            }
        }


    }
}
