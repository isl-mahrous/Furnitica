using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Basket>> GetBasketById(string userId)
        {
            var basket = await basketRepository.GetBasketAsync(userId);
            if (basket == null)
                return BadRequest(new ApiResponse(400));

            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> UpdateBasket(Basket basket)
        {
            var updatedBasket = await basketRepository.UpdateBasketAsync(basket);
            
            if (updatedBasket != null)
                return Ok(updatedBasket);
            else
                return BadRequest(new ApiResponse(400));
        }

        [HttpDelete]
        public async Task DeleteBasket(int id)
        {
            await basketRepository.DeleteBasketAsync(id);
        }
    }
}
