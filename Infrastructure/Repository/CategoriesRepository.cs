using System.Data;
using Dapper;
using Domain.Models;
using Infrastructure.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly IDatabaseConnection _dbConnection;

    public CategoryRepository(IDatabaseConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<ApiResponseModel<List<CategoryModel>>> GetCategoriesAsync()
    {
        var result = new ApiResponseModel<List<CategoryModel>>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_GetCategories";
        result.Data = (await connection.Data.QueryAsync<CategoryModel>(command, null, commandType: CommandType.StoredProcedure)).ToList();
        result.IsSuccess = true;

        return result;
    }

    public async Task<ApiResponseModel<CategoryModel>> GetCategoryByIdAsync(int id)
    {
        var result = new ApiResponseModel<CategoryModel>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_GetCategoryById";
        result.Data = await connection.Data.QuerySingleOrDefaultAsync<CategoryModel>(command, new { Id = id }, commandType: CommandType.StoredProcedure);
        result.IsSuccess = result.Data != null;

        return result;
    }

    public async Task<ApiResponseModel<int>> CreateCategoryAsync(CreateCategoryRequestModel category)
    {
        var result = new ApiResponseModel<int>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_CreateCategory";
        var parameters = new
        {
            category.Name
        };
        result.Data = await connection.Data.ExecuteScalarAsync<int>(command, parameters, commandType: CommandType.StoredProcedure);
        result.IsSuccess = true;

        return result;
    }

    public async Task<ApiResponseModel<int>> UpdateCategoryAsync(UpdateCategoryRequestModel category)
    {
        var result = new ApiResponseModel<int>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_UpdateCategory";
        var parameters = new
        {
            category.Id,
            category.Name
        };
        result.Data = await connection.Data.ExecuteAsync(command, parameters, commandType: CommandType.StoredProcedure);
        result.IsSuccess = true;

        return result;
    }

    public async Task<ApiResponseModel<int>> DeleteCategoryAsync(int id)
    {
        var result = new ApiResponseModel<int>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_DeleteCategory";
        result.Data = await connection.Data.ExecuteAsync(command, new { Id = id }, commandType: CommandType.StoredProcedure);
        result.IsSuccess = true;

        return result;
    }
}
