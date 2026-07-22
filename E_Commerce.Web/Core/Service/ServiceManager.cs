using AutoMapper;
using DomainLayer.RepoInterface;
using ServiceAbstraction;
using Microsoft.AspNetCore.Identity;
using DomainLayer.Models.Identity;
using Microsoft.Extensions.Configuration;

namespace Services
{
    // collect all services here and deal with all services by service manager 
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, ICustomBasketRepo _basketRepo, UserManager<ApplicationUser> _userManager,IConfiguration _configuration) : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService productService => _lazyProductService.Value;


        private readonly Lazy<ICustomBasketService> _lazyCustomBasketService = new Lazy<ICustomBasketService>(() => new CustomBasketService(_basketRepo, mapper));
        public ICustomBasketService CustomBasketService => _lazyCustomBasketService.Value;


        private readonly Lazy<IAuthenticationService> _lazyauthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager,_configuration,mapper));
        public IAuthenticationService authenticationService => _lazyauthenticationService.Value;

    }
}
