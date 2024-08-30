using Domain.Models;
using Domain.Models.Product;
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

    }

}
