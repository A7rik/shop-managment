using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using Domain.Models.Utils;

public interface IDatabaseConnection
{
    Task<ApiResponseModel<IDbConnection>> GetConnection();
}

public class DatabaseConnection : IDatabaseConnection
{
    private readonly string _connectionString;

    public DatabaseConnection(IOptions<DatabaseConnectionModel> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    public async Task<ApiResponseModel<IDbConnection>> GetConnection()
    {
        var response = new ApiResponseModel<IDbConnection>();
        try
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            response.IsSuccess = true;
            response.Data = connection;
            response.Message = "Database connection established.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Error connecting to database.";
            response.Errors = new List<string> { ex.Message };
        }

        return response;
    }
}
