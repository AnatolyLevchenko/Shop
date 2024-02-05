using Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository productRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        return Ok(await productRepository.GetProductsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        return Ok(await productRepository.GetProductByIdAsync(id));
    }

    [HttpGet("brands")]
    public async Task<IActionResult> GetProductBrands()
    {
        return Ok(await productRepository.GetProductBrandsAsync());
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetProductTypes()
    {
        return Ok(await productRepository.GetProductTypesAsync());
    }
}