using System.Data;
using Dapper;
using Domain.Models.User;
using Domain.Models.Utils;
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

    public async Task<ApiResponseModel<UserModel>> CreateUserAsync(CreateUserRequestModel user)
    {
        var result = new ApiResponseModel<UserModel>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_CreateUser";
        var parameters = new DynamicParameters();
        parameters.Add("@FirstName", user.FirstName);
        parameters.Add("@LastName", user.LastName);
        parameters.Add("@PhoneNumber", user.PhoneNumber);
        parameters.Add("@Email", user.Email);
        parameters.Add("@HashedPassword", user.Password);


        result.Data = await connection.Data.QueryFirstOrDefaultAsync<UserModel>(
            command,
            parameters,
            commandType: CommandType.StoredProcedure
        );
        result.IsSuccess = true;
        result.Message = "User created successfully";


        return result;
    }

    public async Task<ApiResponseModel<UserModel>> UpdateUserAsync(UpdateUserRequestModel user)
    {
        var result = new ApiResponseModel<UserModel>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_UpdateUser";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", user.Id);
        parameters.Add("@FirstName", user.FirstName);
        parameters.Add("@LastName", user.LastName);
        parameters.Add("@PhoneNumber", user.PhoneNumber);
        parameters.Add("@Email", user.Email);
        parameters.Add("@HashedPassword", user.Password);
        result.Data = await connection.Data.QueryFirstOrDefaultAsync<UserModel>(
            command,
            parameters,
            commandType: CommandType.StoredProcedure
        );
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
    public async Task<ApiResponseModel<UserAndPasswordModel>> LoginAsync(string Email)
    {
        var result = new ApiResponseModel<UserAndPasswordModel>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;
        var command = "PRC_GetUserByEmail";
        var parameters = new DynamicParameters();
        parameters.Add("@Email", Email);


        result.Data = await connection.Data.QueryFirstOrDefaultAsync<UserAndPasswordModel>(
            command,
            parameters,
            commandType: CommandType.StoredProcedure
        );

        return result;
    }

    public async Task<ApiResponseModel<int>> UpdateUserRoleByEmailAsync(UpdateUserRoleByEmailModel model)
    {
        var result = new ApiResponseModel<int>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_UpdateUserRoleByEmail";
        var parameters = new DynamicParameters();
        parameters.Add("@Email", model.Email);
        parameters.Add("@RoleID", model.RoleID);
        result.Data = await connection.Data.ExecuteAsync(command, parameters, commandType: CommandType.StoredProcedure);
        result.IsSuccess = true;
        result.Message = "User role updated successfully";
        return result;
    }
}
