using AutoMapper;
using DomainLayer.RepoInterface;
using Services;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    // collect all services here and deal with all services by service manager 
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper,ICustomBasketRepo _basketRepo ) : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService productService => _lazyProductService.Value;

        private readonly Lazy<ICustomBasketService> _lazyCustomBasketService = new Lazy<ICustomBasketService>(() => new CustomBasketService(_basketRepo, mapper));
        public ICustomBasketService CustomBasketService => _lazyCustomBasketService.Value;
    }
}
