using Core.Entities;

namespace API.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsSold { get; set; }
        public DateTime ManufactureDate { get; set; }
        public Specification Specifications { get; set; }

        public  string ProductType { get; set; }

        public  string ProductBrand { get; set; }

    }
}
