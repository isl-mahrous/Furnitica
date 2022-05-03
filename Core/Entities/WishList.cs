using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class WishList
    {
        [Key]
        public int Id { get; set; }

        //public string UserId { get; set; }

        //[ForeignKey(nameof(UserId))]
        //public AppUser User { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
