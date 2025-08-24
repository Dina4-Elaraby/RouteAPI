using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public ProductBrand productBrand { get; set; } //navigation property
        public int BrandId { get; set; } //FK
        public ProductType productType { get; set; } //navigation property
        public int TypeId { get; set; } //FK
    }
}
