using Domain.Models.User;
using Domain.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.User
{



    public interface IUsersRepository
    {
        Task<ApiResponseModel<List<UserModel>>> GetUsersAsync();
        Task<ApiResponseModel<UserModel>> GetUserByIdAsync(int id);
        Task<ApiResponseModel<UserModel>> CreateUserAsync(CreateUserRequestModel user);
        Task<ApiResponseModel<UserModel>> UpdateUserAsync(UpdateUserRequestModel user);
        Task<ApiResponseModel<int>> DeleteUserAsync(int id);
        Task<ApiResponseModel<UserAndPasswordModel>> LoginAsync(string Email);
        Task<ApiResponseModel<int>> UpdateUserRoleByEmailAsync(UpdateUserRoleByEmailModel model);
    }

}
