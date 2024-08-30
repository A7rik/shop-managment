using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Application.Services.Product;
using Domain.Models.Product;

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
}
