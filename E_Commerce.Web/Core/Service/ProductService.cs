using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProductModule;
using DomainLayer.RepoInterface;
using ServiceAbstraction;
using Services.Specifications;
using Shared_DTOs_;
using Shared_DTOs_.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(queryParams);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specifications);
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
            var ProductCounts = products.Count();
            var CountSpecification = new ProductCountSpecification(queryParams);
            var TotalCount = await _unitOfWork.GetRepository<Product, int>().CountAsync(CountSpecification);
            return new PaginatedResult<ProductDTO>(queryParams.PageSize, ProductCounts, TotalCount, Data);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int Id)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(Id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specifications);
            if(product is null) throw new ProductNotFoundException(Id);
            
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
           
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var typesDTO = _mapper.Map<IEnumerable<ProductType>, IEnumerable<GenericIdName>>(types);
            return typesDTO;
        }


    }
}
