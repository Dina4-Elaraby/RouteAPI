using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class StoredDbContext:DbContext
    {
        public StoredDbContext(DbContextOptions<StoredDbContext>options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Get Assembly from any file exist in folder which exist in it AssemblyReference and check which file exexute IEntityTypeConfiguration(StoredDbContext) and execute it 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductBrand> ProductBrand { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
    }
}
