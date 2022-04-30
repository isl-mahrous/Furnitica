using API.DTOs;
using AutoMapper;
using Core.Entities;
using Infrastructue.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        

      

        [HttpGet("{id}")]

        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await Context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).
                FirstOrDefaultAsync(p => p.Id == id);

            return Ok(Mapper.Map<Product, ProductToReturnDto>(product));

        }



        [HttpPut("{id}")]

        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            var oldProduct = await Context.Products.FindAsync(id);
            if (oldProduct == null)
            {
                return NotFound();
            }

            oldProduct.Name= product.Name;
            oldProduct.Description= product.Description;
            oldProduct.Price= product.Price;
            oldProduct.UnitsSold = product.UnitsSold;
            oldProduct.UnitsInStock= product.UnitsInStock;
            oldProduct.ManufactureDate= product.ManufactureDate;
            oldProduct.ProductBrandId = product.ProductBrandId;
            oldProduct.ProductTypeId=product.ProductTypeId;

            await Context.SaveChangesAsync();

            return NoContent();

        }

            

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            Context.Products.Add(product);

            await Context.SaveChangesAsync();


            return Ok(product);

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {

            var product = await Context.Products.FindAsync(id);
            if(product == null)
            {

                return NotFound();

            }
            Context.Remove(product);
            await Context.SaveChangesAsync();
            return NoContent();


        }



    }
}
