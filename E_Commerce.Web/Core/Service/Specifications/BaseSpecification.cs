using DomainLayer.RepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using DomainLayer.Models;

namespace Services.Specifications
{
    public abstract class BaseSpecification<Entity, Key> : ISpecification<Entity, Key> where Entity : BaseEntity<Key>
    {
        protected BaseSpecification(Expression<Func<Entity, bool>>? CriteriaExpresseion)
        {
            Criteria = CriteriaExpresseion;
        }
        public Expression<Func<Entity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<Entity, object>>> IncludeExpressions { get; } = [];

        protected void AddInclude(Expression<Func<Entity, object>> IncludeExpression)
        {
            IncludeExpressions.Add(IncludeExpression);
        }

        public Expression<Func<Entity, object>> OrderByDescending { get; private set; }

        public Expression<Func<Entity, object>> OrderByAsc { get; private set; }
       
        protected void AddOrderByAsc(Expression<Func<Entity, object>> OrderByAscExpression)
        {
            OrderByAsc = OrderByAscExpression;
        }
        protected void AddOrderByDes(Expression<Func<Entity, object>> OrderByDesExpression)
        {
            OrderByDescending = OrderByDesExpression;
        }

        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool IsPaginated { get; set; } // default = false

        protected void ApplyPagination(int PageSize,int PageIndex)
        {
            // when use this function is paginted = true
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize ;
        }
    }

}
