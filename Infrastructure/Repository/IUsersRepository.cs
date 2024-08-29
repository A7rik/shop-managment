using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{



    public interface IUsersRepository
    {
        Task<ApiResponseModel<List<UserModel>>> GetUsersAsync();
        Task<ApiResponseModel<UserModel>> GetUserByIdAsync(int id);
        Task<ApiResponseModel<int>> CreateUserAsync(CreateUserRequestModel user);
        Task<ApiResponseModel<int>> UpdateUserAsync(UpdateUserRequestModel user);
        Task<ApiResponseModel<int>> DeleteUserAsync(int id);
    }

}
