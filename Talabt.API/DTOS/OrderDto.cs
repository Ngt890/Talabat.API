using Microsoft.Owin.BuilderProperties;
using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.identity;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabt.API.DTOS
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }
        [Required]
        public   int DeliveryMethodId { get; set; }
        [Required]
        public AddressDto ShippingAddress { get; set; } 
    }
}
