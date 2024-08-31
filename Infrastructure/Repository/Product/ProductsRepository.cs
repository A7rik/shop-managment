using System.Data;
using Dapper;
using Domain.Models.Product;
using Domain.Models.Utils;
using Infrastructure.Repository.Product;

public class ProductsRepository : IProductsRepository
{
    private readonly IDatabaseConnection _dbConnection;

    public ProductsRepository(IDatabaseConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<ApiResponseModel<List<ProductModel>>> GetProductsAsync()
    {
        var result = new ApiResponseModel<List<ProductModel>>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_GetProducts";
        result.Data = (await connection.Data.QueryAsync<ProductModel>(command, null, commandType: CommandType.StoredProcedure)).ToList();
        result.IsSuccess = true;
        result.Message = "";


        return result;
    }

    public async Task<ApiResponseModel<ProductModel>> GetProductByIdAsync(int id)
    {
        var result = new ApiResponseModel<ProductModel>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_GetProductById";
        result.Data = await connection.Data.QuerySingleOrDefaultAsync<ProductModel>(command, new { Id = id }, commandType: CommandType.StoredProcedure);
        result.IsSuccess = result.Data != null;
        result.Message = "";

        return result;
    }

    public async Task<ApiResponseModel<int>> CreateProductAsync(CreateProductRequestModel product)
    {
        var result = new ApiResponseModel<int>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_CreateProduct";
        var parameters = new
        {
            product.Name,
            product.Description,
            product.Price,
            product.CategoryId,
            product.Available
        };
        result.Data = await connection.Data.ExecuteScalarAsync<int>(command, parameters, commandType: CommandType.StoredProcedure);
        result.IsSuccess = true;
        result.Message = "Product created successfully";


        return result;
    }

    public async Task<ApiResponseModel<int>> UpdateProductAsync(UpdateProductRequestModel product)
    {
        var result = new ApiResponseModel<int>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_UpdateProduct";
        var parameters = new
        {
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.CategoryId,
            product.Available
        };
        result.Data = await connection.Data.ExecuteAsync(command, parameters, commandType: CommandType.StoredProcedure);
        result.IsSuccess = true;
        result.Message = "Product updated successfully";


        return result;
    }

    public async Task<ApiResponseModel<int>> DeleteProductAsync(int id)
    {
        var result = new ApiResponseModel<int>();

        var connection = await _dbConnection.GetConnection();
        result.IsSuccess = connection.IsSuccess;
        result.Message = connection.Message;
        if (!result.IsSuccess) return result;

        var command = "PRC_DeleteProduct";
        result.Data = await connection.Data.ExecuteAsync(command, new { Id = id }, commandType: CommandType.StoredProcedure);
        result.IsSuccess = true;
        result.Message = "Product deleted successfully";

        return result;
    }
}
