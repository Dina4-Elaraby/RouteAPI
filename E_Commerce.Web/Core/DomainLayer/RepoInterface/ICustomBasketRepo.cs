using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.BasketModule;

namespace DomainLayer.RepoInterface
{
    public interface ICustomBasketRepo
    {
        //Get Basket
        Task<CustomBasket?> GetBasketAsync(string Id);
        //Create Or Update Basket
        Task<CustomBasket?> CreateOrUpdateBasketAsync(CustomBasket customBasket, TimeSpan? TimeToLive = null);
        //Delete Basket
        Task<bool> DeleteBasketAsync(string Id);
    }
}
