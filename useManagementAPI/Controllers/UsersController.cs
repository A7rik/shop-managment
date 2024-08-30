using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services.User;
using Domain.Models.User;
using Domain.Models.Product;

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

        [HttpPost]
        public async Task<ActionResult<ApiResponseModel<int>>> CreateUser([FromBody] CreateUserRequestModel model)
        {
            var result = await _userService.CreateUserAsync(model);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseModel<bool>>> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }

}
