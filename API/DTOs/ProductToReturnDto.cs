namespace API.DTOs
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsSold { get; set; }
        public DateTime ManufactureDate { get; set; }

        //public Specification Specifications { get; set; }

        //public ICollection<Review> Reviews { get; set; }

        //public virtual ICollection<Media> Pictures { get; set; }

        public string  ProductType { get; set; }
     

        public string  ProductBrand { get; set; }
        
    }
}
