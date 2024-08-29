﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IProductsService
    {
        Task<ApiResponseModel<List<ProductModel>>> GetAllProductsAsync();
        Task<ApiResponseModel<ProductModel>> GetProductByIdAsync(int id);
        Task<ApiResponseModel<int>> CreateProductAsync(CreateProductRequestModel product);
        Task<ApiResponseModel<int>> UpdateProductAsync(UpdateProductRequestModel product);
        Task<ApiResponseModel<int>> DeleteProductAsync(int id);
    }
}
