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
        public async Task<Basket> GetBasketAsync(string userId)
        {
            var user = await context.Users.FindAsync(userId);
            if (user == null)
                return null;
            var basket = await context.Baskets.SingleOrDefaultAsync(b => b.UserId == userId);
            if (basket == null)
            {
                var newBasket = new Basket()
                {
                    User = user,
                    UserId = userId
                };
                await context.Baskets.AddAsync(newBasket);
                await context.SaveChangesAsync();
                return newBasket;
            }
            else
                return basket;
        }
        public async Task<Basket> UpdateBasketAsync(Basket basket)
        {
            var existingBasket = await context.Baskets.FindAsync(basket.Id);

            if (existingBasket != null)
            {
                // TODO: Check if this is the best logic
                existingBasket.BasketItems = basket.BasketItems;
                await context.SaveChangesAsync();
                return existingBasket;
            }
            else
            {
                return null;
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
