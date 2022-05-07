using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CustomerBasket
    {
        public int Id { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
    }
}
