using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using DomainLayer.RepoInterface;
using Persistence;
using Persistence.Repositiories;
using Services.MappingProfile;
using ServiceAbstraction;
using Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoredDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            #region ServicesToRegisterationOfServices
            //builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

            //builder.Services.AddAutoMapper(m => m.AddProfile(new ProductProfile()));
            //builder.Services.AddScoped<IServiceManager, ServiceManager>(); 
            #endregion
            builder.Services.RegisterServicesOfServices();
            var app = builder.Build();

            //manual DI create object of dataseeding if someone demand object of idataseeding
            using var Scoop = app.Services.CreateScope();// open and close scope
            var objectDataSeeding = Scoop.ServiceProvider.GetRequiredService<IDataSeeding>();// create object of IDataSeeding
            await objectDataSeeding.DataSeedingAsync(); // function in class dataseeding

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapControllers();
            #endregion 

            app.Run();
        }
    }
}
