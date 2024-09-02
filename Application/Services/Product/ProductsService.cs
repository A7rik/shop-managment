using Domain.Models.Product;
using Domain.Models.Utils;
using Infrastructure.Repository.Product;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Product
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
        public Task<ApiResponseModel<List<ProductModel>>> ProductsByCategoryNameAsync(string categoryName)
        {
            return _productsRepository.ProductsByCategoryNameAsync(categoryName);
        }
        public Task<ApiResponseModel<List<ProductModel>>> ListProductsByNameAsync(string searchString)
        {
            return _productsRepository.ListProductsByNameAsync(searchString);
        }
        public async Task<ApiResponseModel<int>> TotalNumberOfProductsAsync()
        {
            var productsResponse = await _productsRepository.GetProductsAsync();

            if (productsResponse.IsSuccess && productsResponse.Data != null)
            {
                int productCount = productsResponse.Data.Count;

                return new ApiResponseModel<int>
                {
                    IsSuccess = true,
                    Message = "Total number of products retrieved successfully",
                    Data = productCount
                };
            }
            else
            {
                return new ApiResponseModel<int>
                {
                    IsSuccess = false,
                    Message = "Failed to retrieve products",
                    Data = 0
                };
            }
        }

        public async Task<ApiResponseModel<List<ProductModel>>> GetPagedProductsAsync(int pageNumber, int pageSize, ApiResponseModel<List<ProductModel>> products, string sortBy)
        {
            if (!products.IsSuccess || products.Data == null)
            {
                return new ApiResponseModel<List<ProductModel>>
                {
                    IsSuccess = false,
                    Message = "Failed to retrieve products"
                };
            }

            IEnumerable<ProductModel> sortedProducts = products.Data;

            if (sortBy == "updated")
            {
                sortedProducts = sortedProducts.OrderByDescending(p => p.UpdatedDate);
            }
            else
            {
                sortedProducts = sortedProducts.OrderByDescending(p => p.CreatedDate);
            }

            var pagedProducts = sortedProducts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var result = new ApiResponseModel<List<ProductModel>>();
            result.Data = pagedProducts;
            result.IsSuccess = true;
            result.Message = $"Page {pageNumber} of products retrieved successfully";

            return result;
        }


    }
}
