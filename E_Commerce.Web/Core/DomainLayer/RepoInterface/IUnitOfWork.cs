using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.RepoInterface
{
    public interface IUnitOfWork
    {
        public IGenericRepository<Entity, Key> GetRepository<Entity, Key>() where Entity : BaseEntity<Key>;
        Task<int> SaveChangesAsync();
    }
}
