using Domain.Models.User;
using Infrastructure.Repository.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Middleware;
using Application.Services.Jwt;
using Domain.Models.Utils;

namespace Application.Services.User
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;


        public UsersService(IUsersRepository usersRepository, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public Task<ApiResponseModel<List<UserModel>>> GetAllUsersAsync()
        {
            return _usersRepository.GetUsersAsync();
        }

        public Task<ApiResponseModel<UserModel>> GetUserByIdAsync(int id)
        {
            return _usersRepository.GetUserByIdAsync(id);
        }


        public Task<ApiResponseModel<UserModel>> UpdateUserAsync(UpdateUserRequestModel user)
        {
            var hashedPassword = _passwordHasher.HashPassword(user.Password);
            user.Password = hashedPassword;
            return _usersRepository.UpdateUserAsync(user);
        }

        public Task<ApiResponseModel<int>> DeleteUserAsync(int id)
        {
            return _usersRepository.DeleteUserAsync(id);
        }

        public async Task<ApiResponseModel<AuthResponseModel>> LoginAsync(LoginRequestModel loginRequest)
        {
            var response = new ApiResponseModel<AuthResponseModel>();

            var user = await _usersRepository.LoginAsync(loginRequest.Email);
            if (user == null || !_passwordHasher.VerifyPassword(loginRequest.Password, user.Data.HashedPassword))
            {
                response.IsSuccess = false;
                response.Message = "Invalid email or password.";
                return response;
            }
            UserModel userModel = new UserModel();
            userModel.Id = user.Data.Id;
            userModel.FirstName = user.Data.FirstName;
            userModel.LastName = user.Data.LastName;
            userModel.Email = user.Data.Email;
            userModel.PhoneNumber = user.Data.PhoneNumber;
            userModel.RoleID = user.Data.RoleID;

            var token = _jwtService.GenerateToken(userModel);

            var authResponse = new AuthResponseModel
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtService.TokenExpiryMinutes),
                User = userModel
            };

            response.Data = authResponse;
            response.IsSuccess = true;
            response.Message = "Login successful.";
            return response;
        }
        public async Task<ApiResponseModel<AuthResponseModel>> SignUpAsync(CreateUserRequestModel createUserRequest)
        {
            var response = new ApiResponseModel<AuthResponseModel>();

            var hashedPassword = _passwordHasher.HashPassword(createUserRequest.Password);

            createUserRequest.Password = hashedPassword;

            var userCreationResult = await _usersRepository.CreateUserAsync(createUserRequest);

            if (!userCreationResult.IsSuccess)
            {
                response.IsSuccess = false;
                response.Message = userCreationResult.Message;
                return response;
            }

            var token = _jwtService.GenerateToken(userCreationResult.Data);

            var authResponse = new AuthResponseModel
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtService.TokenExpiryMinutes),
                User = userCreationResult.Data
            };

            response.Data = authResponse;
            response.IsSuccess = true;
            response.Message = "User created successfully and authenticated.";
            return response;
        }
        public ApiResponseModel LogoutUser(string token)
        {
            var result = new ApiResponseModel();

            TokenBlacklistMiddleware.BlacklistToken(token);

            result.IsSuccess = true;
            result.Message = "User logged out successfully";

            return result;
        }
        public Task<ApiResponseModel<int>> UpdateUserRoleByEmailAsync(UpdateUserRoleByEmailModel user)
        {
            return _usersRepository.UpdateUserRoleByEmailAsync(user);
        }
     
    }
}
