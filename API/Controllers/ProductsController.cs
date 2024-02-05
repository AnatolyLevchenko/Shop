using API.Dtos;
using Core.Entities;
using Core.Interface;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericRepository<Product> productRepository,
                                IGenericRepository<ProductBrand> productBrandRepository,
                                IGenericRepository<ProductType> productTypeRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandsSpecification();
        var products = await productRepository.ListAsync(spec);

        return Ok(products.Select(x => new ProductToReturnDto
        {
            Id = x.Id,
            ProductBrand = x.ProductBrand.Name,
            ProductType = x.ProductType.Name,
            Description = x.Description,
            Name = x.Name,
            PictureUrl = x.PictureUrl,
            Price = x.Price
        }).ToList());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await productRepository.GetEntityWithSpec(spec);
        if (product == null) return NotFound(id);

        return Ok(new ProductToReturnDto
        {
            Id = product.Id,
            ProductBrand = product.ProductBrand.Name,
            ProductType = product.ProductType.Name,
            Description = product.Description,
            Name = product.Name,
            PictureUrl = product.PictureUrl,
            Price = product.Price
        });
    }

    [HttpGet("brands")]
    public async Task<IActionResult> GetProductBrands()
    {
        return Ok(await productBrandRepository.ListAllAsync());
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetProductTypes()
    {
        return Ok(await productTypeRepository.ListAllAsync());
    }
}