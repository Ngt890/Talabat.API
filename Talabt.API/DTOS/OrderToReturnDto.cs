using Microsoft.Owin.BuilderProperties;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabt.API.DTOS
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }   
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } 
        public String Status { get; set; }
        public Adress ShippingAddress { get; set; }
        public String DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
        public decimal SubTotal { get; set; }
      public decimal Total { get; set; }
        public string PaymentIntentId { get; set; }

    }
}
