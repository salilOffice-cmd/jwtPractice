using Application.DTOs;
using Application.UserCQRS.Commands;
using Application.UserCQRS.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class UserController : APIControllerBase
    {
        [HttpGet]
        [Route("users")]
        public async Task<ActionResult> GetAllUsersAsync()
        {
            var gotAllEmployees = await Mediator.Send(new GetAllUsers());
            return Ok(gotAllEmployees);
        }

        [HttpGet]
        [Route("GetUserByUserName/{_username}")]
        public async Task<ActionResult> GetUserByUserNameAsync([FromRoute] string _username)
        {
            var gotEmployee = await Mediator.Send(new GetUserByUserName { _userName = _username });


            if (gotEmployee == null)
            {
                return Ok(new { msg = "User not found" });
            }

            return Ok(gotEmployee);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> register([FromBody] RegisterUserDTO _regiserUserDTO)
        {
            var responseMessage = await Mediator.Send(new RegisterUser { registerUserDTO = _regiserUserDTO });

            return Ok(responseMessage);
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> login([FromBody] LoginUserDTO _loginUserdto)
        {
            var responseMsg = await Mediator.Send(new LoginUser { loginUserDTO = _loginUserdto });

            return Ok(responseMsg);

        }
    }
}
