using Domain.Models.Product;
using Domain.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Product
{
    public interface IProductsRepository
    {
        Task<ApiResponseModel<List<ProductModel>>> GetProductsAsync();
        Task<ApiResponseModel<ProductModel>> GetProductByIdAsync(int id);
        Task<ApiResponseModel<int>> CreateProductAsync(CreateProductRequestModel product);
        Task<ApiResponseModel<int>> UpdateProductAsync(UpdateProductRequestModel product);
        Task<ApiResponseModel<int>> DeleteProductAsync(int id);
        Task<ApiResponseModel<List<ProductModel>>> ProductsByCategoryNameAsync(string CategoryName);
        Task<ApiResponseModel<List<ProductModel>>> ListProductsByNameAsync(string searchString);

    }

}
