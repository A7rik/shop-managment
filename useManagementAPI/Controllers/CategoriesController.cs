using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Category;
using Domain.Models.Category;
using Domain.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Domain.Models.Utils;


namespace useManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoryService;

        public CategoriesController(ICategoriesService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseModel<List<CategoryModel>>>> GetCategories()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseModel<List<ProductModel>>>> GetCategoryById(int id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponseModel<int>>> CreateCategory([FromBody] CreateCategoryRequestModel model)
        {
            var result = await _categoryService.CreateCategoryAsync(model);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseModel<bool>>> UpdateCategory(int id, [FromBody] UpdateCategoryRequestModel model)
        {
            model.Id = id;
            var result = await _categoryService.UpdateCategoryAsync(model);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseModel<bool>>> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
     

    }

}
