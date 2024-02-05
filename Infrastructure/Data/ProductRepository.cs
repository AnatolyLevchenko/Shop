using Core.Entities;
using Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext storeContext) : IProductRepository
{
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await storeContext.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await storeContext.Products.
            Include(p => p.ProductType).
            Include(p => p.ProductBrand)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
        return await storeContext.ProductBrands.ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
        return await storeContext.ProductTypes.ToListAsync();
    }
}