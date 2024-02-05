using Core.Entities;
using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context, ILoggerFactory logeFactory)
    {
        var path = "../Infrastructure/Data/SeedData/";
        try
        {
            SeedEntities<ProductBrand>(context, $"{path}brands.json");
            SeedEntities<ProductType>(context, $"{path}types.json");
            SeedEntities<Product>(context, $"{path}products.json");

            if (context.ChangeTracker.HasChanges())
                await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            var logger = logeFactory.CreateLogger<StoreContextSeed>();
            logger.LogError("Seed error:{Message}",e.Message);

        }
    }

    private static void SeedEntities<T>(DbContext context, string filePath) where T : class
    {
        if (context.Set<T>().Any()) return;
        var jsonData = File.ReadAllText(filePath);
        var entities = JsonSerializer.Deserialize<List<T>>(jsonData);
        context.Set<T>().AddRange(entities!);
    }

}