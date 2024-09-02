using Domain.Models.Product;
using Domain.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Product
{
    public interface IProductsService
    {
        Task<ApiResponseModel<List<ProductModel>>> GetAllProductsAsync();
        Task<ApiResponseModel<ProductModel>> GetProductByIdAsync(int id);
        Task<ApiResponseModel<int>> CreateProductAsync(CreateProductRequestModel product);
        Task<ApiResponseModel<int>> UpdateProductAsync(UpdateProductRequestModel product);
        Task<ApiResponseModel<int>> DeleteProductAsync(int id);
        Task<ApiResponseModel<List<ProductModel>>> ProductsByCategoryNameAsync(string categoryName);
        Task<ApiResponseModel<List<ProductModel>>> ListProductsByNameAsync(string searchString);
        Task<ApiResponseModel<int>> TotalNumberOfProductsAsync();
        Task<ApiResponseModel<List<ProductModel>>> GetPagedProductsAsync(int pageNumber, int pageSize, ApiResponseModel<List<ProductModel>> products, string sortBy);
    }
}
