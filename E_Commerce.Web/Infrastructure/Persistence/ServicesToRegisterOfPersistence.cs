using DomainLayer.Models.Identity;
using DomainLayer.RepoInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Identity;
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
            Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnectionString"));
            });
            Services.AddDbContext<IdentityDbContextStored>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });
            Services.AddIdentityCore<ApplicationUser>(Options =>
            {
                Options.User.RequireUniqueEmail = true;
            })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<IdentityDbContextStored>();
            return Services;

            
        }
    }
}
