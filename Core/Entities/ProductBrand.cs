namespace Core.Entities
{
    public class ProductBrand: BaseEntity
    {
        public string Name { get; set; }
        public string Origin { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}