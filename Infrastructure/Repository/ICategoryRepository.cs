using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    
    public interface ICategoryRepository

    {
        Task<ApiResponseModel<List<CategoryModel>>> GetCategoriesAsync();
        Task<ApiResponseModel<CategoryModel>> GetCategoryByIdAsync(int id);
        Task<ApiResponseModel<int>> CreateCategoryAsync(CreateCategoryRequestModel category);
        Task<ApiResponseModel<int>> UpdateCategoryAsync(UpdateCategoryRequestModel category);
        Task<ApiResponseModel<int>> DeleteCategoryAsync(int id);


    }
}
