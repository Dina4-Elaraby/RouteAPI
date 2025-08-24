using AutoMapper;
using DomainLayer.RepoInterface;
using Service;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    // collect all services here and deal with all services by service manager 
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper) : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService productService => _lazyProductService.Value;
    }
}
