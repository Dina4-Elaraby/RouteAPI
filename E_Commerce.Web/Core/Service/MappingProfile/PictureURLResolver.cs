using AutoMapper;

using Microsoft.Extensions.Configuration;
using DomainLayer.Models.ProductModule;
using Shared_DTOs_.ProductDTOs;

namespace Services.MappingProfile
{
    public class PictureURLResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
            else
            {
                var url = $"{_configuration.GetSection("URLS")["BaseURL"]}{source.PictureUrl}";
                return url;

            }
        }
       
    }
}
