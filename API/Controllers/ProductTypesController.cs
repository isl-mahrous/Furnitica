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
    public class ProductTypesController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductTypesController(IGenericRepository<Product> productRepo,
                                      IGenericRepository<ProductType> productTypeRepo,
                                      IMapper mapper)
        {
            _productRepo = productRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductTypeDto>>> GetProductsTypes([FromQuery] ProductSpecParams productParams)
        {
            var specs = new ProductTypesSpecification(productParams);

            var countSpecs = new ProductTypesWithFilterCount(productParams);

            var totalItems = await _productTypeRepo.CountAsync(countSpecs);

            var productstypes = await _productTypeRepo.GetAllAsync(specs);

            var data = _mapper.Map<IReadOnlyList<ProductType>, IReadOnlyList<ProductTypeDto>>(productstypes);

            var reponse = new Pagination<ProductTypeDto>()
            {
                Count = totalItems,
                PageSize = productParams.PageSize,
                PageIndex = productParams.PageIndex,
                Data = data
            };

            return Ok(reponse);
        }
    }
}
