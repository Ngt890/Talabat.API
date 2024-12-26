using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.Specifications
{
    public  class OrderWithPaymentSpecifications:BaseSpecifications<Order>

    {
        public OrderWithPaymentSpecifications(string PaymentIntent):base(o=>o.PaymentIntentId==PaymentIntent)
        {
            
        }
    }


}
