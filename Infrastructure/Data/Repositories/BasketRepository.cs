using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly StoreContext context;
        public BasketRepository(StoreContext context)
        {
            this.context = context;
        }
        public async Task<CustomerBasket> GetBasketAsync(int basketId)
        {
            var basket = await context.Baskets.FindAsync(basketId);
            return basket != null ? basket : null;
        }
        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var existingBasket = await context.Baskets.FindAsync(basket.Id);
            if(existingBasket != null)
            {
                // TODO: Check if this is the best logic
                existingBasket.BasketItems = new();
                existingBasket.BasketItems.AddRange(basket.BasketItems);
                await context.SaveChangesAsync();
                return existingBasket;
            }
            else
            {
                context.Baskets.Add(basket);
                await context.SaveChangesAsync();
                return basket;
            }
        }
        public async Task DeleteBasketAsync(int basketId)
        {
            var foundBasket = await context.Baskets.FindAsync(basketId);

            if (foundBasket != null)
            {
                EntityEntry entityEntry = context.Entry(foundBasket);
                entityEntry.State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
}
