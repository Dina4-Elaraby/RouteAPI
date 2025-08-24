using AutoMapper;
using DomainLayer.Models;
using Shared_DTOs_;

using Microsoft.Extensions.Configuration;

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
