using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.BasketModule;
using Shared_DTOs_.CustomBasketDTOs;

namespace Services.MappingProfile
{
    public class CustomBasketProfile : Profile
    {
       public CustomBasketProfile()
        {
            CreateMap<CustomBasket,CustomBasketDTO>().ReverseMap();
            CreateMap<BasketItem,BasketItemDTO>().ReverseMap();
        }
    }
}
