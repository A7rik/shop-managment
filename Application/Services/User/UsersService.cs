using Domain.Models;
using Domain.Models.User;
using Infrastructure.Repository.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.User
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Task<ApiResponseModel<List<UserModel>>> GetAllUsersAsync()
        {
            return _usersRepository.GetUsersAsync();
        }

        public Task<ApiResponseModel<UserModel>> GetUserByIdAsync(int id)
        {
            return _usersRepository.GetUserByIdAsync(id);
        }

        public Task<ApiResponseModel<int>> CreateUserAsync(CreateUserRequestModel user)
        {
            return _usersRepository.CreateUserAsync(user);
        }

        public Task<ApiResponseModel<int>> UpdateUserAsync(UpdateUserRequestModel user)
        {
            return _usersRepository.UpdateUserAsync(user);
        }

        public Task<ApiResponseModel<int>> DeleteUserAsync(int id)
        {
            return _usersRepository.DeleteUserAsync(id);
        }
    }
}
