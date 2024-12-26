using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data.Config;
using Talabat.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Talabat.Repository
{
     public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbcontext;
        private  Hashtable _repo;
      

        public UnitOfWork(StoreContext dbcontext )
        {
            _dbcontext = dbcontext;
            _repo = new Hashtable();    
        }
        public async Task<int> CompleteAsync()
     
          =>  await _dbcontext.SaveChangesAsync();
       

      public   IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            //typeof -->namespace
            var type = typeof(T).Name;
            if (!_repo.ContainsKey(type)) //at first time 
            {
                var Repository = new GenericRepository<T>(_dbcontext);
                _repo.Add(type, Repository);
            }
            return _repo[type] as GenericRepository<T>;

        }

        public async ValueTask DisposeAsync()
           => await _dbcontext.DisposeAsync();
    }
}
