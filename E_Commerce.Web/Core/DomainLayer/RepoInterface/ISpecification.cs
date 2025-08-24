using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.RepoInterface
{
    public interface ISpecification<TEntity,TKey> where TEntity:BaseEntity<TKey>
    {
        // -dbContext.set<entity>.where(p=>p.id == id).include(p=>p,ProductType).include(p=>p,ProductBrand)

        //i wanna store here signature of property of each expresseion of include in query

        //1. Expreseeion where
        public Expression<Func<TEntity,bool>> Criteria { get; }

        //2. Expression Include
        public List< Expression<Func<TEntity,object>>> IncludeExpressions { get; }

    }
}
