using Domain.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public Task<ApiResponseModel<List<ProductModel>>> GetAllProductsAsync()
        {
            return _productsRepository.GetProductsAsync();
        }

        public Task<ApiResponseModel<ProductModel>> GetProductByIdAsync(int id)
        {
            return _productsRepository.GetProductByIdAsync(id);
        }

        public Task<ApiResponseModel<int>> CreateProductAsync(CreateProductRequestModel product)
        {
            return _productsRepository.CreateProductAsync(product);
        }

        public Task<ApiResponseModel<int>> UpdateProductAsync(UpdateProductRequestModel product)
        {
            return _productsRepository.UpdateProductAsync(product);
        }

        public Task<ApiResponseModel<int>> DeleteProductAsync(int id)
        {
            return _productsRepository.DeleteProductAsync(id);
        }
    }
}
