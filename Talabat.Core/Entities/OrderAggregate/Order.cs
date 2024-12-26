using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderAggregate
{
    public  class Order :BaseEntity
    {
        

        public Order()
        {
            
        }

        public Order(string buyerEmail, Adress shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal,string PaymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = PaymentIntentId;
        }

        public string BuyerEmail {  get; set; }  
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now;  
        public OrderStatus Status { get; set; }=OrderStatus.Pending;
        public Adress ShippingAddress { get; set; }
        public DeliveryMethod? DeliveryMethod{  get; set; }  //nav
        public ICollection<OrderItem> Items { get; set; } =new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Cost;
        }
        public string PaymentIntentId {  get; set; }    

    }
}
