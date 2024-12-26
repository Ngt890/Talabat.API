using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;

using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public  class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository( StoreContext context) 
        {
            _context = context;
        }

        #region Withoutspecs
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {

            return await _context.Set<T>().ToListAsync();
        }


        public async Task<T?> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

    
        #endregion


        #region WithSpecs

     

        public async Task<T> GetAsyncWithSpec(ISpecification<T> specs)
        {
            return  await ApplySpecifications(specs).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsyncWithSpec(ISpecification<T> specs)
        {
           return await ApplySpecifications(specs).ToListAsync();
        }

       private IQueryable<T> ApplySpecifications(ISpecification<T> specs)
        {
            return  SpecificationEvaluator<T>.Getquery(_context.Set<T>(), specs);
        }
        #endregion
        public async Task<int > GetCountWithSpec(ISpecification<T> specs)
        {
            return await ApplySpecifications(specs).CountAsync();   
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);       
        }

        public void Update(T entity)
        {
            _context.Update(entity);    
        }

        public void Delete(T entity)
        {
           _context.Remove(entity); 
        }
    }
}
