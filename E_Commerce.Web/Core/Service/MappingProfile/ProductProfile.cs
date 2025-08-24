using AutoMapper;
using DomainLayer.Models;
using Shared_DTOs_;


namespace Services.MappingProfile
{
   public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            //convert from productdto to product
            CreateMap<Product, ProductDTO>()
                .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.productBrand.Name))
                .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.productType.Name))
                .ForMember(dist => dist.PictureUrl, options => options.MapFrom<PictureURLResolver>());

            CreateMap<ProductBrand, GenericIdName>();
            CreateMap<ProductType, GenericIdName>();
        }
    }
}
