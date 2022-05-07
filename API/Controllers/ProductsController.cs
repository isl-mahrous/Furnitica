using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<Media> mediaRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IGenericRepository<ProductType> productTypeRepo;
        private readonly IMapper mapper;
        private readonly IMediaHandler mediaHandler;

      

        public ProductsController(IGenericRepository<Product> productRepo,
            IGenericRepository<Media> mediaRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper,
            IMediaHandler mediaHandler
            
            )
        {
            this.productRepo = productRepo;
            this.mediaRepo = mediaRepo;
            this.productBrandRepo = productBrandRepo;
            this.productTypeRepo = productTypeRepo;
            this.mapper = mapper;
            this.mediaHandler = mediaHandler;
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var specs = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpecs = new ProductsWithFiltersCount(productParams);
            var totalItems = await productRepo.CountAsync(countSpecs);
            var products = await productRepo.GetAllAsync(specs);
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

        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromForm] DataWithImagesUpload obj)
        {

         
            var specs = new ProductsWithTypesAndBrandsSpecification(id);


            var checkProudct = await this.productRepo.GetByIdAsync(id, specs);

            if (checkProudct == null)
            {
                return NotFound(new ApiResponse(404));
            }
            else
            {
                foreach (var media in checkProudct.Pictures)
                {

                    mediaHandler.RemoveImage(media.ImageUrl);

                    await this.mediaRepo.DeleteAsync(media.Id);

                }
               

                Product product = JsonConvert.DeserializeObject<Product>(obj.product);
                
                for (int i = 0; i < obj.files.Count; i++)
                {
                    string imgPath = mediaHandler.UploadImage(obj.files[i]);

                    Media picture = new Media() { ProductId = id, ImageUrl = imgPath };
                   
                    await this.mediaRepo.AddAsync(picture);
                }
                await this.productRepo.UpdateAsync(id, product);
                return NoContent();


            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromForm] DataWithImagesUpload obj)
        {


            Product product = JsonConvert.DeserializeObject<Product>(obj.product);
            

            await this.productRepo.AddAsync(product);

            int id=product.Id;

            for (int i = 0; i < obj.files.Count; i++)
            {
                string imgPath = mediaHandler.UploadImage(obj.files[i]);

                Media picture = new Media() {ProductId=id, ImageUrl = imgPath };

                await this.mediaRepo.AddAsync(picture);
            }

            return Ok(product);


        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {


            var specs = new ProductsWithTypesAndBrandsSpecification(id);

 
            var checkProudct= await this.productRepo.GetByIdAsync(id,specs);

            if (checkProudct == null)
            {
                return NotFound(new ApiResponse(404));
            }
            else
            {
                await this.productRepo.DeleteAsync(checkProudct.Id);
                if (checkProudct.Pictures != null)
                {
                    foreach (var media in checkProudct.Pictures)
                    {

                        mediaHandler.RemoveImage(media.ImageUrl);

                    }
                }
                return NoContent();
            }
            


        }


    }
}
