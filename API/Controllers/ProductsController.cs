using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController:ControllerBase
{
    private readonly StoreContext _storeContext;

    public ProductsController(StoreContext storeContext)
    {
        _storeContext = storeContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        return Ok(await _storeContext.Products.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        return Ok(await _storeContext.Products.FindAsync(id));
    }
}