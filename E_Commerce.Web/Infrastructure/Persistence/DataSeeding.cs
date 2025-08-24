using DomainLayer.Models;
using DomainLayer.RepoInterface;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

using System.Text.Json;



namespace Persistence
{
    public class DataSeeding(StoredDbContext _dbContext) : IDataSeeding
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
	
    }
}
