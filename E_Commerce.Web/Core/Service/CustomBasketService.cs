using AutoMapper;
using DomainLayer.Models.BasketModule;
using DomainLayer.RepoInterface;
using ServiceAbstraction;
using Shared_DTOs_.CustomBasketDTOs;
using DomainLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CustomBasketService(ICustomBasketRepo _basketRepo, IMapper _mapper) : ICustomBasketService
    {
        public async Task<CustomBasketDTO> CreateOrUpdateCustBasketAsync(CustomBasketDTO customBasketDTO)
        {
            //making mapping first because _basketRepo deal with custombasket not dto
            var CustomBasket = _mapper.Map<CustomBasketDTO, CustomBasket>(customBasketDTO);
            var CreateOrUpdateCustBasket = _basketRepo.CreateOrUpdateBasketAsync(CustomBasket);
            if (CreateOrUpdateCustBasket is not null)
                return await GetCustBasketAsync(customBasketDTO.Id);
            else
                throw new Exception("Cannot Create or Update Basket");
        }

        public async Task<bool> DeleteCustBasketAsync(string Id)
        {
           return  await _basketRepo.DeleteBasketAsync(Id);
        }

        public async Task<CustomBasketDTO> GetCustBasketAsync(string Id)
        {
            var Basket = await _basketRepo.GetBasketAsync(Id);
            if(Basket is not null)
            
                return _mapper.Map<CustomBasket, CustomBasketDTO>(Basket);
            else
                //Throw exception basket is not found(basket is null)
                throw new BasketNotFoundException(Id);
        }
    }
}
