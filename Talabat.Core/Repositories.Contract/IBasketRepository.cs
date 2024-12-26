using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositories.Contract
{
   public interface IBasketRepository
    {
        //Get
        Task<CustomerBasket?> GetBasketAsync(string id);    
        //Update
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);

        //Delete
        Task <bool>DeleteBasketAsync(string id);
    }

}
