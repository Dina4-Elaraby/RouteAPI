using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared_DTOs_;
using Shared_DTOs_.ProductDTOs;

namespace Presentation.Controllers
{
    public class ProductController(IServiceManager _serviceManager): ApiBaseController
    {
        // four endpoints

        [HttpGet] // Get:BaseURL/api/Product/
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProduct([FromQuery]ProductQueryParams queryParams)
        {
            //type fromQuery because queryparams is complex datatype and this function without body
            //and when this function bind on complex object check from body firstly so must type from query
            var products = await _serviceManager.productService.GetAllProductsAsync(queryParams);
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
