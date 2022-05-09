using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.AspNetCore.Identity;
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
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory,
            UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
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

                // Seed Reviews

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

                // Seed Roles
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                if (!await roleManager.RoleExistsAsync("Customer"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Customer"));
                }
                var adminEmail = "admin@furnitica.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if(adminUser == null)
                {
                    var newAdmin = new AppUser()
                    {
                        Email = adminEmail,
                        UserName = "Thanos",
                        ProfilePicture = "https://i.ytimg.com/vi/N2YTmooNR8E/maxresdefault.jpg"
                    };
                    var result = await userManager.CreateAsync(newAdmin, "Admin@1234");
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }

                var customerEmail = "customer@furnitica.com";
                var customerUser = await userManager.FindByEmailAsync(customerEmail);
                if (customerUser == null)
                {
                    var newCustomer = new AppUser()
                    {
                        Email = customerEmail,
                        UserName = "Wanda",
                        ProfilePicture = "https://sm.ign.com/ign_me/news/w/wandavisio/wandavision-director-says-theres-a-lot-more-of-scarlet-witch_wyke.jpg"
                    };
                    await userManager.CreateAsync(newCustomer, "Customer@1234");
                    await userManager.AddToRoleAsync(newCustomer, "Customer");
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
