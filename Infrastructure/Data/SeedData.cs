using Core.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SeedData
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                //Seed Brands
                if (!context.ProductBrands.Any())
                {
                    var brandsData = await
                        File.ReadAllTextAsync("../Infrastructure/Data/SeedData/brands.json");

                    //var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    var brands = JsonConvert.DeserializeObject<List<ProductBrand>>(brandsData);

                    foreach (var brand in brands)
                    {
                        context.ProductBrands.Add(brand);
                    }
                    await context.SaveChangesAsync();
                }

                //Seed Types
                if (!context.ProductTypes.Any())
                {
                    var typesData = await
                        File.ReadAllTextAsync("../Infrastructure/Data/SeedData/types.json");

                    //var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    var types = JsonConvert.DeserializeObject<List<ProductType>>(typesData);

                    foreach (var type in types)
                    {
                        context.ProductTypes.Add(type);
                    }
                    await context.SaveChangesAsync();
                }

                //Seed Products
                if (!context.Products.Any())
                {
                    var productsData = await
                        File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

                    

                    //var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    var products = JsonConvert.DeserializeObject<List<Product>>(productsData);

                    foreach (var product in products)
                    {
                        context.Products.Add(product);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Reviews.Any())
                {
                    var reviewsData = await
                        File.ReadAllTextAsync("../Infrastructure/Data/SeedData/reviews.json");

                    var reviews = JsonConvert.DeserializeObject<List<Review>>(reviewsData);

                    foreach (var review in reviews)
                    {
                        context.Reviews.Add(review);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Medias.Any())
                {
                    var mediaData = await
                        File.ReadAllTextAsync("../Infrastructure/Data/SeedData/media.json");

                    var medias = JsonConvert.DeserializeObject<List<Media>>(mediaData);

                    foreach (var media in medias)
                    {
                        context.Medias.Add(media);
                    }
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<SeedData>();

                logger.LogError(ex.Message);
            }
        }

    }
}
