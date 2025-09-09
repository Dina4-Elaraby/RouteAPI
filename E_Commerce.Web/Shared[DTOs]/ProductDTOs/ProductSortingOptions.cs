using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_DTOs_.ProductDTOs
{
    public enum ProductSortingOptions
    {
        NameAsc = 1,
        NameDesc = 2,
        PriceAsc = 3,
        PriceDesc = 4,
        //when i wanna add thing to make sorting based on it firstly add here and
        //assign value beacuse without make sorting show data order based on the first(nameasc)
    }
}
