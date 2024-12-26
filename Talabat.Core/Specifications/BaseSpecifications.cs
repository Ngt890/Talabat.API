using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderbyAsc { get; set; }
        public Expression<Func<T, object>> OrderbyDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool ISPaginationEnabled { get; set; }


        //GetAllProduct
        public BaseSpecifications()
        {


        }
        //GetById 
        public BaseSpecifications(Expression<Func<T, bool>> criteriaexpression)
        {
            Criteria = criteriaexpression;

        }
        public void AddOrderbyAsc(Expression<Func<T, object>> orderbyasc)
        {
            OrderbyAsc = orderbyasc;
        }

        public void AddOrderbyDesc(Expression<Func<T, object>> orderbydes)
        {
            OrderbyDesc = orderbydes;
        }

        public void ApplyPagination(int skip, int take)
        {
            ISPaginationEnabled = true;
            Skip = skip;
            Take = take;


        }
      
        
        
     
  

    }

}
