using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core;
using Talabat.Core.Entities.identity;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Services;
using Talabat.Service;
using Talabt.API.DTOS;
using Talabt.API.Errors;

namespace Talabt.API.Controllers
{

    public class OrderController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IOrderServices _orderServices;
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IMapper mapper, IOrderServices orderServices,IUnitOfWork unitOfWork)
        {

            _mapper = mapper;
            _orderServices = orderServices;
            _unitOfWork = unitOfWork;
        }



        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(typeof(ApiResponses), 404)]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDto, Adress>(orderDto.ShippingAddress);
            var Order = await _orderServices.CreateOrderAsync(Email, orderDto.BasketId, orderDto.DeliveryMethodId,MappedAddress);
            if (Order is null)
            {
                return BadRequest(new ApiResponses(404, "There is A problem In Your Order"));
                return Ok(Order);
            }
            return Ok(Order);
        }


        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IReadOnlyList<Order>), 200)]
      [ProducesResponseType(typeof(ApiResponses) ,404)]

        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()

        {

            var Email=User.FindFirstValue(ClaimTypes.Email);    
            var Order = await _orderServices.GetOrderForSpecificUserAsync(Email);
            if (Order is null) return NotFound(new ApiResponses(404, "There Is No Orders Here")); 
            var MappedOrder = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(Order);
            return Ok(MappedOrder);   


        }
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IReadOnlyList<Order>), 200)]
        [ProducesResponseType(typeof(ApiResponses), 404)]
        public async Task<ActionResult<Order>> GetOrderByIdFOrUser(int id)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Order = await _orderServices.GetOrderByIdForSpecificUserAsync(Email,id);
            if (Order is null) return NotFound(new ApiResponses(404, $"There Is No id: {id} Orders Here"));
            var MappedOrder = _mapper.Map<Order, OrderToReturnDto>(Order);
            return Ok(MappedOrder);



        }

        #region Get All Delivery Methods
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return Ok(deliveryMethods);
        }
        #endregion


    }
}
