using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class ProductUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductToReturnDto, string>
{
    public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
        {
            return configuration["ApiUrl"]! + source.PictureUrl;
        }

        return null!;
    }
}