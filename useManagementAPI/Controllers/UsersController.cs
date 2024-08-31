using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services.User;
using Domain.Models.User;
using Domain.Models.Product;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Domain.Models.Utils;

namespace useManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseModel<List<UserModel>>>> GetUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseModel<List<ProductModel>>>> GetUserById(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseModel<bool>>> UpdateUser(int id, [FromBody] UpdateUserRequestModel model)
        {
            model.Id = id;
            var result = await _userService.UpdateUserAsync(model);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{Email}")]
        public async Task<ActionResult<ApiResponseModel<bool>>> updateUserRole(string Email, [FromBody] UpdateUserRoleByEmailModel model)
        {
            model.Email = Email;
            var result = await _userService.UpdateUserRoleByEmailAsync(model);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseModel<bool>>> DeleteUser(int id)
        {
            var RoleID = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var result = await _userService.DeleteUserAsync(id);
            if (result.IsSuccess)
            {
                if (RoleID != "4")
                {
                    var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                    var LogOutResult = _userService.LogoutUser(token);
                    if (!LogOutResult.IsSuccess)
                        return BadRequest(new { message = LogOutResult.Message });
                    return Ok();

                }
                return Ok(result);

            }
            return BadRequest(result);


        }

    }
}