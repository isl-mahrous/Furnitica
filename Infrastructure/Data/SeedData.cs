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

                //Seed Delivery Methods
                if (!context.DeliveryMethods.Any())
                {
                    var DeliveryMethodsData = await
                        File.ReadAllTextAsync("../Infrastructure/Data/SeedData/delivery.json");

                    //var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    var deliveyMethods = JsonConvert.DeserializeObject<List<DeliveryMethod>>(DeliveryMethodsData);

                    foreach (var method in deliveyMethods)
                    {
                        context.DeliveryMethods.Add(method);
                    }
                    await context.SaveChangesAsync();
                }

                //Seed Orders
                if (!context.Orders.Any())
                {
                    /*
                        public string BuyerEmail { get; set; }
                        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
                        public Address ShipToAddress { get; set; }
                        public DeliveryMethod DeliveryMethod { get; set; }
                        public IReadOnlyList<OrderItem> OrderItems { get; set; }
                        public decimal Subtotal { get; set; }
                        public OrderStatus Status { get; set; } = OrderStatus.Pending;
                        public string PaymentIntentId { get; set; }
                     */
                    /*
                        FirstName = firstName;
                        LastName = lastName;
                        Street = street;
                        City = city;
                        State = state;
                        Zipcode = zipcode;
                     */

                    /*
                        public string ShortName { get; set; }
                        public string DeliveryTime { get; set; }
                        public string Description { get; set; }
                        public decimal Price { get; set; }
                     */
                    /* (Order Item)
                        public ProductItemOrdered ItemOrdered { get; set; }
                        public decimal Price { get; set; }
                        public int Quantity { get; set; }
                     */
                    /*
                        public int ProductItemId { get; set; }
                        public string ProductName { get; set; }
                     */

                    var ordersData = new List<Order>()
                    {
                        new Order()
                        {
                            BuyerEmail = "Cusomter@1234",
                            OrderDate = DateTime.Now,
                            ShipToAddress = new Address()
                            {
                                FirstName = "Hiso",
                                LastName = "Hoso",
                                Street = "street1",
                                City = "cairo",
                                State = "NY",
                                Zipcode = "1234"
                            },
                            DeliveryMethod = new DeliveryMethod()
                            {
                                ShortName = "by Car",
                                DeliveryTime = "one day",
                                Description = "هتوصل إن شاء الله",
                                Price = 2434
                            },
                            OrderItems = new List<OrderItem>()
                            {
                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 3,
                                        ProductName = "Chair"
                                    },
                                    Price = 100,
                                    Quantity = 4,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 1,
                                        ProductName = "Sofa"
                                    },
                                    Price = 40,
                                    Quantity = 3,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 7,
                                        ProductName = "table"
                                    },
                                    Price = 200,
                                    Quantity = 1,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 5,
                                        ProductName = "molla"
                                    },
                                    Price = 900,
                                    Quantity = 8,
                                },
                            },
                            Subtotal = 7920,
                            Status = OrderStatus.Pending,
                            PaymentIntentId = "53454nskjdfb"
                        },

                        new Order()
                        {
                            BuyerEmail = "Cusomter@1234",
                            OrderDate = DateTime.Now,
                            ShipToAddress = new Address()
                            {
                                FirstName = "Hiso",
                                LastName = "Hoso",
                                Street = "street1",
                                City = "cairo",
                                State = "NY",
                                Zipcode = "1234"
                            },
                            DeliveryMethod = new DeliveryMethod()
                            {
                                ShortName = "by Car",
                                DeliveryTime = "one day",
                                Description = "هتوصل إن شاء الله",
                                Price = 2434
                            },
                            OrderItems = new List<OrderItem>()
                            {
                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 3,
                                        ProductName = "Chair"
                                    },
                                    Price = 34,
                                    Quantity =5,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 1,
                                        ProductName = "Sofa"
                                    },
                                    Price = 67,
                                    Quantity = 7,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 7,
                                        ProductName = "table"
                                    },
                                    Price = 76,
                                    Quantity = 3,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 5,
                                        ProductName = "molla"
                                    },
                                    Price = 900,
                                    Quantity = 8,
                                },
                            },
                            Subtotal = 7920,
                            Status = OrderStatus.Pending,
                            PaymentIntentId = "53454nskjdfb"
                        },

                        new Order()
                        {
                            BuyerEmail = "Cusomter@1234",
                            OrderDate = DateTime.Now,
                            ShipToAddress = new Address()
                            {
                                FirstName = "Hiso",
                                LastName = "Hoso",
                                Street = "street1",
                                City = "cairo",
                                State = "NY",
                                Zipcode = "1234"
                            },
                            DeliveryMethod = new DeliveryMethod()
                            {
                                ShortName = "by Car",
                                DeliveryTime = "one day",
                                Description = "هتوصل إن شاء الله",
                                Price = 2434
                            },
                            OrderItems = new List<OrderItem>()
                            {
                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 3,
                                        ProductName = "Chair"
                                    },
                                    Price = 100,
                                    Quantity = 4,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 1,
                                        ProductName = "Sofa"
                                    },
                                    Price = 40,
                                    Quantity = 3,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 7,
                                        ProductName = "table"
                                    },
                                    Price = 200,
                                    Quantity = 1,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 5,
                                        ProductName = "molla"
                                    },
                                    Price = 900,
                                    Quantity = 8,
                                },
                            },
                            Subtotal = 7920,
                            Status = OrderStatus.Pending,
                            PaymentIntentId = "53454nskjdfb"
                        },

                        new Order()
                        {
                            BuyerEmail = "Cusomter@1234",
                            OrderDate = DateTime.Now,
                            ShipToAddress = new Address()
                            {
                                FirstName = "Hiso",
                                LastName = "Hoso",
                                Street = "street1",
                                City = "cairo",
                                State = "NY",
                                Zipcode = "1234"
                            },
                            DeliveryMethod = new DeliveryMethod()
                            {
                                ShortName = "by Car",
                                DeliveryTime = "one day",
                                Description = "هتوصل إن شاء الله",
                                Price = 2434
                            },
                            OrderItems = new List<OrderItem>()
                            {
                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 3,
                                        ProductName = "Chair"
                                    },
                                    Price = 34,
                                    Quantity = 6,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 1,
                                        ProductName = "Sofa"
                                    },
                                    Price = 677,
                                    Quantity = 6,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 7,
                                        ProductName = "table"
                                    },
                                    Price = 200,
                                    Quantity = 1,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 5,
                                        ProductName = "molla"
                                    },
                                    Price = 122,
                                    Quantity = 3,
                                },
                            },
                            Subtotal = 7920,
                            Status = OrderStatus.Pending,
                            PaymentIntentId = "53454nskjdfb"
                        },

                        new Order()
                        {
                            BuyerEmail = "Cusomter@1234",
                            OrderDate = DateTime.Now,
                            ShipToAddress = new Address()
                            {
                                FirstName = "Hiso",
                                LastName = "Hoso",
                                Street = "street1",
                                City = "cairo",
                                State = "NY",
                                Zipcode = "1234"
                            },
                            DeliveryMethod = new DeliveryMethod()
                            {
                                ShortName = "by Car",
                                DeliveryTime = "one day",
                                Description = "هتوصل إن شاء الله",
                                Price = 2434
                            },
                            OrderItems = new List<OrderItem>()
                            {
                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 3,
                                        ProductName = "Chair"
                                    },
                                    Price = 100,
                                    Quantity = 4,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 1,
                                        ProductName = "Sofa"
                                    },
                                    Price = 40,
                                    Quantity = 3,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 7,
                                        ProductName = "table"
                                    },
                                    Price = 200,
                                    Quantity = 1,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 5,
                                        ProductName = "molla"
                                    },
                                    Price = 66,
                                    Quantity = 2,
                                },
                            },
                            Subtotal = 7920,
                            Status = OrderStatus.Pending,
                            PaymentIntentId = "53454nskjdfb"
                        },

                        new Order()
                        {
                            BuyerEmail = "Cusomter@1234",
                            OrderDate = DateTime.Now,
                            ShipToAddress = new Address()
                            {
                                FirstName = "Hiso",
                                LastName = "Hoso",
                                Street = "street1",
                                City = "cairo",
                                State = "NY",
                                Zipcode = "1234"
                            },
                            DeliveryMethod = new DeliveryMethod()
                            {
                                ShortName = "by Car",
                                DeliveryTime = "one day",
                                Description = "هتوصل إن شاء الله",
                                Price = 2434
                            },
                            OrderItems = new List<OrderItem>()
                            {
                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 3,
                                        ProductName = "Chair"
                                    },
                                    Price = 100,
                                    Quantity = 12,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 1,
                                        ProductName = "Sofa"
                                    },
                                    Price = 40,
                                    Quantity = 7,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 7,
                                        ProductName = "table"
                                    },
                                    Price = 200,
                                    Quantity = 1,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 5,
                                        ProductName = "molla"
                                    },
                                    Price = 900,
                                    Quantity = 5,
                                },
                            },
                            Subtotal = 7920,
                            Status = OrderStatus.Pending,
                            PaymentIntentId = "53454nskjdfb"
                        },

                        new Order()
                        {
                            BuyerEmail = "Cusomter@1234",
                            OrderDate = DateTime.Now,
                            ShipToAddress = new Address()
                            {
                                FirstName = "Hiso",
                                LastName = "Hoso",
                                Street = "street1",
                                City = "cairo",
                                State = "NY",
                                Zipcode = "1234"
                            },
                            DeliveryMethod = new DeliveryMethod()
                            {
                                ShortName = "by Car",
                                DeliveryTime = "one day",
                                Description = "هتوصل إن شاء الله",
                                Price = 2434
                            },
                            OrderItems = new List<OrderItem>()
                            {
                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 3,
                                        ProductName = "Chair"
                                    },
                                    Price = 100,
                                    Quantity = 7,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 1,
                                        ProductName = "Sofa"
                                    },
                                    Price = 40,
                                    Quantity = 3,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 7,
                                        ProductName = "table"
                                    },
                                    Price = 200,
                                    Quantity = 1,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 5,
                                        ProductName = "molla"
                                    },
                                    Price = 900,
                                    Quantity = 10,
                                },
                            },
                            Subtotal = 7920,
                            Status = OrderStatus.Pending,
                            PaymentIntentId = "53454nskjdfb"
                        },

                        new Order()
                        {
                            BuyerEmail = "Cusomter@1234",
                            OrderDate = DateTime.Now,
                            ShipToAddress = new Address()
                            {
                                FirstName = "Hiso",
                                LastName = "Hoso",
                                Street = "street1",
                                City = "cairo",
                                State = "NY",
                                Zipcode = "1234"
                            },
                            DeliveryMethod = new DeliveryMethod()
                            {
                                ShortName = "by Car",
                                DeliveryTime = "one day",
                                Description = "هتوصل إن شاء الله",
                                Price = 2434
                            },
                            OrderItems = new List<OrderItem>()
                            {
                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 3,
                                        ProductName = "Chair"
                                    },
                                    Price = 100,
                                    Quantity = 14,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 1,
                                        ProductName = "Sofa"
                                    },
                                    Price = 40,
                                    Quantity = 3,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 7,
                                        ProductName = "table"
                                    },
                                    Price = 200,
                                    Quantity = 1,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 5,
                                        ProductName = "molla"
                                    },
                                    Price = 900,
                                    Quantity = 16,
                                },
                            },
                            Subtotal = 7920,
                            Status = OrderStatus.PaymentRecieved,
                            PaymentIntentId = "53454nskjdfb"
                        },

                        new Order()
                        {
                            BuyerEmail = "Cusomter@1234",
                            OrderDate = DateTime.Now,
                            ShipToAddress = new Address()
                            {
                                FirstName = "Hiso",
                                LastName = "Hoso",
                                Street = "street1",
                                City = "cairo",
                                State = "NY",
                                Zipcode = "1234"
                            },
                            DeliveryMethod = new DeliveryMethod()
                            {
                                ShortName = "by Car",
                                DeliveryTime = "one day",
                                Description = "هتوصل إن شاء الله",
                                Price = 2434
                            },
                            OrderItems = new List<OrderItem>()
                            {
                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 3,
                                        ProductName = "Chair"
                                    },
                                    Price = 100,
                                    Quantity = 4,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 1,
                                        ProductName = "Sofa"
                                    },
                                    Price = 40,
                                    Quantity = 3,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 7,
                                        ProductName = "table"
                                    },
                                    Price = 200,
                                    Quantity = 1,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 5,
                                        ProductName = "molla"
                                    },
                                    Price = 900,
                                    Quantity = 8,
                                },
                            },
                            Subtotal = 7920,
                            Status = OrderStatus.PaymentRecieved,
                            PaymentIntentId = "53454nskjdfb"
                        },

                        new Order()
                        {
                            BuyerEmail = "Cusomter@1234",
                            OrderDate = DateTime.Now,
                            ShipToAddress = new Address()
                            {
                                FirstName = "Hiso",
                                LastName = "Hoso",
                                Street = "street1",
                                City = "cairo",
                                State = "NY",
                                Zipcode = "1234"
                            },
                            DeliveryMethod = new DeliveryMethod()
                            {
                                ShortName = "by Car",
                                DeliveryTime = "one day",
                                Description = "هتوصل إن شاء الله",
                                Price = 2434
                            },
                            OrderItems = new List<OrderItem>()
                            {
                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 3,
                                        ProductName = "Chair"
                                    },
                                    Price = 100,
                                    Quantity = 3,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 1,
                                        ProductName = "Sofa"
                                    },
                                    Price = 40,
                                    Quantity = 3,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 7,
                                        ProductName = "table"
                                    },
                                    Price = 200,
                                    Quantity = 1,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 5,
                                        ProductName = "molla"
                                    },
                                    Price = 900,
                                    Quantity = 2,
                                },
                            },
                            Subtotal = 7920,
                            Status = OrderStatus.PaymentFailed,
                            PaymentIntentId = "53454nskjdfb"
                        },

                        new Order()
                        {
                            BuyerEmail = "Cusomter@1234",
                            OrderDate = DateTime.Now,
                            ShipToAddress = new Address()
                            {
                                FirstName = "Hiso",
                                LastName = "Hoso",
                                Street = "street1",
                                City = "cairo",
                                State = "NY",
                                Zipcode = "1234"
                            },
                            DeliveryMethod = new DeliveryMethod()
                            {
                                ShortName = "by Car",
                                DeliveryTime = "one day",
                                Description = "هتوصل إن شاء الله",
                                Price = 2434
                            },
                            OrderItems = new List<OrderItem>()
                            {
                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 3,
                                        ProductName = "Chair"
                                    },
                                    Price = 100,
                                    Quantity = 1,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 1,
                                        ProductName = "Sofa"
                                    },
                                    Price = 40,
                                    Quantity = 1,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 7,
                                        ProductName = "table"
                                    },
                                    Price = 200,
                                    Quantity = 1,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 5,
                                        ProductName = "molla"
                                    },
                                    Price = 900,
                                    Quantity = 1,
                                },
                            },
                            Subtotal = 7920,
                            Status = OrderStatus.PaymentRecieved,
                            PaymentIntentId = "53454nskjdfb"
                        },

                        new Order()
                        {
                            BuyerEmail = "Cusomter@1234",
                            OrderDate = DateTime.Now,
                            ShipToAddress = new Address()
                            {
                                FirstName = "Hiso",
                                LastName = "Hoso",
                                Street = "street1",
                                City = "cairo",
                                State = "NY",
                                Zipcode = "1234"
                            },
                            DeliveryMethod = new DeliveryMethod()
                            {
                                ShortName = "by Car",
                                DeliveryTime = "one day",
                                Description = "هتوصل إن شاء الله",
                                Price = 2434
                            },
                            OrderItems = new List<OrderItem>()
                            {
                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 3,
                                        ProductName = "Chair"
                                    },
                                    Price = 100,
                                    Quantity = 4,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 1,
                                        ProductName = "Sofa"
                                    },
                                    Price = 40,
                                    Quantity = 3,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 7,
                                        ProductName = "table"
                                    },
                                    Price = 200,
                                    Quantity = 1,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 5,
                                        ProductName = "molla"
                                    },
                                    Price = 900,
                                    Quantity = 8,
                                },
                            },
                            Subtotal = 7920,
                            Status = OrderStatus.PaymentRecieved,
                            PaymentIntentId = "53454nskjdfb"
                        },

                        new Order()
                        {
                            BuyerEmail = "Cusomter@1234",
                            OrderDate = DateTime.Now,
                            ShipToAddress = new Address()
                            {
                                FirstName = "Hiso",
                                LastName = "Hoso",
                                Street = "street1",
                                City = "cairo",
                                State = "NY",
                                Zipcode = "1234"
                            },
                            DeliveryMethod = new DeliveryMethod()
                            {
                                ShortName = "by Car",
                                DeliveryTime = "one day",
                                Description = "هتوصل إن شاء الله",
                                Price = 2434
                            },
                            OrderItems = new List<OrderItem>()
                            {
                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 3,
                                        ProductName = "Chair"
                                    },
                                    Price = 100,
                                    Quantity = 4,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 1,
                                        ProductName = "Sofa"
                                    },
                                    Price = 40,
                                    Quantity = 3,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 7,
                                        ProductName = "table"
                                    },
                                    Price = 200,
                                    Quantity = 1,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 5,
                                        ProductName = "molla"
                                    },
                                    Price = 900,
                                    Quantity = 8,
                                },
                            },
                            Subtotal = 7920,
                            Status = OrderStatus.Pending,
                            PaymentIntentId = "53454nskjdfb"
                        },

                        new Order()
                        {
                            BuyerEmail = "Cusomter@1234",
                            OrderDate = DateTime.Now,
                            ShipToAddress = new Address()
                            {
                                FirstName = "Hiso",
                                LastName = "Hoso",
                                Street = "street1",
                                City = "cairo",
                                State = "NY",
                                Zipcode = "1234"
                            },
                            DeliveryMethod = new DeliveryMethod()
                            {
                                ShortName = "by Car",
                                DeliveryTime = "one day",
                                Description = "هتوصل إن شاء الله",
                                Price = 2434
                            },
                            OrderItems = new List<OrderItem>()
                            {
                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 3,
                                        ProductName = "Chair"
                                    },
                                    Price = 100,
                                    Quantity = 4,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 1,
                                        ProductName = "Sofa"
                                    },
                                    Price = 40,
                                    Quantity = 3,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 7,
                                        ProductName = "table"
                                    },
                                    Price = 200,
                                    Quantity = 1,
                                },

                                new OrderItem()
                                {
                                    ItemOrdered = new ProductItemOrdered()
                                    {
                                        ProductItemId = 5,
                                        ProductName = "molla"
                                    },
                                    Price = 900,
                                    Quantity = 8,
                                },
                            },
                            Subtotal = 7920,
                            Status = OrderStatus.PaymentFailed,
                            PaymentIntentId = "53454nskjdfb"
                        }
                    };

                    context.Orders.AddRange(ordersData);

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
