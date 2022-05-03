using API.DTOs;
using API.Errors;
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
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IGenericRepository<ProductType> productTypeRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper)
        {
            this.productRepo = productRepo;
            this.productBrandRepo = productBrandRepo;
            this.productTypeRepo = productTypeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var specs = new ProductTypesSpecification(productParams);

            var countSpecs = new ProductsWithFiltersCount(productParams);
            var totalItems = await productRepo.CountAsync(countSpecs);

            var products = await productRepo.GetAllAsync();
            
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            var reponse = new Pagination<ProductDto>()
            {
                Count = totalItems,
                PageSize = productParams.PageSize,
                PageIndex = productParams.PageIndex,
                Data = data
            };

            return Ok(reponse);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var specs = new ProductsWithTypesAndBrandsSpecification(id);
          
            var product=  await this.productRepo.GetByIdAsync(id,specs);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }



            return Ok(mapper.Map<Product, ProductDto>(product));

        }


        [HttpPut("{id}")]

        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var checkProudct= await this.productRepo.UpdateAsync(id, product);
            if (checkProudct == null)
            {
                return NotFound(new ApiResponse(404));
            }
           
            return NoContent();

        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            await this.productRepo.AddAsync(product);
          
            return Ok(product);

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {

            var checkProudct= await this.productRepo.DeleteAsync(id);
            if (checkProudct == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return NoContent();


        }


    }
}
