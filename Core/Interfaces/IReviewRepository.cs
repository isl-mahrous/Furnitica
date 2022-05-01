using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReviewRepository
    {


        Task<Review> GetReviewByIdAsync(int id);

        Task<IReadOnlyList<Review>> GetReviewsByProductIdAsync(int id);

        Task<IReadOnlyList<Review>> GetReviewsAsync();

        void DeleteReviewAsync(int id);
    }
}
