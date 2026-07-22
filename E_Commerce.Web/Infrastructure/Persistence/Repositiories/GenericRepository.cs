using DomainLayer.Models;
using DomainLayer.RepoInterface;
using Microsoft.EntityFrameworkCore;
using Persistence.Identity;


namespace Persistence.Repositiories
{
    public class GenericRepository<Entity,Key>(StoredDbContext _dbContext) : IGenericRepository<Entity, Key> where Entity : BaseEntity<Key>
    {
        public async Task AddAsync(Entity entity)
        {
            await _dbContext.Set<Entity>().AddAsync(entity);
        }

        public async Task<int> CountAsync(ISpecification<Entity, Key> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(InputQuery: _dbContext.Set<Entity>(), specifications).CountAsync();
        }

        public async Task<IEnumerable<Entity>> GetAllAsync(ISpecification<Entity, Key> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(InputQuery: _dbContext.Set<Entity>(), specifications).ToListAsync();
        }

        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
           return await _dbContext.Set<Entity>().ToListAsync();
        }

        public async Task<Entity?> GetByIdAsync(ISpecification<Entity, Key> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(InputQuery: _dbContext.Set<Entity>(), specifications).FirstOrDefaultAsync();
        }

        public async Task<Entity?> GetByIdAsync(Key Id)
        {
            return await _dbContext.Set<Entity>().FindAsync(Id);
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
