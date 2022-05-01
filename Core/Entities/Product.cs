using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product:BaseEntity
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsSold { get; set; }
        public DateTime ManufactureDate { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int Weight { get; set; }
        public string Color { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Media> Pictures { get; set; }

        public virtual ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }

        public virtual ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
    }
}
