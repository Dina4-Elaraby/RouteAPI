using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared_DTOs_.CustomBasketDTOs;

namespace Presentation.Controllers
{
    
    public class BasketController(IServiceManager _serviceManager): ApiBaseController
    {
        //Get Basket
        [HttpGet] //BaseUrl/api/Basket
        public async Task<ActionResult<CustomBasketDTO>>GetBasket(string Id)
        {
            var Basket = await _serviceManager.CustomBasketService.GetCustBasketAsync(Id);
            return Ok(Basket);

        }

        //Create OR Update
        [HttpPost]
        public async Task<ActionResult<CustomBasketDTO>> CreateOrUpdateBasket(CustomBasketDTO basketDTO)
        {
            var basket = await _serviceManager.CustomBasketService.CreateOrUpdateCustBasketAsync(basketDTO);
            return Ok(basket);
        }

        //Delete Basket
        [HttpDelete("{Id}")]
        public async Task<ActionResult<bool>>DeleteBasket(string Id)
        {
            var result = await _serviceManager.CustomBasketService.DeleteCustBasketAsync(Id);
            return Ok(result);
        }

    }
}
