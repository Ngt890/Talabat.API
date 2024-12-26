using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services;
using Talabat.Core.Specifications;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Service
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository 
            _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentServices(IConfiguration configuration,IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId)
        {
            //Configuration For Secret Key
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];
            //GetBasket
            var Basket = await _basketRepository.GetBasketAsync(BasketId);
            if (Basket is null) return null; 
         
            var ShippingPrice = 0M;
            //Amount =SubTotal+DM.cost
            if (Basket.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(Basket.DeliveryMethodId.Value);
                ShippingPrice = DeliveryMethod.Cost;
            }
            //Check Equality OF Price

            if (    Basket.Items.Count() > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var Product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                    if (Product.Price != item.Price)

                        item.Price = Product.Price;
                }    
             
            }
             var SubTortal = Basket.Items.Sum(items => items.Price * items.Quantity);
            var Services = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(Basket.PaymentIntentId))
            {
                //create
                var options = new PaymentIntentCreateOptions
                {
                    //Amount
                    Amount = (long)SubTortal * 100 + (long)ShippingPrice * 100,
                    //Currency
                    Currency = "usd",
                    //list of payment methd
                    PaymentMethodTypes = new List<string>() { "card" }

                };

                paymentIntent = await Services.CreateAsync(options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;


            }
            else
            {
                //update
                var UpdatedOptions = new PaymentIntentUpdateOptions()
                { Amount = (long)SubTortal * (long)ShippingPrice * 100 };

                paymentIntent = await Services.UpdateAsync(Basket.PaymentIntentId, UpdatedOptions);

                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }

            //Save @Reduis
           await _basketRepository.UpdateBasketAsync(Basket);    
            return Basket;






        }

        public async Task<Order> UpdatePaymentIntentToSucceededOrFailed(string paymentIntentId, bool isSucceeded)
        {
            var spec = new OrderWithPaymentSpecifications(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetAsyncWithSpec(spec);
            if (isSucceeded)
                order.Status = OrderStatus.PaymentReceived;
            else
                order.Status = OrderStatus.PaymentFailed;

           _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.CompleteAsync();
            return order;


        }
    }
}
