using Microsoft.IdentityModel.Protocols.OpenIdConnect;
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


namespace Talabat.Service
{
    public class OrderService : IOrderServices

    {
        private readonly IUnitOfWork _unitOfWork;
     
        private readonly IBasketRepository _basketRepository;
        private readonly IPaymentServices _paymentServices;

        public OrderService(IUnitOfWork UnitOfWork
            ,IBasketRepository basketRepository
            ,IPaymentServices paymentServices)
           
            
        {
            _unitOfWork = UnitOfWork;
           _basketRepository = basketRepository;
           _paymentServices = paymentServices;
        }

        public async Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Adress ShippingAddress)
        {
            //Get Selected Items in basket
            var Basket = await _basketRepository.GetBasketAsync(BasketId);


            
            var  OrderItems = new List<OrderItem>();
            if (Basket?.Items.Count >= 0)
            {
                foreach (var item in Basket.Items)
                {
                    var Product= await  _unitOfWork.Repository<Product>().GetAsync(item.Id);
                    var ProductItemOrdered= new ProductItemOrdered(Product.Id, Product.Name,Product.PictureUrl);
                   var OrderItem = new OrderItem(ProductItemOrdered, Product.Price,item.Quantity);

                    OrderItems.Add(OrderItem);  
                }
            }
         //Create DeliveryMethod
            var DeliveryMethod= await _unitOfWork.Repository<DeliveryMethod>().GetAsync(DeliveryMethodId);
            // Get SubTotalPrice
           var SubTotal=OrderItems.Sum(items=>items.Price *items.Quantity);
            //Check for having paymentintentid 
            var spec = new OrderWithPaymentSpecifications(Basket.PaymentIntentId);
            var ExOrder = await _unitOfWork.Repository<Order>().GetAsyncWithSpec(spec);
            if(ExOrder is not null)
            {

                _unitOfWork.Repository<Order>().Delete(ExOrder);
                await _paymentServices.CreateOrUpdatePaymentIntent(BasketId);
            }
                

            // Create Order 
            var Order = new Order(BuyerEmail, ShippingAddress,DeliveryMethod, OrderItems, SubTotal,Basket.PaymentIntentId);
              //Add Order Locally

            await  _unitOfWork.Repository<Order>().AddAsync(Order);
            //Save order in DB Using SaveChanges 
            var result = await _unitOfWork.CompleteAsync();
            if (result <=0) return null;
            return Order;   
            
            



        }

        public async Task<Order?> GetOrderByIdForSpecificUserAsync(string BuyerEmail, int OrderId)
        {
            var spec = new OrderSpecifications(BuyerEmail,OrderId);
            var Order = await _unitOfWork.Repository<Order>().GetAsyncWithSpec(spec);
            return Order;
           
        }

        public async Task<IReadOnlyList<Order>> GetOrderForSpecificUserAsync(string BuyerEmail)
        {
            var spec = new OrderSpecifications(BuyerEmail);
            var Order = await _unitOfWork.Repository<Order>().GetAllAsyncWithSpec(spec);  
            return Order;
        }


       
    }
}
