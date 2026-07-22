using DomainLayer.RepoInterface;
using E_Commerce.Web.CustomMiddlewares;

namespace E_Commerce.Web
{
    public static class WebApplicationRegisteration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            //manual DI create object of dataseeding if someone demand object of idataseeding
            using var Scoop = app.Services.CreateScope();// open and close scope
            var objectDataSeeding = Scoop.ServiceProvider.GetRequiredService<IDataSeeding>();// create object of IDataSeeding
            await objectDataSeeding.DataSeedingAsync(); // function in class dataseeding
            await objectDataSeeding.IdentityDataSeedAsync();
        }

        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandleMiddleware>();
            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleWare(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
