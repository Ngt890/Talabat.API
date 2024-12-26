using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Stripe;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Services;
using Talabat.Service;
using Talabt.API.DTOS;
using Talabt.API.Errors;

namespace Talabt.API.Controllers
{

    public class PaymentController : BaseApiController

    {
        private readonly IPaymentServices _paymentServices;
        private readonly ILogger<PaymentController> _logger;
        private const string _whSecret = "whsec_aeab7c412a6d40f7b8c791a72fb79d92bdc643c7404a88d5956ecae1d26a77e5";
        public PaymentController(IPaymentServices paymentServices, ILogger<PaymentController> logger)
        {
            _logger = logger;
        
            _paymentServices = paymentServices;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(String BasketId)
        {
            var Basket = await _paymentServices.CreateOrUpdatePaymentIntent(BasketId);
            if (Basket == null) return BadRequest(new ApiResponses(400, "There is a problem With your Basket"));
            return Ok(Basket);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], _whSecret);

            var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
            Order order;
            try
            {
                switch (stripeEvent.Type)
                {
                    case "PaymentIntentSucceeded":
                        order = await _paymentServices.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id, true);
                        _logger.LogInformation("Payment is Succeeded :)", paymentIntent.Id);
                        break;
                    case "PaymentIntentPaymentFailed":
                        order = await _paymentServices.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id, false);
                        _logger.LogInformation("Payment is Failed :(", paymentIntent.Id);
                        break;
                    default:
                        _logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error processing payment intent: {0}", ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
            return Ok();


        }
    }
}
