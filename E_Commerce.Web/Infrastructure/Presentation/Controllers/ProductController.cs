using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared_DTOs_;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //BaseURL/api/ControllerName(Product)
    public class ProductController(IServiceManager _serviceManager):ControllerBase
    {
        // four endpoints

        [HttpGet] // Get:BaseURL/api/Product/
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProduct()
        {
            var products = await _serviceManager.productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{Id}")] // Get:BaseURL/api/Product/Id
        public async Task<ActionResult<ProductDTO>> GetProductById(int Id)
        {
            var product = await _serviceManager.productService.GetProductByIdAsync(Id);
            return Ok(product);
        }

        [HttpGet("Brands")] //Get:BaseURL/api/Product/Brands
        public async Task<ActionResult<IEnumerable<GenericIdName>>>GetAllBrands()
        {
            var Brands = await _serviceManager.productService.GetAllBrandsAsync();
            return Ok(Brands);
        }

        [HttpGet("Types")] //Get:BaseURL/api/Product/Types
        public async Task<ActionResult<IEnumerable<GenericIdName>>>GetAllTypes()
        {
            var Types = await _serviceManager.productService.GetAllTypesAsync();
            return Ok(Types);
        }

    }
}
