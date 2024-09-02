using Application.Services;
using Domain.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utils;
using Application.Services.User;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Domain.Models.Product;
using Domain.Models.Utils;
namespace useManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _userService;

        public AuthController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpPost]

        
        public async Task<ActionResult<ApiResponseModel>> Login([FromBody] LoginRequestModel loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loginResult = await _userService.LoginAsync(loginRequest);

            if (!loginResult.IsSuccess)
                return Unauthorized(new { message = loginResult.Message });

            return Ok(loginResult);
            //return Ok(new
            //{
            //    Token = loginResult.Data.Token,
            //    Expiration = loginResult.Data.Expiration,
            //    User = loginResult.Data.User
            //});
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseModel>> SignUp([FromBody] CreateUserRequestModel createUserRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var signUpResult = await _userService.SignUpAsync(createUserRequest);

            if (!signUpResult.IsSuccess)
                return BadRequest(new { message = signUpResult.Message });

            return Ok(new
            {
                Token = signUpResult.Data.Token,
                Expiration = signUpResult.Data.Expiration,
                User = signUpResult.Data.User
            });
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponseModel>> LogOut()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var LogOutResult = _userService.LogoutUser(token);
            if (!LogOutResult.IsSuccess)
                return BadRequest(new { message = LogOutResult.Message });
            return Ok();
        }
    }


}
