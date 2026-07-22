using DomainLayer.Models.Identity;
using DomainLayer.Models.ProductModule;
using DomainLayer.RepoInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Identity;
using System.Text.Json;



namespace Persistence
{
    public class DataSeeding(StoredDbContext _dbContext,
        UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole> _roleManager,
        IdentityDbContextStored _identityDbContext
        ) : IDataSeeding
    {
        public async Task DataSeedingAsync()
        {
            //Before make data seeding must sure all migrations applied
            //Make DI for dbcontext to get Database
            try
            {
                //GetPendingMigrations() => return IEnumerable<string>
                //GetPendingMigrationsAsync() => return Task IEnumerable<string>
                var PengingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (PengingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
                //Firstly make dataseeding for entities undependent(prductbrand,producttype) 
                if (!_dbContext.ProductBrand.Any())
                {
                    //Read file
                    //var productBrandsData =await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");// return string and DeserializeAsync wanna file stream
                    var productBrandsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");// return sttreamfile now

                    //convert data from string (json) to object of c#
                    var productBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandsData);
                    if (productBrands is not null && productBrands.Any())
                    {
                        await _dbContext.ProductBrand.AddRangeAsync(productBrands);
                    }
                }

                if (!_dbContext.ProductType.Any())
                {
                    var productTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    var productTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypeData);
                    if (productTypes is not null && productTypes.Any())
                    {
                        await _dbContext.ProductType.AddRangeAsync(productTypes);
                    }
                }

                if (!_dbContext.Product.Any())
                {
                    var productData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    var product = await JsonSerializer.DeserializeAsync<List<Product>>(productData);
                    if (product is not null && product.Any())
                    {
                        await _dbContext.Product.AddRangeAsync(product);
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                ex.ToString();

            }

        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if(!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var user1 = new ApplicationUser() { Email = "dina1@gmail.com", DisplayName = "dina", PhoneNumber = "01091761620", UserName = "dina1" };
                    ApplicationUser user2 = new ApplicationUser() { Email = "ahmed1@gmail.com", DisplayName = "ahmed", PhoneNumber = "01191761620", UserName = "ahmed1" };
                    await _userManager.CreateAsync(user1, "Password@123");
                    await _userManager.CreateAsync(user2, "Password@123");

                    await _userManager.AddToRoleAsync(user1, "Admin");
                    await _userManager.AddToRoleAsync(user2, "SuperAdmin");

                }

                await _identityDbContext.SaveChangesAsync();
            }
            catch
            {

            }

        }
    }
}
