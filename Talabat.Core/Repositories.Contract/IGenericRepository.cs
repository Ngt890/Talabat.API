using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Contract
{
    public  interface IGenericRepository <T> where T : BaseEntity

    {
      Task<IReadOnlyList<T>>GetAllAsync();
        
        Task<T?> GetAsync(int id);

            Task<T> GetAsyncWithSpec(ISpecification<T> specs);
       Task<IReadOnlyList<T>> GetAllAsyncWithSpec(ISpecification<T> specs);

        Task<int>  GetCountWithSpec(ISpecification<T> specs);
        Task AddAsync(T entity);    
        void Update(T entity);
        void Delete(T entity);
    }
}
