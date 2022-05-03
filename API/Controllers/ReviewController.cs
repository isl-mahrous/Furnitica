using API.DTOs;
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
    public class ReviewController : ControllerBase
    {
        private readonly IGenericRepository<Review> reviewRepo;
        private readonly IMapper mapper;
        public ReviewController(IMapper _mapper, IGenericRepository<Review> _reviewRepo)
        {
            mapper = _mapper;
            reviewRepo = _reviewRepo;
        }

        [HttpGet]
        public async Task<IActionResult> getReviews()
        {
            var specs = new ReviewSpecification();
            var reviews = await reviewRepo.GetAllAsync(specs);
            var data = mapper.Map<IReadOnlyList<Review>, IReadOnlyList<ReviewDto>>(reviews);

            return Ok(data);
        }
    }
}
