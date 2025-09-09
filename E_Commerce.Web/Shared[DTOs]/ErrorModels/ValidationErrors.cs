using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_DTOs_.ErrorModels
{
    public class ValidationErrors
    {
        public string Field { get; set; }
        public IEnumerable<string> Errors { get; set; } = [];
    }
}
