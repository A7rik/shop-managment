﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUsersService
    {
        Task<ApiResponseModel<List<UserModel>>> GetAllUsersAsync();
        Task<ApiResponseModel<UserModel>> GetUserByIdAsync(int id);
        Task<ApiResponseModel<int>> CreateUserAsync(CreateUserRequestModel user);
        Task<ApiResponseModel<int>> UpdateUserAsync(UpdateUserRequestModel user);
        Task<ApiResponseModel<int>> DeleteUserAsync(int id);
    }
}
