using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_DTOs_.CustomBasketDTOs
{
    public class CustomBasketDTO
    {
        public string Id { get; set; }
        public ICollection<BasketItemDTO> Items { get; set; } = [];
    }
}
