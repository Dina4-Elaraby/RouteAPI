using AutoMapper;
using DomainLayer.Models;
using Shared_DTOs_.IdentityDTOs;

namespace Services.MappingProfile
{
    public class IdentityProfile:Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
