using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.RepoInterface
{
    public interface IGenericRepository<Entity, Key> where Entity : BaseEntity<Key>
    {
        //5 Signatures
        #region GetAll
        Task<IEnumerable<Entity>> GetAllAsync();
        #endregion

        #region GetById 
        Task<Entity?> GetByIdAsync(Key Id);
        #endregion

        #region Add
        Task AddAsync(Entity entity);
        #endregion

        #region Update
        void Update(Entity entity);
        #endregion

        #region Remove
        void Remove(Entity entity);
        #endregion
        
    }
}
