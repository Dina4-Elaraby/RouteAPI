using DomainLayer.Models.ProductModule;
using Shared_DTOs_.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product, int>
    {
        //GetAll products with brands and types

        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams)
            : base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId) 
            && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId) 
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            //where(p=>p.BrandId = BrandId && p.TypeId = TypeId
            //we make !BrandId.HasValue because if user filter on brand only it is will be true
            AddInclude(p => p.productBrand);
            AddInclude(p => p.productType);

            switch (queryParams.sortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderByAsc(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDes(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderByAsc(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDes(p => p.Price);
                    break;
                default:
                    break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }
        //get by id 
        public ProductWithBrandAndTypeSpecifications(int Id) : base(p => p.Id == Id)
        {
            AddInclude(p => p.productBrand);
            AddInclude(p => p.productType);

        }

    }
}
