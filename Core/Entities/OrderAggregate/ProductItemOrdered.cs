using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    [Owned]
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {

        }
        public ProductItemOrdered(int productItemId, string productName, ICollection<Media> pictures)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            Pictures = pictures;
        }

        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public ICollection<Media> Pictures { get; set; }


    }
}
