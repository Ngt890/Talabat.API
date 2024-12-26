using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        //Where
        Expression<Func<T, bool>> Criteria { set; get; }

         List<Expression<Func<T, object>>> Includes { set; get; }

       Expression<Func<T, object>> OrderbyAsc { set; get; }
     Expression<Func<T, object>> OrderbyDesc { set; get; }

         int Skip{set;get;}
          int Take { set; get; }  
        bool ISPaginationEnabled {  set; get; }  




    }
}
