using Domain.Models;
using Domain.Models.Category;
using Infrastructure.Repository.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Category
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<ApiResponseModel<List<CategoryModel>>> GetAllCategoriesAsync()
        {
            return _categoryRepository.GetCategoriesAsync();
        }

        public Task<ApiResponseModel<CategoryModel>> GetCategoryByIdAsync(int id)
        {
            return _categoryRepository.GetCategoryByIdAsync(id);
        }

        public Task<ApiResponseModel<int>> CreateCategoryAsync(CreateCategoryRequestModel category)
        {
            return _categoryRepository.CreateCategoryAsync(category);
        }

        public Task<ApiResponseModel<int>> UpdateCategoryAsync(UpdateCategoryRequestModel category)
        {
            return _categoryRepository.UpdateCategoryAsync(category);
        }

        public Task<ApiResponseModel<int>> DeleteCategoryAsync(int id)
        {
            return _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
