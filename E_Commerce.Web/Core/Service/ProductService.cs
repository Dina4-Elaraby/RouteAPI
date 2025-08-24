using AutoMapper;
using DomainLayer.Models;
using DomainLayer.RepoInterface;
using ServiceAbstraction;
using Shared_DTOs_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products =await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int Id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Id);
            return _mapper.Map<Product, ProductDTO>(product);
        }

        public async Task<IEnumerable<GenericIdName>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var brandsDTO = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<GenericIdName>>(brands);
            return brandsDTO;
        }

        public async Task<IEnumerable<GenericIdName>> GetAllTypesAsync()
        {
            var types =await  _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var typesDTO = _mapper.Map < IEnumerable<ProductType>, IEnumerable<GenericIdName>>(types);
            return typesDTO;
        }

        
    }
}
