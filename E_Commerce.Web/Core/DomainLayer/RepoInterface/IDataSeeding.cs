using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.RepoInterface
{
   public interface IDataSeeding
    {
        Task DataSeedingAsync();
    }
}
