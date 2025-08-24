using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using DomainLayer.RepoInterface;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Persistence.Repositiories
{
    public class GenericRepository<Entity,Key>(StoredDbContext _dbContext) : IGenericRepository<Entity, Key> where Entity : BaseEntity<Key>
    {
        public async Task AddAsync(Entity entity)
        {
            await _dbContext.Set<Entity>().AddAsync(entity);
        }

        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
           return  await _dbContext.Set<Entity>().ToListAsync();
        }

        public async Task<Entity?> GetByIdAsync(Key Id)
        {
           return  await _dbContext.Set<Entity>().FindAsync(Id);
        }

        public void Remove(Entity entity)
        {
            _dbContext.Set<Entity>().Remove(entity);
        }

        public void Update(Entity entity)
        {
            _dbContext.Set<Entity>().Update(entity);
        }

      
    }
}
