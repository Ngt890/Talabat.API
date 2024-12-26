using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.identity;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.Services
{
    public interface IOrderServices
    {

        Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Adress ShippingAddress);


     Task<IReadOnlyList<Order>> GetOrderForSpecificUserAsync(string BuyerEmail);

        Task<Order?> GetOrderByIdForSpecificUserAsync(string BuyerEmail, int OrderId);
       


    }   }
