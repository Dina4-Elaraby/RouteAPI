using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.RepoInterface
{
    public interface ISpecification<Entity,Key> where Entity:BaseEntity<Key>
    {
        // -dbContext.set<entity>.where(p=>p.id == id).include(p=>p,ProductType).include(p=>p,ProductBrand)

        //i wanna store here signature of property of each expresseion of include in query

        //1. Expreseeion where
        public Expression<Func<Entity,bool>>? Criteria { get; }

        //2. Expression Include
        public List< Expression<Func<Entity,object>>> IncludeExpressions { get; }

        //4.orderby(p=>p.Name)
        Expression<Func<Entity, object>> OrderByDescending { get; }
        Expression<Func<Entity, object>> OrderByAsc { get; }

        //5.pagination skip() take()
        public int Skip { get;}
        public int Take { get; }
        public bool IsPaginated { get; set; }

    }
}
