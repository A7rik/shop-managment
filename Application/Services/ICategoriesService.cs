using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICategoriesService
    {
        Task<ApiResponseModel<List<CategoryModel>>> GetAllCategoriesAsync();
        Task<ApiResponseModel<CategoryModel>> GetCategoryByIdAsync(int id);
        Task<ApiResponseModel<int>> CreateCategoryAsync(CreateCategoryRequestModel category);
        Task<ApiResponseModel<int>> UpdateCategoryAsync(UpdateCategoryRequestModel category);
        Task<ApiResponseModel<int>> DeleteCategoryAsync(int id);
    }
}
