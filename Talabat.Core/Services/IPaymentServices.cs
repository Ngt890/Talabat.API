using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.Services
{
    public interface IPaymentServices
    {
        //Create Or Update Payment Intent
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(String BasketId);
        Task<Order> UpdatePaymentIntentToSucceededOrFailed(string paymentIntentId, bool isSucceeded);

    }
}
