using Shared_DTOs_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        //get all products return ienumerable<productdto>
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();

        //Get productById return productdto based on id
        Task<ProductDTO> GetProductByIdAsync(int Id);

        //Get all Brands return ienumerable<branddto> of id,name
        Task<IEnumerable<GenericIdName>> GetAllBrandsAsync();

        //Get all Types return ienumerable<typedto> of id,name
        Task<IEnumerable<GenericIdName>> GetAllTypesAsync();
    }
}
