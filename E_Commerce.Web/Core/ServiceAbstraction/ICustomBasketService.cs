using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared_DTOs_.CustomBasketDTOs;

namespace ServiceAbstraction
{
    public interface ICustomBasketService
    {
        //signatures for 3 methods, deal with dto not model

        //Get Custumor Basket
        Task<CustomBasketDTO> GetCustBasketAsync(string Id);

        //Create Or update Custom Basket
        Task<CustomBasketDTO> CreateOrUpdateCustBasketAsync(CustomBasketDTO customBasket);

        //Delete
        Task<bool> DeleteCustBasketAsync(string Id);

    }
}
