using Microsoft.AspNetCore.Mvc;
using Application.Services.Product;
using Domain.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Domain.Models.Utils;

[Route("api/[controller]/[action]")]
[ApiController]

public class ProductsController : ControllerBase
{
    private readonly IProductsService _productService;

    public ProductsController(IProductsService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponseModel<List<ProductModel>>>> GetProducts()
    {
        var result = await _productService.GetAllProductsAsync();
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponseModel<List<ProductModel>>>> GetProductById(int id)
    {
        var result = await _productService.GetProductByIdAsync(id);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpPost]
    public async Task<ActionResult<ApiResponseModel<ProductModel>>> CreateProduct([FromBody] CreateProductRequestModel model)
    {
        var result = await _productService.CreateProductAsync(model);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponseModel<ProductModel>>> UpdateProduct(int id, [FromBody] UpdateProductRequestModel model)
    {
        model.Id = id;
        var result = await _productService.UpdateProductAsync(model);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponseModel<bool>>> DeleteProduct(int id)
    {
        var result = await _productService.DeleteProductAsync(id);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponseModel<int>>> TotalNumberOfProducts()
    {
        var result = await _productService.TotalNumberOfProductsAsync();
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpGet("{pageNumber},{pageSize},{sortBy}")]
    public async Task<ActionResult<ApiResponseModel<List<ProductModel>>>> GetProductsForAllCategory(int pageNumber, int pageSize, string sortBy)
    {
        var products = await _productService.GetAllProductsAsync();
        var result = await _productService.GetPagedProductsAsync(pageNumber, pageSize, products, sortBy);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpGet("{searchString},{pageNumber},{pageSize},{sortBy}")]
    public async Task<ActionResult<ApiResponseModel<List<ProductModel>>>> ListProductsByName(string searchString, int pageNumber, int pageSize, string sortBy)
    {
        var products = await _productService.ListProductsByNameAsync(searchString);
        var result = await _productService.GetPagedProductsAsync(pageNumber, pageSize, products, sortBy);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpGet("{categoryName},{pageNumber},{pageSize},{sortBy}")]
    public async Task<ActionResult<ApiResponseModel<List<ProductModel>>>> ProductsByCategoryName(string categoryName, int pageNumber, int pageSize, string sortBy)
    {
        var products = await _productService.ProductsByCategoryNameAsync(categoryName);
        var result = await _productService.GetPagedProductsAsync(pageNumber, pageSize, products, sortBy);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}