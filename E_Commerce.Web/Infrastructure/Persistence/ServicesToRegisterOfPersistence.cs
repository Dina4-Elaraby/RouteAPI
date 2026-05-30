using DomainLayer.RepoInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Repositiories;
using StackExchange.Redis;

namespace Persistence
{
    public static class ServicesToRegisterOfPersistence
    {
        public static IServiceCollection AddServiceOfPersistence(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<StoredDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<ICustomBasketRepo, CustomBasketRepo>();
            Services.AddSingleton<IConnectionMultiplexer>( (_) =>
            {
               return ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnectionString"));
            });
            return Services;
        }
    }
}
