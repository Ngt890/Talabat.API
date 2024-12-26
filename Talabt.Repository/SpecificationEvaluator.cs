using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
     public static class SpecificationEvaluator<T> where T : BaseEntity

    {
        // ================ Function to evaluate Query ===================================
         public static IQueryable<T> Getquery(IQueryable<T> inputquery , ISpecification<T> Specs )
         {

            // 
            var query = inputquery;            // _Dbcontext.set < t >
            if (Specs.Criteria is not null)
            {
                query = inputquery.Where(Specs.Criteria);         //_Dbcontext.set<t>.where(p => p.id == id)
            }
            if (Specs.OrderbyAsc is not null)
            {
                query=query.OrderBy(Specs.OrderbyAsc);
            }
            if (Specs.OrderbyDesc is not null)
            {
                query = query.OrderByDescending(Specs.OrderbyDesc);
            }
            if(Specs.ISPaginationEnabled)
            {
                query=query.Skip(Specs.Skip).Take(Specs.Take);  

            }
            
            query = Specs.Includes.Aggregate( query, (currentquery, includewxpression)=>currentquery.Include(includewxpression));

            return query;



     
          }



    }
}
