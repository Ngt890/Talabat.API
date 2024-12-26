using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.Specifications
{
   public class OrderSpecifications :BaseSpecifications<Order>
    {
        public OrderSpecifications(String email):base(O=>O.BuyerEmail==email) 
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            AddOrderbyDesc(O => O.OrderDate);

        }
        public OrderSpecifications(String email,int OrderId) : base(O => O.BuyerEmail == email&& O.Id==OrderId)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

        }
    }
}
