using E_Commerce.Web.Extensions;
using Persistence;
using Services;
namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.
            builder.Services.AddControllers();

            builder.Services.RegisterServicesOfServices();
            builder.Services.AddServiceOfPersistence(builder.Configuration);
            builder.Services.AddSwaggerServices();
            builder.Services.AddWebApplicationnSerices();

            var app = builder.Build();

            #region How make middleware
            //app.Use(async (RequestContext, NextMiddleware) =>
            //   {
            //       Console.WriteLine("Request under processing");
            //       await NextMiddleware.Invoke();
            //       Console.WriteLine("Waiting processing");
            //       Console.WriteLine(RequestContext.Response.Body);
            //   });
            #endregion
            app.UseCustomExceptionMiddleWare();
            await app.SeedDataBaseAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapControllers();
            #endregion 
            app.Run();
        }
    }
}
