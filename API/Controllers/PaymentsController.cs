using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        
        [HttpPost("{userId}")]
        public async Task<ActionResult<Basket>> CreateOrUpdatePaymentIntent(string userId)
        {
            return await paymentService.CreateOrUpdatePaymentIntent(userId);
        }
    }
}
