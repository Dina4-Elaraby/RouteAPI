using DomainLayer.Models;
using DomainLayer.RepoInterface;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositiories
{
    public class UnitOfWork(StoredDbContext _storedDbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<Entity, Key> GetRepository<Entity, Key>() where Entity : BaseEntity<Key>
        {
           
            //Get TypeName
            var typeName = typeof(Entity).Name; // like as product
            //Check if type name exist in dictionary or not
            if(_repositories.TryGetValue(typeName,out object?value))
                return (IGenericRepository<Entity,Key >) value;
            else
            {
                //create object
                var repo = new GenericRepository<Entity, Key>(_storedDbContext);
                //store object in dic
                _repositories["typeName"] = repo;
                //return object
                return repo;
            }

        }

        public async Task<int> SaveChangesAsync() => await _storedDbContext.SaveChangesAsync();


    }
}
