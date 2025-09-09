using DomainLayer.Models;
using DomainLayer.RepoInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class SpecificationEvaluator
    {
        //Create Query -> _dbcontext.Products.where(p=>p.id).Include(...)
        public static IQueryable<Entity> CreateQuery<Entity,Key>(IQueryable<Entity> InputQuery,ISpecification<Entity,Key>Specifications) where Entity:BaseEntity<Key>
        {
            var Query = InputQuery;
            if(Specifications.Criteria is not null)
            {
                Query = Query.Where(Specifications.Criteria);
            }

            if(Specifications.OrderByAsc is not null)
            {
                Query = Query.OrderBy(Specifications.OrderByAsc);
            }
            if(Specifications.OrderByDescending is not null)
            {
                Query = Query.OrderByDescending(Specifications.OrderByDescending);
            }
            if(Specifications.IncludeExpressions is not null && Specifications.IncludeExpressions.Count > 0)
            {
                //foreach (var expression in Specifications.IncludeExpressions)
                //{
                //    Query = Query.Include(expression);
                //}
                Query = Specifications.IncludeExpressions.Aggregate(Query, (CurrentQuery, IncludeExpr) => CurrentQuery.Include(IncludeExpr));
            }
            if(Specifications.IsPaginated)
            {
                Query = Query.Skip(Specifications.Skip).Take(Specifications.Take);
            }
            return Query;
        }
    }
}
