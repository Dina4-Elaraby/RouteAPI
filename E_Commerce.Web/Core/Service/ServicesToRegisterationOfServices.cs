using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using Services.MappingProfile;

namespace Services
{
    public static class ServicesToRegisterationOfServices
    {
        public static IServiceCollection RegisterServicesOfServices(this IServiceCollection Services)
        {
            //Services.AddAutoMapper(m => m.AddProfile(new ProductProfile()));
            //Services.AddAutoMapper(typeof(PictureURLResolver).Assembly);

            Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ProductProfile>();
            }, typeof(ProductProfile).Assembly);
            Services.AddTransient<PictureURLResolver>();

            Services.AddScoped<IServiceManager, ServiceManager>();
            return Services;
        }
    }
}
