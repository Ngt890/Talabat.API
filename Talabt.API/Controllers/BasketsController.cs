using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabt.API.DTOS;
using Talabt.API.Errors;

namespace Talabt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : BaseApiController

    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketsController(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        [HttpGet("{basketid}")]
        public async Task<ActionResult<CustomerBasket>>GetBasket(string basketid)
        {
           
                var Basket=await _basketRepository.GetBasketAsync(basketid);
            return Basket is null ?new CustomerBasket(basketid) :(Basket);   
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto Customerbasket)
        {
            
            var mappedbasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(Customerbasket);
            var CreatedOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(mappedbasket);
            if (CreatedOrUpdatedBasket is null) return BadRequest( new ApiResponses(400));
            return Ok(CreatedOrUpdatedBasket);  

        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id);
        }




    }
}
