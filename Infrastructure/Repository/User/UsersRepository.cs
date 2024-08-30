using System.Data;
using Dapper;
using Domain.Models;
using Domain.Models.User;
using Infrastructure.Repository.User;

public class UsersRepository : IUsersRepository
{
    private readonly IDatabaseConnection _dbConnection;

    public UsersRepository(IDatabaseConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<ApiResponseModel<List<UserModel>>> GetUsersAsync()
    {
        var result = new ApiResponseModel<List<UserModel>>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_GetUsers";
        result.Data = (await connection.Data.QueryAsync<UserModel>(command, null, commandType: CommandType.StoredProcedure)).ToList();
        result.IsSuccess = true;
        result.Message = "";


        return result;
    }

    public async Task<ApiResponseModel<UserModel>> GetUserByIdAsync(int id)
    {
        var result = new ApiResponseModel<UserModel>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_GetUserById";
        result.Data = await connection.Data.QuerySingleOrDefaultAsync<UserModel>(command, new { Id = id }, commandType: CommandType.StoredProcedure);
        result.IsSuccess = result.Data != null;
        result.Message = "";


        return result;
    }

    public async Task<ApiResponseModel<int>> CreateUserAsync(CreateUserRequestModel user)
    {
        var result = new ApiResponseModel<int>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_CreateUser";
        var parameters = new
        {
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.Email
        };
        result.Data = await connection.Data.ExecuteScalarAsync<int>(command, parameters, commandType: CommandType.StoredProcedure);
        result.IsSuccess = true;
        result.Message = "User created successfully";


        return result;
    }

    public async Task<ApiResponseModel<int>> UpdateUserAsync(UpdateUserRequestModel user)
    {
        var result = new ApiResponseModel<int>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_UpdateUser";
        var parameters = new
        {
            user.Id,
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.Email
        };
        result.Data = await connection.Data.ExecuteAsync(command, parameters, commandType: CommandType.StoredProcedure);
        result.IsSuccess = true;
        result.Message = "User updated successfully";


        return result;
    }

    public async Task<ApiResponseModel<int>> DeleteUserAsync(int id)
    {
        var result = new ApiResponseModel<int>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_DeleteUser";
        result.Data = await connection.Data.ExecuteAsync(command, new { Id = id }, commandType: CommandType.StoredProcedure);
        result.IsSuccess = true;
        result.Message = "User deleted successfully";


        return result;
    }
}
