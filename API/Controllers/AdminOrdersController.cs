using API.DTOs;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminOrdersController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        string email;
        public AdminOrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Pagination<OrderToReturnDto>>>> GetAllOrders([FromQuery] OrderSpecParams orderParams)
        {
            var specs = new OrdersSpecification(orderParams);

            var totalItems = await _orderService.CountAsync(specs);

            var orders = await _orderService.GetAllOrdersAsync(specs);

            var data = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders);

            var response = new Pagination<OrderToReturnDto>
            {
                Count = totalItems,
                PageSize = orderParams.PageSize,
                PageIndex = orderParams.PageIndex,
                Data = data
            };

            return Ok(response);

        }

        [HttpGet("maxPrice")]
        public async Task<ActionResult<decimal>> GetMaxPrice()
        {
            var specs = new OrdersSpecification(new OrderSpecParams());
            var result = await _orderService.GetAllOrdersAsync(specs);
            var maxPrice = result.Max(p => p.Subtotal);

            return Ok(maxPrice);
        }
    }
}
