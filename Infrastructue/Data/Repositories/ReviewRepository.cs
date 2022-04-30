using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructue.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        public StoreContext context { get; set; }
        private int TotalReviews { get; set; }
        private int WeightedAverage { get; set; }
        private int[] Stars { get; set; }
        private int Rating { get; set; }

        public ReviewRepository(StoreContext _context)
        {
            context = _context;
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {

            return await context.Reviews
                //.Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IReadOnlyList<Review>> GetReviewsByProductIdAsync(int id)
        {
            return await context.Reviews
                .Where(r => r.ProductId == id)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Review>> GetReviewsAsync()
        {
            return await context.Reviews
                .ToListAsync();
        }

        public async void DeleteReviewAsync(int id)
        {
            Review review = await context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            context.Remove(review);
            await context.SaveChangesAsync();
        }

        public void GetTotalReviews (IReadOnlyList<Review> reviews)
        {
            foreach (var item in reviews)
            {
                switch (item.Stars)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        Stars[item.Stars - 1] += 1;
                        break;
                }

                TotalReviews += 1;
            }
        }

        public int CalculateRating()
        {
            var counter = 1;
            foreach (var item in Stars)
            {
                WeightedAverage += counter * item;
                counter++;
            }
            if (TotalReviews != 0)
            {
                Rating = WeightedAverage / TotalReviews;
            }

            return Rating;
        }
    }
}
