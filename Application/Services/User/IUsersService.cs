using Domain.Models.User;
using Domain.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.User
{
    public interface IUsersService
    {
        Task<ApiResponseModel<List<UserModel>>> GetAllUsersAsync();
        Task<ApiResponseModel<UserModel>> GetUserByIdAsync(int id);
        Task<ApiResponseModel<UserModel>> UpdateUserAsync(UpdateUserRequestModel user);
        Task<ApiResponseModel<int>> DeleteUserAsync(int id);
        Task<ApiResponseModel<AuthResponseModel>> LoginAsync(LoginRequestModel loginRequest);
        Task<ApiResponseModel<AuthResponseModel>> SignUpAsync(CreateUserRequestModel createUserRequest);
        ApiResponseModel LogoutUser(string token);
        Task<ApiResponseModel<int>> UpdateUserRoleByEmailAsync(UpdateUserRoleByEmailModel user);
    }
}
