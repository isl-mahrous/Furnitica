﻿using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IConfiguration configuration;
        private readonly IGenericRepository<DeliveryMethod> dmRepo;
        private readonly IGenericRepository<Core.Entities.Product> productRepo;

        public PaymentService(IBasketRepository basketRepository,
            IConfiguration configuration,
            IGenericRepository<DeliveryMethod> dmRepo,
            IGenericRepository<Core.Entities.Product> productRepo)
        {
            this.basketRepository = basketRepository;
            this.configuration = configuration;
            this.dmRepo = dmRepo;
            this.productRepo = productRepo;
        }
        public async Task<Basket> CreateOrUpdatePaymentIntent(string userId)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];

            var basket = await basketRepository.GetBasketAsync(userId);
            var shippingPrice = 0M;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await dmRepo.GetByIdAsync((int)basket.DeliveryMethodId);
                shippingPrice = deliveryMethod.Price;
            }

            // This check here should not happen unless client tampers with product price
            // before making a payment request

            foreach (var item in basket.BasketItems)
            {
                var productItem = await productRepo.GetByIdAsync(item.ProductId);

                if(item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }

            var service = new PaymentIntentService();

            PaymentIntent intent;

            // Creating a new payment intent

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long) basket.BasketItems.Sum(i => 
                    i.Quantity * (i.Price * 100)) + (long) shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() {"card"}
                };
                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.BasketItems.Sum(i =>
                    i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }
            await basketRepository.UpdateBasketAsync(basket);

            return basket;
        }
    }
}
